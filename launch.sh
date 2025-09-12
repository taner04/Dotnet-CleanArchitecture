#!/usr/bin/env sh
set -eu

PORT=17266
URL="https://localhost:${PORT}"

SCRIPT_DIR="$(CDPATH= cd -- "$(dirname -- "$0")" && pwd)"
PROJECT_PATH="${SCRIPT_DIR}/tools/AppHost"

missing_deps=""

add_missing() {
  missing_deps="${missing_deps}\n$1|$2"
}

show_missing_table() {
  if command -v tput >/dev/null 2>&1; then
    C_WHITE="$(tput setaf 7)"
    C_BLUE="$(tput setaf 4)"
    C_RESET="$(tput sgr0)"
  else
    C_WHITE=""
    C_BLUE=""
    C_RESET=""
  fi

  printf "\n"
  printf "%s%-15s %s%s\n" "${C_WHITE}" "Dependency" "Link" "${C_RESET}"
  printf "%s%-15s %s%s\n" "${C_WHITE}" "----------" "----" "${C_RESET}"

  printf "%s" "$(printf "%b" "$missing_deps" | sed '/^$/d')" | while IFS='|' read -r name link; do
    [ -z "$name" ] && continue
    printf "%-15s " "$name"
    printf "%s%s%s\n" "${C_BLUE}" "$link" "${C_RESET}"
  done

  printf "\n"
}

have_cmd() { command -v "$1" >/dev/null 2>&1; }

dotnet_has_9() {
  have_cmd dotnet && dotnet --list-sdks 2>/dev/null | grep -E '^[[:space:]]*9\.' >/dev/null 2>&1
}

docker_running() {
  have_cmd docker && docker info >/dev/null 2>&1
}

start_docker_if_possible() {
  if docker_running; then
    return 0
  fi

  if [ "$(uname -s)" = "Darwin" ] && [ -d "/Applications/Docker.app" ] && have_cmd open; then
    open -g -a Docker || true
  else
    if have_cmd systemctl; then
      systemctl start docker 2>/dev/null || true
    fi
  fi

  deadline=$(($(date +%s) + 60))
  while [ "$(date +%s)" -lt "$deadline" ]; do
    if docker_running; then
      return 0
    fi
    sleep 1
  done

  return 1
}

wait_for_port_and_open() {
  port="$1"
  url="$2"

  open_cmd=""
  if have_cmd open; then
    open_cmd="open"
  elif have_cmd xdg-open; then
    open_cmd="xdg-open"
  fi

  deadline=$(($(date +%s) + 60))
  while [ "$(date +%s)" -lt "$deadline" ]; do
    if (command -v nc >/dev/null 2>&1 && nc -z localhost "$port" 2>/dev/null) ||
      (command -v lsof >/dev/null 2>&1 && lsof -iTCP:"$port" -sTCP:LISTEN >/dev/null 2>&1); then
      [ -n "$open_cmd" ] && "$open_cmd" "$url" >/dev/null 2>&1 || true
      break
    fi
    sleep 0.5
  done
}

if ! have_cmd docker; then
  add_missing "docker" "https://docs.docker.com/get-docker/"
fi

if ! dotnet_has_9; then
  add_missing "dotnet9" "https://dotnet.microsoft.com/en-us/download"
fi

if [ -n "$(printf "%b" "$missing_deps" | sed '/^$/d')" ]; then
  printf "\nThe following dependencies are missing:\n"
  show_missing_table
  printf "\nPlease install the missing dependencies and try again.\n"
  exit 1
fi

start_docker_if_possible || {
  printf "\nWarning: Docker does not appear to be running. Continuing anyway...\n"
}

orig_dir="$(pwd)"
trap 'cd "$orig_dir" >/dev/null 2>&1 || true' EXIT

cd "$PROJECT_PATH"

dotnet clean
dotnet build --nologo

wait_for_port_and_open "$PORT" "$URL" &

dotnet run

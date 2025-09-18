# Dotnet Clean Architecture

> **Note:** This is my first project implementing Clean Architecture in .NET. Feedback and suggestions are very welcome—feel free to open an issue or start a discussion!

This repository is a reference implementation of **Clean Architecture** principles in a .NET solution.  
It demonstrates how to structure a modern, maintainable, and testable application by separating concerns into clearly defined layers.

It is also intended to serve as inspiration for others looking to adopt Clean Architecture in their own .NET projects.

---

## 🚀 Getting Started

To get started, ensure you have the following prerequisites installed:

- [Docker](https://www.docker.com/get-started)
- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)

Once prerequisites are installed, you can clone the repository and follow the setup instructions below.

### 🏁 Running the Solution

To launch the application:

1. **Start Docker** to ensure all required containers and services can run.
2. Run the `AppHost` project. This will orchestrate the startup of all necessary services and dependencies.

Alternatively, run the provided script from the root directory:

- **Windows (PowerShell):**
  ```bash
  ./launch.ps1
  ```
- **MacOS/Unix (bash)**
  ```bash
  ./launch.sh
  ```
  This will start the application and all supporting services defined in the Aspire project.

---

## 📝 License

This project is licensed under the [MIT License](/LICENSE)

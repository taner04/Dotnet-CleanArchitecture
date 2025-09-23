<#
.SYNOPSIS
    Launches the AppHost project with required environment checks and opens the application in the browser.
#>

$ErrorActionPreference = 'Stop'

$projectPath = Join-Path $PSScriptRoot 'tools/AppHost'
$port = 17259
$url = "https://localhost:$port"


# --- Helper-Function ---
function Show-MissingTable
{
    param([hashtable]$Missing)

    Write-Host
    Write-Host ("{0,-15} {1}" -f "Dependency", "Link") -ForegroundColor White
    Write-Host ("{0,-15} {1}" -f "----------", "----") -ForegroundColor White

    foreach ($kv in $Missing.GetEnumerator())
    {
        Write-Host ("{0,-15} " -f $kv.Key) -NoNewline
        Write-Host $kv.Value -ForegroundColor Blue
    }

    Write-Host
}

function Test-Dependency
{
    $errors = @{ }
    $dockerDesktopPath = "$Env:ProgramFiles\Docker\Docker\Docker Desktop.exe"

    if (-not (Test-Path $dockerDesktopPath))
    {
        $errors['docker-desktop'] = 'https://www.docker.com/products/docker-desktop'
    }
    if (
    -not (Get-Command dotnet -ErrorAction SilentlyContinue) -or
            -not ((dotnet --list-sdks 2> $null) -match '^9\.')
    )
    {
        $errors['dotnet9'] = 'https://dotnet.microsoft.com/en-us/download'
    }
    return $errors
}

# --- End of Helper-Function ---

$myHashtable = Test-Dependency

if ($myHashtable.Count -gt 0)
{
    Write-Host "`nThe following dependencies are missing:"
    Show-MissingTable -Missing $myHashtable

    Write-Host "`nPlease install the missing dependencies and try again."

    exit 1
}
else
{
    try
    {
        docker desktop start | Out-Null
    }
    catch
    {
    }

    Set-Location $projectPath
    dotnet clean
    dotnet build --nologo

    Start-Job {
        param($u, $p)
        $deadline = (Get-Date).AddSeconds(60)
        while ((Get-Date) -lt $deadline)
        {
            try
            {
                if (Test-NetConnection -ComputerName 'localhost' -Port $p -InformationLevel Quiet)
                {
                    Start-Process $u; break
                }
            }
            catch
            {
            }
            Start-Sleep -Milliseconds 500
        }
    } -ArgumentList $url, $port | Out-Null

    dotnet run
}

Pop-Location

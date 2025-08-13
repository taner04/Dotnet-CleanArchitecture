if (-not (Get-Process -Name "Docker Desktop" -ErrorAction SilentlyContinue)) {
    docker desktop start | Out-Null
} 

Push-Location "src/eShop.AppHost"
try {
    $apiProcess = Get-Process -Name "Api" -ErrorAction SilentlyContinue
    if ($apiProcess) {
        Write-Information "Stopping existing API process..."
        $apiProcess | Stop-Process -Force
    }

    dotnet clean
    dotnet build

    Start-Process "https://localhost:17266"
    
    dotnet run
}
catch {
    Write-Error "An error occurred while starting the eShop application: $_"
} 
finally {
    Pop-Location
}
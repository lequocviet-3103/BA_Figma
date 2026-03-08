# Script ?? setup và ch?y AIBA project

Write-Host "=== AIBA Project Setup ===" -ForegroundColor Green

# Ki?m tra xem ?ã cài ??t EF Core tools ch?a
Write-Host "`nKi?m tra EF Core tools..." -ForegroundColor Yellow
$efToolsInstalled = dotnet tool list -g | Select-String "dotnet-ef"

if (-not $efToolsInstalled) {
    Write-Host "Cài ??t EF Core tools..." -ForegroundColor Yellow
    dotnet tool install --global dotnet-ef
}

# Restore packages
Write-Host "`nRestore NuGet packages..." -ForegroundColor Yellow
dotnet restore

# Build solution
Write-Host "`nBuild solution..." -ForegroundColor Yellow
dotnet build

# T?o migration n?u ch?a có
Write-Host "`nKi?m tra migrations..." -ForegroundColor Yellow
$migrationsPath = "AIBA.Infrastructure\Migrations"

if (-not (Test-Path $migrationsPath)) {
    Write-Host "T?o initial migration..." -ForegroundColor Yellow
    dotnet ef migrations add InitialCreate --project AIBA.Infrastructure --startup-project AIBA
} else {
    Write-Host "Migrations ?ã t?n t?i." -ForegroundColor Green
}

# Update database
Write-Host "`nUpdate database..." -ForegroundColor Yellow
dotnet ef database update --project AIBA.Infrastructure --startup-project AIBA

Write-Host "`n=== Setup hoàn t?t! ===" -ForegroundColor Green
Write-Host "`nL?u ý: Hãy c?p nh?t Gemini API Key trong appsettings.json tr??c khi s? d?ng API /analyze" -ForegroundColor Cyan
Write-Host "`n?? ch?y ?ng d?ng, s? d?ng l?nh:" -ForegroundColor Cyan
Write-Host "  dotnet run --project AIBA" -ForegroundColor White
Write-Host "`nSwagger s? t? ??ng m? t?i: https://localhost:7016/swagger" -ForegroundColor Cyan

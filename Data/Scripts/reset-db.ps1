cd "C:\Users\jorda\source\repos\CsharpCore"

$ErrorActionPreference = "Stop"

Write-Host "Suppression des anciennes migrations..."

Remove-Item -Recurse -Force .\Data\Migrations -ErrorAction SilentlyContinue


Write-Host "Suppression de la base..."

dotnet ef database drop --force `
  --project .\Data\Data.csproj `
  --startup-project .\Data\Data.csproj `
  --context Data.Context.ApplicationDbContext


Write-Host "Création de la migration InitialMigration..."

dotnet ef migrations add InitialMigration `
  --project .\Data\Data.csproj `
  --startup-project .\Data\Data.csproj `
  --context Data.Context.ApplicationDbContext `
  --output-dir Migrations


Write-Host "Mise à jour de la base..."

dotnet ef database update `
  --project .\Data\Data.csproj `
  --startup-project .\Data\Data.csproj `
  --context Data.Context.ApplicationDbContext


Write-Host ""
Write-Host "================================="
Write-Host " Reset DB terminé avec succès"
Write-Host " Migration : InitialMigration"
Write-Host "================================="

pause
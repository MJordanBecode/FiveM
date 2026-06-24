cd "C:\Users\jorda\source\repos\CsharpCore"

Start-Sleep -Seconds 1

Remove-Item -Recurse -Force .\Data\Migrations -ErrorAction SilentlyContinue

dotnet ef database drop --force `
  --project .\Data\Data.csproj `
  --startup-project .\Data\Data.csproj `
  --context Data.Context.ApplicationDbContext

dotnet ef migrations add InitialMigration `
  --project .\Data\Data.csproj `
  --startup-project .\Data\Data.csproj `
  --context Data.Context.ApplicationDbContext `
  --output-dir Migrations

dotnet ef database update `
  --project .\Data\Data.csproj `
  --startup-project .\Data\Data.csproj `
  --context Data.Context.ApplicationDbContext

Write-Host ""
Write-Host "Terminé. Appuie sur une touche pour fermer..."
$null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
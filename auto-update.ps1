cd "C:\Users\jorda\source\repos\CsharpCore"
$ErrorActionPreference = "Stop"

$MigrationName = "AutoUpdate_" + (Get-Date -Format "yyyyMMdd_HHmmss")

Write-Host "=========================================" -ForegroundColor Cyan
Write-Host "⚡ MISE À JOUR CHIRURGICALE DE LA PROD ⚡" -ForegroundColor Cyan
Write-Host "=========================================" -ForegroundColor Cyan
Write-Host ""

Write-Host "⏳ Analyse du code C# et génération du patch..." -ForegroundColor Yellow

dotnet ef migrations add $MigrationName `
  --project .\Data\Data.csproj `
  --startup-project .\Data\Data.csproj `
  --context Data.Context.ApplicationDbContext `
  --output-dir Migrations

# ⚠️ Vérification OBLIGATOIRE : dotnet.exe ne déclenche pas $ErrorActionPreference
if ($LASTEXITCODE -ne 0) {
    Write-Host ""
    Write-Host "❌ ÉCHEC : la génération de migration a échoué (code $LASTEXITCODE)" -ForegroundColor Red
    Write-Host "   Vérifie les erreurs de build ci-dessus." -ForegroundColor Red
    pause
    exit 1
}

Write-Host "🚀 Injection des modifications dans MySQL (sans reset)..." -ForegroundColor Yellow
dotnet ef database update `
  --project .\Data\Data.csproj `
  --startup-project .\Data\Data.csproj `
  --context Data.Context.ApplicationDbContext

if ($LASTEXITCODE -ne 0) {
    Write-Host ""
    Write-Host "❌ ÉCHEC : l'application de la migration sur MySQL a échoué (code $LASTEXITCODE)" -ForegroundColor Red
    pause
    exit 1
}

Write-Host ""
Write-Host "=====================================================" -ForegroundColor Green
Write-Host " ✅ Base de données mise à jour avec succès !" -ForegroundColor Green
Write-Host " Données préservées. Nouvelle migration : $MigrationName" -ForegroundColor Green
Write-Host "=====================================================" -ForegroundColor Green
Write-Host ""

pause
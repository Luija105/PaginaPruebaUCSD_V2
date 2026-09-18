# ==========================================
# Script de Despliegue Automatizado - Blazor
# ==========================================

param(
    [string]$RepoName = "PaginaPruebaUCSD_V2",
    [string]$Branch = "gh-pages"
)

Write-Host "1. Limpiando compilaciones anteriores..." -ForegroundColor Cyan
dotnet clean
dotnet publish -c Release -o release

# Ruta al index.html generado en la publicación
$indexPath = "release/wwwroot/index.html"

if (Test-Path $indexPath) {
    Write-Host "2. Ajustando la etiqueta base en index.html para GitHub Pages..." -ForegroundColor Cyan
    $content = Get-Content $indexPath -Raw
    # Reemplaza la ruta base para que coincida con el subdirectorio de GitHub Pages
    $content = $content -replace '<base href="/" />', "<base href="/PaginaPruebaUCSD_V2/" />"
    Set-Content $indexPath -Content
} else {
    Write-Error "No se encontró el archivo index.html en la carpeta de publicación."
    exit
}

# Crear archivo .nojekyll para evitar que GitHub ignore archivos que empiezan con guion bajo (_framework)
New-Item -Path "release/wwwroot/.nojekyll" -ItemType File -Force | Out-Null

Write-Host "3. Copiando 404.html para soporte de SPA (Single Page Application)..." -ForegroundColor Cyan
if (Test-Path "wwwroot/404.html") {
    Copy-Item "wwwroot/404.html" -Destination "release/wwwroot/404.html" -Force
}

Write-Host "4. Desplegando en la rama $Branch..." -ForegroundColor Cyan
# Entra a la carpeta de publicación compilada
Set-Location "release/wwwroot"

# Inicializa un repositorio temporal para subir los archivos estáticos
git init
git checkout -b $Branch
git add -A
git commit -m "Despliegue automatizado: $(Get-Date -Format 'yyyy-MM-dd HH:mm:ss')"

# Configura tu repositorio remoto (asegúrate de cambiar la URL si es necesario)
git remote add origin https://github.com/Luija105/PaginaPruebaUCSD_V2
git push -f origin $Branch

# Regresa al directorio raíz
Set-Location "../.."

Write-Host "¡Despliegue completado con éxito!" -ForegroundColor Green
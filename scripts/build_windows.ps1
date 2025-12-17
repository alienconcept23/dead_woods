# PowerShell helper to run Unity in batchmode and create a Windows development build.
# Usage: ./build_windows.ps1 -UnityPath "C:\Program Files\Unity\Hub\Editor\2022.3.0f1\Editor\Unity.exe"
param(
    [Parameter(Mandatory=$false)]
    [string]$UnityPath
)

if (-not $UnityPath) {
    # Try common Hub location (may differ depending on install)
    $possible = "C:\Program Files\Unity\Hub\Editor\2022.3.0f1\Editor\Unity.exe"
    if (Test-Path $possible) { $UnityPath = $possible }
}

if (-not $UnityPath -or -not (Test-Path $UnityPath)) {
    Write-Error "Unity executable not found. Pass -UnityPath or install Unity and adjust the script."
    exit 1
}

Write-Host "Running Unity to build Windows dev build..."
& "$UnityPath" -quit -batchmode -projectPath "$PWD" -executeMethod BuildScript.BuildWindowsDevelopment -logFile "build_log.txt"

if ($LASTEXITCODE -eq 0) { Write-Host "Build finished. See Builds/Windows/IsoSurvival" } else { Write-Error "Build exited with code $LASTEXITCODE. Check build_log.txt" }

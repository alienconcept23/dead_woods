<#
Set a GitHub Actions secret on a repository using gh CLI.
Usage examples:
  .\set_github_secret.ps1 -Owner "alienconcept23" -Repo "dead_woods" -Name "UNITY_LICENSE" -FromFile "C:\path\to\UnityLicense.ulf"
  .\set_github_secret.ps1 -Owner "alienconcept23" -Repo "dead_woods" -Name "UNITY_EMAIL" -Value "email@example.com"

Notes:
 - This uses `gh secret set` and requires gh to be authenticated and have permission to write secrets to the repo.
 - Do NOT paste secrets into chat. Run this locally.
#>
param(
    [Parameter(Mandatory=$true)] [string]$Owner,
    [Parameter(Mandatory=$true)] [string]$Repo,
    [Parameter(Mandatory=$true)] [string]$Name,
    [string]$FromFile,
    [string]$Value
)

function ExitWith($msg) { Write-Error $msg; exit 1 }
if (-not (Get-Command gh -ErrorAction SilentlyContinue)) { ExitWith "GitHub CLI not found. Install gh: https://cli.github.com/" }

if ($FromFile) {
    if (-not (Test-Path $FromFile)) { ExitWith "File not found: $FromFile" }
    # Read file and base64-encode
    $content = [System.IO.File]::ReadAllBytes($FromFile)
    $b64 = [Convert]::ToBase64String($content)
    gh secret set $Name --body $b64 --repo "$Owner/$Repo"
    if ($LASTEXITCODE -ne 0) { ExitWith "Failed to set secret $Name" }
    Write-Host "Secret $Name set from file (base64 encoded)."
} elseif ($Value) {
    gh secret set $Name --body $Value --repo "$Owner/$Repo"
    if ($LASTEXITCODE -ne 0) { ExitWith "Failed to set secret $Name" }
    Write-Host "Secret $Name set from provided value."
} else {
    ExitWith "Provide either -FromFile or -Value to set the secret."
}
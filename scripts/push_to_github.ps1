<#
Push the current project to GitHub and create the remote repo if needed.
Usage:
  .\push_to_github.ps1 -Owner "alienconcept23" -Repo "dead_woods"

Requires: Git, Git LFS (optional but recommended), and GitHub CLI (gh) authenticated.
This script will:
 - ensure git and gh are available
 - initialize git if necessary
 - ensure Git LFS is installed
 - create the repo under your GitHub account if it does not exist
 - push the current branch to origin/main
#>
param(
    [Parameter(Mandatory=$true)]
    [string]$Owner,
    [Parameter(Mandatory=$true)]
    [string]$Repo
)

function ExitWith($msg) { Write-Error $msg; exit 1 }

if (-not (Get-Command git -ErrorAction SilentlyContinue)) { ExitWith "git not found. Install Git: https://git-scm.com/downloads" }
if (-not (Get-Command gh -ErrorAction SilentlyContinue)) { ExitWith "GitHub CLI not found. Install gh: https://cli.github.com/" }

# Ensure we're in the project root (where README.md exists)
if (-not (Test-Path "README.md")) { Write-Warning "README.md not found in current folder. Make sure you are running this script from project root." }

# Init repo if needed
if (-not (Test-Path .git)) {
    Write-Host "Initializing local git repository..."
    & git init
    if ($LASTEXITCODE -ne 0) { ExitWith "git init failed (exit $LASTEXITCODE)" }

    Write-Host "Configuring Git LFS (optional)..."
    & git lfs install
    if ($LASTEXITCODE -ne 0) { Write-Warning "git lfs install failed or not available" }

    & git add .
    & git commit -m "Initial project skeleton"
    if ($LASTEXITCODE -ne 0) { Write-Warning "Initial commit failed (maybe nothing to commit)" }
}

$fullRepo = "$Owner/$Repo"
# Check if repo exists remotely
$exists = $false
& gh repo view $fullRepo --repo $fullRepo > $null 2>&1
if ($LASTEXITCODE -eq 0) { $exists = $true }

if (-not $exists) {
    Write-Host "Creating remote repo $fullRepo (public)."
    & gh repo create $fullRepo --public --source=. --remote=origin --push
    if ($LASTEXITCODE -ne 0) { ExitWith "gh repo create failed (exit $LASTEXITCODE)" }
} else {
    Write-Host "Remote repo exists. Setting origin and pushing."
    $remoteUrl = "https://github.com/$fullRepo.git"
    # Remove origin if exists
    & git remote remove origin 2>$null
    # Add and push
    & git remote add origin $remoteUrl
    if ($LASTEXITCODE -ne 0) { Write-Warning "Could not add remote; it may already exist." }
    & git branch -M main
    if ($LASTEXITCODE -ne 0) { Write-Warning "git branch -M main failed" }
    & git push -u origin main --force
    if ($LASTEXITCODE -ne 0) { ExitWith "git push failed (exit $LASTEXITCODE)" }
}

Write-Host "Push complete. Remote: https://github.com/$fullRepo"
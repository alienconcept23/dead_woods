# PowerShell helper to initialize the Git repo and configure Git LFS for the project.
# Run from project root in PowerShell on Windows.

if (-not (Get-Command git -ErrorAction SilentlyContinue)) {
    Write-Error "Git is not installed or not in PATH. Install Git for Windows first: https://git-scm.com/download/win"
    exit 1
}

git init
git add .
git commit -m "Initial project skeleton"

# Set up Git LFS for common asset types
if (-not (Get-Command git-lfs -ErrorAction SilentlyContinue)) {
    Write-Host "Git LFS not found. Installing via choco (requires Chocolatey) or visit https://git-lfs.github.com/ to install manually."
} else {
    git lfs install
    git lfs track "*.psd"
    git lfs track "*.png"
    git lfs track "*.jpg"
    git lfs track "*.mp3"
    git lfs track "*.wav"
    git add .gitattributes
    git commit -m "Configure Git LFS for assets"
}

Write-Host "Repo initialized. Add a remote and push to create a remote repository on GitHub, or run the optional GitHub create step below."

# Optional: create a GitHub remote if gh CLI is available
if (Get-Command gh -ErrorAction SilentlyContinue) {
    $resp = Read-Host "Detected GitHub CLI. Create repo on GitHub now? (y/n)"
    if ($resp -eq 'y') {
        gh repo create --public --source=. --remote=origin --push
    }
}

<#
Trigger the weekly build workflow manually on the repo.
Usage:
  .\trigger_weekly_build.ps1 -Owner "alienconcept23" -Repo "dead_woods"

Requires gh CLI authenticated.
#>
param(
    [Parameter(Mandatory=$true)] [string]$Owner,
    [Parameter(Mandatory=$true)] [string]$Repo
)
if (-not (Get-Command gh -ErrorAction SilentlyContinue)) { Write-Error "gh not installed"; exit 1 }

# Try to run the workflow by filename
gh workflow run weekly-build.yml --repo "$Owner/$Repo" || gh workflow run "Weekly Build" --repo "$Owner/$Repo" || Write-Error "Failed to trigger workflow. Check that workflow exists and gh is authenticated."
Write-Host "Triggered workflow (or attempted to). Check Actions tab on GitHub for status."
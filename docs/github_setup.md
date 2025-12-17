# GitHub setup (quick guide)

These steps let you push the project to `https://github.com/alienconcept23/dead_woods` and enable CI weekly builds.

Prerequisites (local machine):
- Git (https://git-scm.com)
- Git LFS (https://git-lfs.github.com)
- GitHub CLI `gh` (https://cli.github.com)
- Unity installed locally for local builds (if you want to build locally)

Important: Do NOT share secrets in chat. Use GitHub repository secrets only (Settings → Secrets and variables → Actions).

1) Push project to your GitHub repo (run locally in project root):

  .\scripts\push_to_github.ps1 -Owner "alienconcept23" -Repo "dead_woods"

2) Add CI secrets. Preferred: base64-encoded Unity license file (`UNITY_LICENSE`):
  - Generate base64 license file locally (PowerShell example):
    $b=[System.Convert]::ToBase64String([System.IO.File]::ReadAllBytes("C:\path\to\Unity_v2022.ulf"))
    (copy the output string and use it with the helper script)
  - Run:
    .\scripts\set_github_secret.ps1 -Owner "alienconcept23" -Repo "dead_woods" -Name "UNITY_LICENSE" -FromFile "C:\path\to\Unity_v2022.ulf"

  Alternative (credential method):
    .\scripts\set_github_secret.ps1 -Owner "alienconcept23" -Repo "dead_woods" -Name "UNITY_EMAIL" -Value "you@example.com"
    .\scripts\set_github_secret.ps1 -Owner "alienconcept23" -Repo "dead_woods" -Name "UNITY_PASSWORD" -Value "<password>"
    .\scripts\set_github_secret.ps1 -Owner "alienconcept23" -Repo "dead_woods" -Name "UNITY_SERIAL" -Value "<serial>"

3) Trigger the weekly build manually (or wait for Monday 02:00 UTC):
  .\scripts\trigger_weekly_build.ps1 -Owner "alienconcept23" -Repo "dead_woods"

4) Monitor Actions → Weekly Build. If successful the workflow will create a pre-release and attach the Windows build zip.

If you prefer, invite me as a collaborator on `dead_woods` (Write or Admin) and I can finish the CI configuration and run the first build for you. Tell me when the invite is accepted.

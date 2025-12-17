# Playtesting / Local Build Instructions

Quick steps to run the demo locally in the Editor or create a Windows build:

1. Open the project in Unity 2022.3 LTS (recommended).
2. Create the sample scene if it doesn't exist: menu IsoSurvival > Create Sample Scene.
3. Assign a material to the `IsoTilemap` component (create a simple material and add a placeholder texture). Use `IsoSurvival > Generate Demo Tilemap` to create a demo tilemap GameObject.
4. Create a simple placeable prefab (cube with collider) and assign it to `SnapGridBuilder.placeablePrefab`. Set `placementMask` to Default.
5. Press Play in the Editor to test movement, snapping and basic enemy behavior.

To produce a Windows development build (executable):
- Use the menu: IsoSurvival > Build > Build Windows Development (this calls `BuildScript.BuildWindowsDevelopment`).
- Or run the PowerShell helper: `.\scripts\build_windows.ps1 -UnityPath "C:\Path\To\Unity.exe"` from project root.

Notes:
- The build requires Unity to be installed on your machine. The build script places artifacts into `Builds/Windows/IsoSurvival`.
- For CI builds, configure the Unity secrets and use the GitHub Actions workflow in `.github/workflows/unity-build.yml`.
- For a faster test: you can run the Editor sample scene and use the in-Editor Play button.

If you want, I can run CI to provide a build artifact; to enable that I’ll need Unity license data in repository secrets (we can coordinate this securely).

CI secrets & setup (quick guide):
- Preferred: create a Unity license file locally and add it to GitHub as a base64-encoded secret named `UNITY_LICENSE`. Many CI workflows prefer a pre-encoded license to avoid storing email/password.
- Alternative: add `UNITY_EMAIL`, `UNITY_PASSWORD`, and `UNITY_SERIAL` as secrets (some builders support these directly).
- Add the secrets in your GitHub repo under `Settings` → `Secrets and variables` → `Actions`.

CI behavior & cadence:
- I added `weekly-build.yml`, a scheduled workflow that runs every Monday and creates a GitHub **pre-release** with a zipped Windows build attached.
- The existing `unity-build.yml` runs on push to `main` and uploads an Actions artifact (useful for PRs and on-demand builds).

If you want me to fully configure CI (add secrets, create releases), I can do it if you either grant me access to the repository or provide the secrets securely. Otherwise I can provide exact steps for you to add secrets and enable workflows.

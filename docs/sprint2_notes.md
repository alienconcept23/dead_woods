# Sprint 2 Notes (work log)

- Added `SimpleInput` adapter to support Unity Input System and legacy input fallbacks.
- Implemented `IsoTilemap` to generate a simple isometric tile grid for quick prototyping.
- Implemented `SnapGridBuilder` for snap-placement of prefabs onto the iso tilemap.
- Wired `SimpleInput` into `PlayerController` and added a simple attack hook.
- Added camera zoom & clamp controls in `CameraController`.

Next tasks:
- Create a dedicated sample scene with a demo tilemap, spawn a few enemies and a placeable prefab for snap tests.
- Add navigation / obstacle avoidance (NavMesh or grid pathfinding) for enemies and validate difficulty scaling.
- Replace the temporary editor scene creator with an actual Scene asset and prefabs in `Assets/` (requires Unity Editor).- Add an Editor build script and helper to produce a Windows development build locally (`BuildScript.BuildWindowsDevelopment` + `scripts/build_windows.ps1`) and document playtesting steps in `docs/playtesting.md`.
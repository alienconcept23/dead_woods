# Next Steps (Sprint 2)

Sprint 2 goals (in-progress):

- Integrate Unity Input System and provide a fallback adapter for legacy input (keyboard/mouse, controller, touch) — `SimpleInput` created
- Polish isometric camera and add zoom/clamp controls (work in progress)
- Create a basic isometric tilemap generator and placeholder 64px environment textures (`IsoTilemap` added)
- Prototype snap-grid builder for placement tests (`SnapGridBuilder` added)
- Add navigation/obstacle handling for player and enemies (nav/pacing next)
- Replace placeholder InputManager with Unity Input System bindings in next iteration

Acceptance criteria:
- Player can move fluidly and aim/attack
- Scene contains a small handcrafted test region to validate tiles and collisions
- Snap-placement of a prefab onto the isometric grid works via mouse/tap
- Basic enemy spawns and attacks the player, taking and dealing damage

Estimated time: 1–2 weeks

Progress notes:
- Implemented `SimpleInput.cs`, `IsoTilemap.cs`, `SnapGridBuilder.cs`, placeholder tile/prefab instructions, and editor helper to create sample scene.
- Next: wire `SimpleInput` into `PlayerController`, add zoom controls to `CameraController`, and build a small test scene with a tilemap + spawn points.


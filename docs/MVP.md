# MVP Specification — Iso Survival (Core)

Decisions
- Platform: Windows (initial). Later: Linux/macOS/console/mobile.
- Art style: Pixel-styled placeholder art for rapid iteration.
- Multiplayer: Post-MVP. Single-player first; networking designed later using Mirror.

Core loop
- Explore an isometric world, gather resources, build structures on a snap-grid, fight enemies, survive.

Must-have features (MVP)
- Player movement & camera (smooth follow + zoom)
- Isometric tilemap with snap-grid placement and ghost preview
- Basic build system (place/remove functional objects)
- Combat: melee hits, enemy basic AI (patrol/chase/attack)
- Health and loot drops
- One playable handcrafted level + quick random seed for replayability
- Placeholder pixel art and simple audio
- UI: HUD (health), basic inventory
- Windows build and weekly pre-release automation

Acceptance criteria
- Player can move, build simple structures, and fight an enemy in a single session.
- Buildable objects snap to grid and show a ghost on valid placement.
- Game builds on Windows without errors and a playable build zip is produced.

Milestones
1. MVP spec (this document) — agreed ✅
2. Core gameplay prototype — movement, camera, iso tilemap, snap builder
3. Combat & enemies — health, enemy AI, loot
4. UI & inventory
5. Level content + placeholder assets
6. Polish, testing, and Windows build automation

Immediate next steps
- Create `feature/core-prototype` branch with an implementation plan and start the player/camera/tilemap tasks.
- Commit incremental changes and open PRs for review.

Maintainer notes
- Tests: add unit tests for MVM-logic where practical; playtests for scene-level validation.
- CI: re-run builds after each merged milestone and ensure license activation persists.

Signed-off: GitHub Copilot

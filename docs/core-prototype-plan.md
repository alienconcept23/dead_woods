# Core Prototype Implementation Plan

Goal
- Deliver a playable prototype where the player can move around an isometric level, place snap-grid objects, and fight a basic enemy.

Tasks
1. Player & Input
   - Improve `PlayerController` for consistent movement & animations
   - Add input bindings for keyboard and gamepad

2. Camera
   - Implement smooth follow and zooming

3. Isometric Tilemap
   - Create a simple tilemap generator for placeholder tiles
   - Implement coordinate conversions and collision

4. Snap Builder
   - Ghost preview, validation, placement & removal

5. Enemy & Combat
   - Simple enemy AI (patrol -> chase -> attack)
   - Basic combat interactions and health

6. Scene & Playtest
   - Create a sample playtest scene and one level
   - Add quick debug controls for spawning enemies and resources

Deliverables
- Branch: `feature/core-prototype`
- PR with incremental commits for each task above
- Playtest build and docs for running locally

# Roadmap & MVP Definition

## What is the MVP?
MVP = Minimum Viable Product: the smallest, playable subset of features that demonstrates the core gameplay loop and lets us test/validate design and networking. For this project the MVP includes:

- Player spawn and movement (keyboard/mouse + controller + touch) in an isometric view
- Isometric tilemap / one handcrafted region and terrain
- Basic survival loop: health, hunger/thirst, simple inventory
- One class (templated system for Melee/Shooter/Magic) with at least one skill per class
- Basic combat with one enemy type and simple AI
- Snap-building prototype (place/delete, grid-snapping)
- Local save/load (option to enable OneDrive folder for cloud sync later)
- Host/Join (peer-hosted) multiplayer for up to a small number of players

## Short-term milestones (next 8-12 weeks)
1. Project skeleton + player movement + camera (this week)
2. Tilemap, first region, placeholder art (2 weeks)
3. Basic combat & one enemy type (3 weeks)
4. Inventory, crafting, and snap-building prototype (4-6 weeks)
5. Basic networking sync (peer-hosted) and simple lobby (6-8 weeks)

## Longer-term features
- Full class system, difficulty scaling, prestige/permadeath loop
- Additional handcrafted biomes + world persistence
- Steam integration, dedicated server tooling (if chosen later)
- Mobile & web build strategy

> Note: These are estimates; I will update timeline and deliverables as I implement and test major components.

# Iso Survival (Working title)

A cross-platform isometric/pixel survival game prototype. Steam-first with mobile & web later. Multiplayer: hosted peer-to-peer that also supports solo play. Art: 3× pixel characters on 3D-rendered 64px environments. Music: dark indie-folk shanties blending to hip-hop vibes.

## Project goals
- Open-world survival with handcrafted regions (3 starting biomes) and fast travel nodes.
- Class system (Melee / Shooter / Magic), snap-building base system, combat, and difficulty scaling including permadeath prestige runs.
- Multiplayer: peer-hosted sessions (player can host or join), optional solo play.
- Local saves with optional cloud sync integration (OneDrive, Google Drive, etc.) later.

## Tech stack
- Engine: Unity 2022.3 LTS (C#)
- Networking: Mirror (peer-hosted) / later Steamworks for matchmaking & Steam features

## Quick start (dev)
1. Install Unity 2022.3 LTS (recommended) or newer.
2. Open this folder as a Unity project or create a new Unity project and copy `Assets/` and `ProjectSettings/` into it.
3. Scripts live in `src/` for reference; import into Unity's `Assets/Scripts/` when ready.

## Release cadence
- Internal dev builds: weekly
- Public / player-facing builds: monthly

## Roadmap
See `docs/roadmap.md` for MVP definition and next milestones.

---

If you want a repo name change or a different folder layout, tell me now and I'll rename/move things.

---

GitHub quick actions:
- To push this project to `https://github.com/alienconcept23/dead_woods` run:
  - `.\scripts\push_to_github.ps1 -Owner "alienconcept23" -Repo "dead_woods"`
- To set CI secrets locally use the helper in `scripts/set_github_secret.ps1`.
- See `docs/github_setup.md` for more detailed steps and security notes.

## License
This project is currently licensed under the **MIT License** (see `LICENSE`). Placeholder assets are MIT by default; we can change asset licensing later if you prefer a separate commercial or CC license.
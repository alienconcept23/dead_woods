# Content Rating & Family Mode

## Goals
- Provide runtime toggles for family-friendly content while keeping options for more mature play.
- Ensure parents can enable/disable mature content with a PIN-protected option.

## Runtime toggles (data-driven)
- gore_level: 0 (none), 1 (mild), 2 (full)
- language_filter: true/false
- sexual_content: 0 (none), 1 (implied), 2 (mature) — we will avoid explicit nudity entirely
- mature_story_beats: true/false

## Implementation notes
- Settings stored in JSON with versioning; parental PIN option stored locally (optionally encrypted) and in cloud if user opts in.
- Option to build a separate "Family" store build if required by platform policies.
- Content toggles will be checked at spawn time and by the dialogue/quest systems to decide which variants to show.

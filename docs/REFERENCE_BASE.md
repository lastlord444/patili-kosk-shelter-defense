# REFERENCE_BASE.md

## Base Repository

| Field | Value |
|---|---|
| Repo | VampireSurvivorsClone |
| Author | matthiasbroske |
| URL | https://github.com/matthiasbroske/VampireSurvivorsClone |
| License | MIT |
| Import date | 2026-05-28 |
| Import method | GitHub Import (full history preserved) |

## Why This Base

- MIT license permits modification and commercial use with attribution.
- Unity mobile game with object pooling, enemy waves, character movement — architectural patterns relevant to shelter defense.
- Clean codebase for study and conversion.

## What We Use From This Base

- Unity project structure (Assets/, Packages/, ProjectSettings/)
- Game loop architecture: wave spawning, character movement, collision
- Object pooling patterns (uPools reference in source)
- Mobile input handling

## What Must Be Replaced Before Shipping

- All Vampire Survivors visual identity (sprites, UI, branding)
- Any non-MIT or non-commercial assets
- Game design: convert from auto-attack survivor to shelter defense
- All text references to Vampire Survivors

## Technical Reference Only (no code copied)

These repos were used for concept research only. No code or assets from them are in this repo:

- VampireSurvivorsClone (this is the base, MIT)
- sentaur-survivors (concept reference)
- survivors-roguelike-kit (concept reference)
- uPools (architecture reference)

## Template Evaluation Spike (2026-05-31)

A formal template evaluation was conducted to determine whether to continue with this base or pivot. See [TEMPLATE_EVALUATION.md](TEMPLATE_EVALUATION.md) for the full report.

**Decision: Continue with current base.**

| Evaluated Alternative | Reason for Rejection |
|---|---|
| survivors-roguelike-kit (Roo-Roo-Roo) | AI-generated pixel art/UI assets — violates project policy |
| Monster Survivors - Full Game (October Studio, $99) | Asset Store EULA — cannot commit to repo |
| Survival.io (Gorodiski Games, $79) | Asset Store EULA — cannot commit to repo |

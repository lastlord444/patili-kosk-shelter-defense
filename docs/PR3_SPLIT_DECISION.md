# PR #3 Split Decision

This document details the architect decision and rationale regarding the split/salvage decision for **PR #3 (Kenney asset audit and minimal placeholders)**.

---

## Decision Summary

- **Recommendation:** **CLOSE WITHOUT MERGING**
- **Action:** The valuable parts (documentation updates) have been salvaged and consolidated into PR #4. The remaining changes (placeholder asset additions and auto-generated shader noise) should be discarded.

---

## Rationale for Split & Rejection

PR #3 contains a mix of three types of changes:

1. **Asset Changes (Kenney Sprites):**
   - Sprites were added under `Assets/_PatiliKosk/Art/Placeholders/Kenney/`.
   - The corresponding ScriptableObject blueprint modifications (mapping player and enemies to these placeholders) were **fully rolled back** in the second commit of PR #3 (`aa58e30`) because the mass replacement reduced visual quality and collapsed enemy diversity.
   - Consequently, these placeholder sprites are currently **unused** and should not be merged into `main` in their current uncurated form.

2. **Unity-Generated Shader Noise:**
   - Opening the project in Unity 6 automatically generated and modified numerous TextMesh Pro shaders and shader graphs under `Assets/TextMesh Pro/Shaders/`.
   - These files were accidentally committed in PR #3, creating immense noise in the pull request diff and cluttering the repository.

3. **Documentation Changes:**
   - Valuable documentation files (`docs/ASSET_IDENTITY_AUDIT.md`, `docs/LICENSE_AUDIT.md` updates, `docs/RISK_REGISTER.md` updates) were created/modified to log the Kenney placeholder trial and its subsequent rollback.

---

## Salvage & Next Steps Plan

To maintain a clean and production-ready `main` branch, the following steps are proposed:

### Step 1: Close PR #3
- Close PR #3 (`feature/kenney-asset-audit-minimal-replacement`) without merging. This prevents unused assets and shader noise from entering `main`.

### Step 2: Clean and Rebase PR #4
- PR #4 (`docs/visual-direction-after-placeholder-rollback`) contains the salvaged documentation changes (including `docs/VISUAL_DIRECTION.md` and the updated `docs/ASSET_IDENTITY_AUDIT.md`) but *without* the Kenney sprites or TMPro shaders.
- However, PR #4 currently has minor issues that must be fixed before merging:
  - It deletes the mandatory `com.coplaydev.unity-mcp` package from `Packages/manifest.json`. **This must be restored.**
  - It contains verification leftovers like `Assets/AddressableAssetsData/link.xml` and `ProjectSettings/Packages/com.unity.testtools.codecoverage/Settings.json`. **These must be restored/removed from the diff.**
- **Action:** Checkout PR #4's branch, rebase it on `main`, resolve the leftovers, restore MCP package, push, and merge.

### Step 3: Clean Local Untracked Files
- Run `git clean -fd` or manually restore the auto-generated TextMesh Pro shaders in the local editor workspace so they do not contaminate future development branches.

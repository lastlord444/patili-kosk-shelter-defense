# RISK_REGISTER.md

> Last updated: 2026-05-30

## Risk Table

| ID | Risk | Probability | Impact | Mitigation | Status |
|---|---|---|---|---|---|
| R01 | Third-party art assets with unclear license | MEDIUM | HIGH | Full asset audit in Phase 2; replace all non-clear assets | ⚠️ Open (Focus in Phase 2) |
| R02 | Vampire Survivors IP claim (cloning concern) | LOW | HIGH | Game design fully converted to shelter defense; VS identity fully removed | ⚠️ Open (Focus in Phase 2) |
| R03 | Font license not suitable for commercial use | LOW | MEDIUM | Verified Noto Sans & LiberationSans are OFL (safe for commercial use) | ✔️ Mitigated |
| R04 | Audio/music not cleared for commercial use | MEDIUM | MEDIUM | Audit all audio; replace with CC0 or original audio | ⚠️ Open (Focus in Phase 2) |
| R07 | Scope creep — project remains VS clone instead of shelter defense | MEDIUM | HIGH | Enforce REPO_TRUTH and CONVERSION_PLAN in all PRs | ⚠️ Open |
| R08 | Android performance issues from base project | LOW | MEDIUM | Profile after core gameplay conversion | ⚠️ Open (Build success verified) |
| R09 | Mobile virtual joystick input wiring issue | LOW | MEDIUM | Resolved - Uncommented SendValueToControl calls in TouchJoystick.cs | ✔️ Mitigated |
| R15 | Unity MCP removal breaks editor-state verification and can lead to fake Unity evidence | LOW | HIGH | Do not remove MCP package from verification branches. MCP unavailable = blocker. | ⚠️ Open |
| R16 | TextMesh Pro auto-generated shader noise clutters PR diffs | HIGH | LOW | Use `.gitignore` or clean untracked TMPro shaders before commit | ⚠️ Open |
| R17 | Visual variety collapse from over-aggressive placeholders | HIGH | HIGH | Reject mass placeholder trial; enforce curated asset mapping protocol | ✔️ Mitigated (Rollback completed) |

## Closed Risks

| ID | Risk | Resolution |
|---|---|---|
| R00 | MIT license not preserved | Resolved — LICENSE file preserved in import. |
| R05 | Unity version incompatibility on dev machine | Resolved — Successfully upgraded from 2021.3.21f1 to Unity 6 (6000.3.16f1) with 0 compiler errors. |
| R06 | Unity package dependencies with incompatible licenses | Resolved — Audited Packages/manifest.json; all dependencies are official Unity packages under Unity EULA. |
| R09 | Mobile virtual joystick input wiring issue | Resolved — Uncommented SendValueToControl calls in TouchJoystick.cs. Verified in Play Mode. |
| R17 | Visual variety collapse from over-aggressive placeholders | Resolved — Rolled back the Kenney placeholder trial and restored original assets to protect gameplay variety. |

## Notes

- Art and IP risks (R01, R02) will be addressed systematically under the new curated asset mapping protocol (`docs/VISUAL_DIRECTION.md`).
- The success of the Android build verified that there are no compiler blockers or SDK/NDK integration blockers on the development environment.
- Mobile virtual joystick input wiring blocker was successfully resolved and verified via Play Mode smoke test simulation.

# RISK_REGISTER.md

> Last updated: 2026-05-28

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
| R14 | Over-aggressive placeholder replacement can destroy enemy variety and reduce product quality | MEDIUM | HIGH | No mass placeholder application; no one-sprite-for-all-enemies; require visual direction doc and replacement matrix before asset replacement; require screenshots and rollback plan. | ⚠️ Open |

## Closed Risks

| ID | Risk | Resolution |
|---|---|---|
| R00 | MIT license not preserved | Resolved — LICENSE file preserved in import. |
| R05 | Unity version incompatibility on dev machine | Resolved — Successfully upgraded from 2021.3.21f1 to Unity 6 (6000.3.16f1) with 0 compiler errors. |
| R06 | Unity package dependencies with incompatible licenses | Resolved — Audited Packages/manifest.json; all dependencies are official Unity packages under Unity EULA. |
| R09 | Mobile virtual joystick input wiring issue | Resolved — Uncommented SendValueToControl calls in TouchJoystick.cs. Verified in Play Mode. |

## Notes

- Art and IP risks (R01, R02) will be addressed immediately in Phase 2.
- The success of the Android build verified that there are no compiler blockers or SDK/NDK integration blockers on the development environment.
- Mobile virtual joystick input wiring blocker was successfully resolved and verified via Play Mode smoke test simulation.


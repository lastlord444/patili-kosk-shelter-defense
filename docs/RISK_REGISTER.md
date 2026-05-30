# RISK_REGISTER.md

> Last updated: 2026-05-28

## Risk Table

| ID | Risk | Probability | Impact | Mitigation | Status |
|---|---|---|---|---|---|
| R01 | Third-party art assets with unclear license | MEDIUM | HIGH | Full asset audit in Phase 2; replace all non-clear assets | ⚠️ Open |
| R02 | Vampire Survivors IP claim (pony cloning concern) | LOW | HIGH | Game design fully converted to shelter defense; VS identity fully removed before shipping | ⚠️ Open |
| R03 | Font license not suitable for commercial use | MEDIUM | MEDIUM | Audit all fonts; replace with OFL/MIT-licensed fonts | ⚠️ Open |
| R04 | Audio/music not cleared for commercial use | MEDIUM | MEDIUM | Audit all audio; replace with CC0 or original audio | ⚠️ Open |
| R05 | Unity version incompatibility on new dev machine | LOW | MEDIUM | Check ProjectSettings/ProjectVersion.txt for exact version | ⚠️ Open |
| R06 | Unity package dependencies with incompatible licenses | LOW | MEDIUM | Audit Packages/manifest.json; all Unity packages are Unity EULA | ⚠️ Open |
| R07 | Scope creep — project becomes VS clone instead of shelter defense | MEDIUM | HIGH | Enforce REPO_TRUTH and CONVERSION_PLAN in all PRs | ⚠️ Open |
| R08 | Android performance issues from base project | LOW | MEDIUM | Profile after Phase 1 build verification | ⚠️ Open |

## Closed Risks

| ID | Risk | Resolution |
|---|---|---|
| R00 | MIT license not preserved | Resolved — LICENSE file preserved in import. |

## Notes

- Risks R01–R04 require manual Unity project inspection (cannot be audited from GitHub alone).
- All HIGH-impact risks must be resolved before any public/store release.

using UnityEngine;
using System.Collections;

namespace Vampire
{
    public class Level1WaveDirector : MonoBehaviour
    {
        private EntityManager entityManager;
        private LevelBlueprint levelBlueprint;
        private LevelManager levelManager;
        private float waveTimer = 0f;
        private float spawnCooldown = 0f;
        private bool winConditionTriggered = false;

        private bool shownWave1 = false;
        private bool shownWave2 = false;
        private bool shownWave3 = false;

        public void Init(LevelManager levelManager, EntityManager entityManager, LevelBlueprint levelBlueprint)
        {
            this.levelManager = levelManager;
            this.entityManager = entityManager;
            this.levelBlueprint = levelBlueprint;
            waveTimer = 0f;
            spawnCooldown = 0f;
            winConditionTriggered = false;
            shownWave1 = false;
            shownWave2 = false;
            shownWave3 = false;
            Debug.Log("[Level1WaveDirector] Initialized for Level 1 single-lane tutorial onboarding.");
        }

        public bool IsActive(float levelTime)
        {
            // Always handle pacing for Level 1 to prevent default spawner interference
            return true;
        }

        void Update()
        {
            if (levelManager == null || entityManager == null || levelBlueprint == null) return;

            waveTimer += Time.deltaTime;
            
            // Victory win condition at 120 seconds
            if (waveTimer >= 120f)
            {
                if (!winConditionTriggered)
                {
                    winConditionTriggered = true;
                    StartCoroutine(WinSequence());
                }
                return;
            }

            // Trigger wave direction banner announcements
            TriggerWaveAnnouncements();

            spawnCooldown -= Time.deltaTime;

            if (spawnCooldown <= 0f)
            {
                float currentPeriod = waveTimer;
                int activeCount = (entityManager.LivingMonsters != null) ? entityManager.LivingMonsters.Count : 0;
                
                // Active enemy limits on screen at once to prevent player or shelter overwhelm
                int maxAllowed = 99;
                if (currentPeriod < 10f) maxAllowed = 2;       // 0-10s: max 2
                else if (currentPeriod < 30f) maxAllowed = 3;  // 10-30s: max 3
                else if (currentPeriod < 45f) maxAllowed = 4;  // 30-45s: max 4
                else if (currentPeriod < 60f) maxAllowed = 4;  // 45-60s: max 4
                else maxAllowed = 5;                          // 60-120s: max 5

                if (activeCount < maxAllowed)
                {
                    if (currentPeriod >= 0f && currentPeriod < 45f)
                    {
                        // 0-45s: Spawns only from the Right side
                        float cooldownTime = 3.5f;
                        if (currentPeriod < 10f) cooldownTime = 5f;
                        
                        SpawnNormalMeleeEnemyAtDirection(Vector2.right);
                        spawnCooldown = cooldownTime;
                    }
                    else if (currentPeriod >= 45f && currentPeriod < 90f)
                    {
                        // 45-90s: Spawns only from the Left side
                        SpawnNormalMeleeEnemyAtDirection(Vector2.left);
                        spawnCooldown = 3f;
                    }
                    else if (currentPeriod >= 90f && currentPeriod < 120f)
                    {
                        // 90-120s: Small final wave from the Right side
                        SpawnNormalMeleeEnemyAtDirection(Vector2.right);
                        spawnCooldown = 2.5f;
                    }
                }
                else
                {
                    // Too many enemies on screen, delay checking
                    spawnCooldown = 1f;
                }
            }
        }

        private void TriggerWaveAnnouncements()
        {
            if (waveTimer >= 0f && waveTimer < 45f && !shownWave1)
            {
                shownWave1 = true;
                EliteWarningUI.CreateProcedural("DUSMAN SAGDAN GELIYOR", 3.5f);
            }
            else if (waveTimer >= 45f && waveTimer < 90f && !shownWave2)
            {
                shownWave2 = true;
                EliteWarningUI.CreateProcedural("DUSMAN SOLDAN GELIYOR", 3.5f);
            }
            else if (waveTimer >= 90f && waveTimer < 120f && !shownWave3)
            {
                shownWave3 = true;
                EliteWarningUI.CreateProcedural("SON DALGA SAGDAN GELIYOR", 3.5f);
            }
        }

        private void SpawnNormalMeleeEnemyAtDirection(Vector2 direction)
        {
            if (levelBlueprint.monsters == null || levelBlueprint.monsters.Length == 0) return;
            
            // Level 1 Ranged Enemy Ban: filter out any Ranged, Throwing, Boomerang or Boss blueprints
            MonsterBlueprint blueprint = null;
            int poolIndex = 0;
            int tries = 0;
            do
            {
                poolIndex = Random.Range(0, levelBlueprint.monsters.Length);
                var container = levelBlueprint.monsters[poolIndex];
                if (container.monsterBlueprints != null && container.monsterBlueprints.Length > 0)
                {
                    int blueprintIndex = Random.Range(0, container.monsterBlueprints.Length);
                    blueprint = container.monsterBlueprints[blueprintIndex];
                }
                tries++;
            } while ((blueprint == null || 
                      blueprint is RangedMonsterBlueprint || 
                      blueprint is ThrowingMonsterBlueprint || 
                      blueprint is BoomerangMonsterBlueprint || 
                      blueprint is BossMonsterBlueprint) && tries < 50);

            if (blueprint == null) return;
            
            // Early enemy HP override for onboarding balance (negative value sets absolute HP)
            float hpValue = -blueprint.hp;
            if (waveTimer < 60f)
            {
                hpValue = -10f; // Dies in exactly 1 hit of 12 damage
            }
            else
            {
                hpValue = -15f; // Dies in 2 hits of 12 damage
            }
            
            entityManager.SpawnMonsterAtDirection(poolIndex, blueprint, direction, hpValue);
        }

        private IEnumerator WinSequence()
        {
            Debug.Log("[Level1WaveDirector] Win Condition met! 120s survived.");
            
            // Show Victory/Clear banner
            EliteWarningUI.CreateProcedural("BARINAK KORUNDU - SEVIYE TAMAMLANDI!", 4f);
            
            // Wait for 3 seconds of real time (since Time.timeScale is still 1 here)
            yield return new WaitForSeconds(3f);
            
            // Trigger victory screen in level manager
            if (levelManager != null)
            {
                levelManager.LevelPassed(null);
            }
        }
    }
}

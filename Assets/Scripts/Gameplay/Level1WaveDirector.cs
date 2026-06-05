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
        private bool eliteWarningTriggered = false;
        private bool eliteSpawned = false;
        private bool winConditionTriggered = false;

        private bool shownWave1 = false;
        private bool shownWave2 = false;
        private bool shownWave3 = false;
        private bool shownWave4 = false;

        public void Init(LevelManager levelManager, EntityManager entityManager, LevelBlueprint levelBlueprint)
        {
            this.levelManager = levelManager;
            this.entityManager = entityManager;
            this.levelBlueprint = levelBlueprint;
            waveTimer = 0f;
            spawnCooldown = 0f;
            eliteWarningTriggered = false;
            eliteSpawned = false;
            winConditionTriggered = false;
            shownWave1 = false;
            shownWave2 = false;
            shownWave3 = false;
            shownWave4 = false;
            Debug.Log("[Level1WaveDirector] Initialized for Level 1 wave pacing.");
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
            
            // Survived 120 seconds victory check
            if (waveTimer >= 120f)
            {
                if (!winConditionTriggered)
                {
                    winConditionTriggered = true;
                    StartCoroutine(WinSequence());
                }
                return;
            }

            // Trigger wave banner announcements
            TriggerWaveAnnouncements();

            spawnCooldown -= Time.deltaTime;

            if (spawnCooldown <= 0f)
            {
                float currentPeriod = waveTimer;
                int activeCount = (entityManager.LivingMonsters != null) ? entityManager.LivingMonsters.Count : 0;
                
                // Screen crowding safeguards: limit active enemies on screen at once
                int maxAllowed = 99;
                if (currentPeriod < 10f) maxAllowed = 2;       // 0-10s: max 2
                else if (currentPeriod < 30f) maxAllowed = 3;  // 10-30s: max 3
                else if (currentPeriod < 60f) maxAllowed = 5;  // 30-60s: max 5
                else if (currentPeriod < 90f) maxAllowed = 8;  // 60-90s: max 8
                else maxAllowed = 12;                          // 90-120s: max 12

                if (activeCount < maxAllowed)
                {
                    if (currentPeriod >= 0f && currentPeriod < 10f)
                    {
                        // 0-10s: Onboarding, very low density (1 normal enemy every 5s) from the Right
                        SpawnNormalEnemyAtDirection(Vector2.right);
                        spawnCooldown = 5f;
                    }
                    else if (currentPeriod >= 10f && currentPeriod < 30f)
                    {
                        // 10-30s: Low/medium pressure (1 normal enemy every 3s) from the Left
                        SpawnNormalEnemyAtDirection(Vector2.left);
                        spawnCooldown = 3f;
                    }
                    else if (currentPeriod >= 30f && currentPeriod < 60f)
                    {
                        // 30-60s: Increased pressure (2 normal enemies every 4s) from Top & Bottom
                        SpawnNormalEnemyAtDirection(Vector2.up);
                        SpawnNormalEnemyAtDirection(Vector2.down);
                        spawnCooldown = 4f;
                    }
                    else if (currentPeriod >= 60f && currentPeriod < 90f)
                    {
                        // 60-90s: All directions (2 normal enemies every 3s)
                        SpawnNormalEnemyAtDirection(GetRandomDirection());
                        SpawnNormalEnemyAtDirection(GetRandomDirection());
                        spawnCooldown = 3f;
                    }
                    else if (currentPeriod >= 90f && currentPeriod < 105f)
                      {
                        // 90-105s: Constant pressure (2 normal enemies every 3s)
                        SpawnNormalEnemyAtDirection(GetRandomDirection());
                        SpawnNormalEnemyAtDirection(GetRandomDirection());
                        spawnCooldown = 3f;
                    }
                    else if (currentPeriod >= 105f && currentPeriod < 108f)
                    {
                        // 105-108s: Elite warning buffer, no new spawns
                        if (!eliteWarningTriggered)
                        {
                            Character player = FindFirstObjectByType<Character>();
                            if (player != null && player.CurrentLevel >= 3)
                            {
                                eliteWarningTriggered = true;
                                EliteWarningUI.CreateProcedural("TEHLIKELI ELIT DUSMAN YAKLASIYOR", 3.5f);
                            }
                        }
                        spawnCooldown = 0.5f; // Keep checking but don't spawn
                    }
                    else if (currentPeriod >= 108f && currentPeriod < 120f)
                    {
                        // 108s: Conditional Elite spawn
                        if (!eliteSpawned)
                        {
                            eliteSpawned = true;
                            Character player = FindFirstObjectByType<Character>();
                            if (player != null && player.CurrentLevel >= 3)
                            {
                                SpawnEliteEnemyAtDirection(Vector2.right);
                            }
                            else
                            {
                                // Spawn a small normal wave instead of elite to prevent unfair death
                                for (int i = 0; i < 3; i++)
                                {
                                    SpawnNormalEnemyAtDirection(GetRandomDirection());
                                }
                            }
                        }

                        // 108-120s: Final light pressure
                        SpawnNormalEnemyAtDirection(GetRandomDirection());
                        spawnCooldown = 3.5f;
                    }
                }
                else
                {
                    // Too many enemies on screen, check again in 1s
                    spawnCooldown = 1f;
                }
            }
        }

        private void TriggerWaveAnnouncements()
        {
            if (waveTimer >= 0f && waveTimer < 10f && !shownWave1)
            {
                shownWave1 = true;
                EliteWarningUI.CreateProcedural("DUSMAN SAGDAN GELIYOR", 3.5f);
            }
            else if (waveTimer >= 10f && waveTimer < 30f && !shownWave2)
            {
                shownWave2 = true;
                EliteWarningUI.CreateProcedural("DUSMAN SOLDAN GELIYOR", 3.5f);
            }
            else if (waveTimer >= 30f && waveTimer < 60f && !shownWave3)
            {
                shownWave3 = true;
                EliteWarningUI.CreateProcedural("DUSMAN YUKARIDAN VE ASAGIDAN GELIYOR", 3.5f);
            }
            else if (waveTimer >= 60f && waveTimer < 90f && !shownWave4)
            {
                shownWave4 = true;
                EliteWarningUI.CreateProcedural("DUSMAN HER YONDEN GELIYOR", 3.5f);
            }
        }

        private Vector2 GetRandomDirection()
        {
            float rand = Random.value;
            if (rand < 0.25f) return Vector2.right;
            if (rand < 0.50f) return Vector2.left;
            if (rand < 0.75f) return Vector2.up;
            return Vector2.down;
        }

        private void SpawnNormalEnemyAtDirection(Vector2 direction)
        {
            if (levelBlueprint.monsters == null || levelBlueprint.monsters.Length == 0) return;
            
            // Choose a random monster index from standard monsters container
            int poolIndex = Random.Range(0, levelBlueprint.monsters.Length);
            var container = levelBlueprint.monsters[poolIndex];
            if (container.monsterBlueprints == null || container.monsterBlueprints.Length == 0) return;
            
            int blueprintIndex = Random.Range(0, container.monsterBlueprints.Length);
            MonsterBlueprint blueprint = container.monsterBlueprints[blueprintIndex];
            
            // Dynamic HP scale based on wave time for onboarding / rescue balance
            float hpValue = blueprint.hp;
            if (waveTimer < 30f)
            {
                hpValue = 15f; // dies in 2 hits of 10 damage
            }
            else if (waveTimer < 60f)
            {
                hpValue = 20f; // dies in 2 hits
            }
            else if (waveTimer < 90f)
            {
                hpValue = 25f; // dies in 3 hits
            }
            else
            {
                hpValue = 30f; // dies in 3 hits
            }
            
            entityManager.SpawnMonsterAtDirection(poolIndex, blueprint, direction, hpValue);
        }

        private void SpawnEliteEnemyAtDirection(Vector2 direction)
        {
            if (levelBlueprint.monsters == null || levelBlueprint.monsters.Length == 0) return;

            // Pick first monster pool as the elite template (Melee Crab)
            int poolIndex = 0; 
            var container = levelBlueprint.monsters[poolIndex];
            if (container.monsterBlueprints == null || container.monsterBlueprints.Length == 0) return;

            MonsterBlueprint blueprint = container.monsterBlueprints[0];
            
            // Spawn monster at direction
            Monster elite = entityManager.SpawnMonsterAtDirection(poolIndex, blueprint, direction, blueprint.hp);
            if (elite != null)
            {
                // Apply modifiers: 1.5x HP, 1.05x Speed, 1.2x Scale, purple/red tint, no damage buff
                elite.ApplyEliteModifiers(1.5f, 1.05f, 1.2f, new Color(0.9f, 0.4f, 0.9f, 1f));
                Debug.Log($"[Level1WaveDirector] Spawned Elite Monster: {blueprint.name} from {direction} with 1.5x HP, 1.05x Speed.");
            }
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

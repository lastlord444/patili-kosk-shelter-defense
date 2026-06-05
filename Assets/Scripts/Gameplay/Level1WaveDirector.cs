using UnityEngine;

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

        public void Init(LevelManager levelManager, EntityManager entityManager, LevelBlueprint levelBlueprint)
        {
            this.levelManager = levelManager;
            this.entityManager = entityManager;
            this.levelBlueprint = levelBlueprint;
            waveTimer = 0f;
            spawnCooldown = 0f;
            eliteWarningTriggered = false;
            eliteSpawned = false;
            Debug.Log("[Level1WaveDirector] Initialized for Level 1 wave pacing.");
        }

        public bool IsActive(float levelTime)
        {
            // Handles pacing for the first 90 seconds
            return levelTime <= 90f;
        }

        void Update()
        {
            if (levelManager == null || entityManager == null || levelBlueprint == null) return;

            waveTimer += Time.deltaTime;
            if (waveTimer > 90f) return;

            spawnCooldown -= Time.deltaTime;

            if (spawnCooldown <= 0f)
            {
                float currentPeriod = waveTimer;
                if (currentPeriod >= 0f && currentPeriod < 10f)
                {
                    // 0-10s: Onboarding, very low density (1 normal enemy every 5s)
                    SpawnRandomNormalEnemy();
                    spawnCooldown = 5f;
                }
                else if (currentPeriod >= 10f && currentPeriod < 30f)
                {
                    // 10-30s: Low/medium pressure (1 normal enemy every 3s)
                    SpawnRandomNormalEnemy();
                    spawnCooldown = 3f;
                }
                else if (currentPeriod >= 30f && currentPeriod < 60f)
                {
                    // 30-60s: Increased pressure (2 normal enemies every 4s)
                    SpawnRandomNormalEnemy();
                    SpawnRandomNormalEnemy();
                    spawnCooldown = 4f;
                }
                else if (currentPeriod >= 60f && currentPeriod < 63f)
                {
                    // 60-63s: Elite warning buffer, no new spawns
                    if (!eliteWarningTriggered)
                    {
                        eliteWarningTriggered = true;
                        TriggerEliteWarning();
                    }
                    spawnCooldown = 0.5f; // Keep checking but don't spawn
                }
                else if (currentPeriod >= 63f && currentPeriod < 90f)
                {
                    // 63s: Spawn elite yengeç (Melee Crab)
                    if (!eliteSpawned)
                    {
                        eliteSpawned = true;
                        SpawnEliteEnemy();
                        // Spawn a few companion normal enemies
                        for (int i = 0; i < 3; i++)
                        {
                            SpawnRandomNormalEnemy();
                        }
                    }

                    // 63-90s: Controlled high pressure (2 normal enemies every 3s)
                    SpawnRandomNormalEnemy();
                    SpawnRandomNormalEnemy();
                    spawnCooldown = 3f;
                }
            }
        }

        private void SpawnRandomNormalEnemy()
        {
            if (levelBlueprint.monsters == null || levelBlueprint.monsters.Length == 0) return;
            
            // Choose a random monster index from standard monsters container
            int poolIndex = Random.Range(0, levelBlueprint.monsters.Length);
            var container = levelBlueprint.monsters[poolIndex];
            if (container.monsterBlueprints == null || container.monsterBlueprints.Length == 0) return;
            
            int blueprintIndex = Random.Range(0, container.monsterBlueprints.Length);
            MonsterBlueprint blueprint = container.monsterBlueprints[blueprintIndex];
            
            entityManager.SpawnMonsterRandomPosition(poolIndex, blueprint, blueprint.hp);
        }

        private void TriggerEliteWarning()
        {
            EliteWarningUI.CreateProcedural(3f);
        }

        private void SpawnEliteEnemy()
        {
            if (levelBlueprint.monsters == null || levelBlueprint.monsters.Length == 0) return;

            // Pick first monster pool as the elite template (Melee Crab)
            int poolIndex = 0; 
            var container = levelBlueprint.monsters[poolIndex];
            if (container.monsterBlueprints == null || container.monsterBlueprints.Length == 0) return;

            MonsterBlueprint blueprint = container.monsterBlueprints[0];
            
            // Spawn monster randomly offscreen
            Monster elite = entityManager.SpawnMonsterRandomPosition(poolIndex, blueprint, blueprint.hp);
            if (elite != null)
            {
                // Apply modifiers: 2.0x HP, 1.15x Speed, 1.3x Scale, purple/red tint
                elite.ApplyEliteModifiers(2.0f, 1.15f, 1.3f, new Color(0.9f, 0.4f, 0.9f, 1f));
                Debug.Log($"[Level1WaveDirector] Spawned Elite Monster: {blueprint.name} with 2x HP, 1.15x Speed.");
            }
        }
    }
}

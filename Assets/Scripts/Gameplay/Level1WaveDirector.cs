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

        private bool shownWave1 = false;
        private bool shownWave2 = false;
        private bool shownWave3 = false;
        private bool shownWave4 = false;
        private bool shownWave5 = false;

        public void Init(LevelManager levelManager, EntityManager entityManager, LevelBlueprint levelBlueprint)
        {
            this.levelManager = levelManager;
            this.entityManager = entityManager;
            this.levelBlueprint = levelBlueprint;
            waveTimer = 0f;
            spawnCooldown = 0f;
            eliteWarningTriggered = false;
            eliteSpawned = false;
            shownWave1 = false;
            shownWave2 = false;
            shownWave3 = false;
            shownWave4 = false;
            shownWave5 = false;
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

            // Trigger wave banner announcements
            TriggerWaveAnnouncements();

            spawnCooldown -= Time.deltaTime;

            if (spawnCooldown <= 0f)
            {
                float currentPeriod = waveTimer;
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
                    // 63s: Spawn elite yengeç (Melee Crab) on the Right
                    if (!eliteSpawned)
                    {
                        eliteSpawned = true;
                        SpawnEliteEnemyAtDirection(Vector2.right);
                        // Spawn a few companion normal enemies from random directions
                        for (int i = 0; i < 3; i++)
                        {
                            Vector2 dir = GetRandomDirection();
                            SpawnNormalEnemyAtDirection(dir);
                        }
                    }

                    // 63-90s: Controlled high pressure (2 normal enemies every 3s) from All Directions
                    SpawnNormalEnemyAtDirection(GetRandomDirection());
                    SpawnNormalEnemyAtDirection(GetRandomDirection());
                    spawnCooldown = 3f;
                }
            }
        }

        private void TriggerWaveAnnouncements()
        {
            if (waveTimer >= 0f && waveTimer < 10f && !shownWave1)
            {
                shownWave1 = true;
                EliteWarningUI.CreateProcedural("DUSMANLAR SAGDAN YAKLASIYOR!", 3.5f);
            }
            else if (waveTimer >= 10f && waveTimer < 30f && !shownWave2)
            {
                shownWave2 = true;
                EliteWarningUI.CreateProcedural("DUSMANLAR SOLDAN YAKLASIYOR!", 3.5f);
            }
            else if (waveTimer >= 30f && waveTimer < 60f && !shownWave3)
            {
                shownWave3 = true;
                EliteWarningUI.CreateProcedural("DUSMANLAR KUZEY VE GUNEYDEN YAKLASIYOR!", 3.5f);
            }
            else if (waveTimer >= 60f && waveTimer < 63f && !shownWave4)
            {
                shownWave4 = true;
                EliteWarningUI.CreateProcedural("TEHLIKELI ELIT DUSMAN YAKLASIYOR!", 3.5f);
            }
            else if (waveTimer >= 63f && waveTimer < 90f && !shownWave5)
            {
                shownWave5 = true;
                EliteWarningUI.CreateProcedural("DUSMANLAR HER YONDEN SALDIRIYOR!", 3.5f);
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
            
            entityManager.SpawnMonsterAtDirection(poolIndex, blueprint, direction, blueprint.hp);
        }

        private void TriggerEliteWarning()
        {
            // TriggerWaveAnnouncements handles the procedural warning UI
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
                // Apply modifiers: 2.0x HP, 1.15x Speed, 1.3x Scale, purple/red tint
                elite.ApplyEliteModifiers(2.0f, 1.15f, 1.3f, new Color(0.9f, 0.4f, 0.9f, 1f));
                Debug.Log($"[Level1WaveDirector] Spawned Elite Monster: {blueprint.name} from {direction} with 2x HP, 1.15x Speed.");
            }
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

namespace Vampire
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private LevelBlueprint levelBlueprint;
        [SerializeField] private Character playerCharacter;
        [SerializeField] private EntityManager entityManager;
        [SerializeField] private AbilityManager abilityManager;
        [SerializeField] private AbilitySelectionDialog abilitySelectionDialog;
        [SerializeField] private InfiniteBackground infiniteBackground;
        [SerializeField] private Inventory inventory;
        [SerializeField] private StatsManager statsManager;
        [SerializeField] private GameOverDialog gameOverDialog;
        [SerializeField] private GameTimer gameTimer;
        private PatiliKosk.Shelter shelter;
        private float levelTime = 0;
        private float timeSinceLastMonsterSpawned;
        private float timeSinceLastChestSpawned;
        private bool miniBossSpawned = false;
        private bool finalBossSpawned = false;

        public void Init(LevelBlueprint levelBlueprint)
        {
            this.levelBlueprint = levelBlueprint;
            levelTime = 0;

            if (shelter == null)
            {
                shelter = FindFirstObjectByType<PatiliKosk.Shelter>();
            }
            
            // Initialize the entity manager
            entityManager.Init(this.levelBlueprint, playerCharacter, inventory, statsManager, infiniteBackground, abilitySelectionDialog, shelter);
            // Initialize the ability manager
            abilityManager.Init(this.levelBlueprint, entityManager, playerCharacter, abilityManager);
            abilitySelectionDialog.Init(abilityManager, entityManager, playerCharacter);
            // Initialize the character
            playerCharacter.Init(entityManager, abilityManager, statsManager);
            playerCharacter.OnDeath.AddListener(GameOver);
            if (shelter != null)
            {
                shelter.OnDeath.AddListener(GameOver);
            }
            // Spawn initial gems (Bypassed for Level 1 to prevent instant level up at 00:00)
            bool isLevel1 = SceneManager.GetActiveScene().name == "Level 1" || SceneManager.GetActiveScene().buildIndex == 1;
            if (!isLevel1)
            {
                entityManager.SpawnGemsAroundPlayer(this.levelBlueprint.initialExpGemCount, this.levelBlueprint.initialExpGemType);
            }
            // Spawn a singular chest (Bypassed for Level 1 to focus on shelter defense onboarding)
            if (!isLevel1)
            {
                entityManager.SpawnChest(levelBlueprint.chestBlueprint);
            }
            // Initialize the infinite background
            infiniteBackground.Init(this.levelBlueprint.backgroundTexture, playerCharacter.transform);
            // Initialize inventory
            inventory.Init();

            // Initialize Level 1 Wave Director if present
            Level1WaveDirector waveDirector = GetComponent<Level1WaveDirector>();
            if (waveDirector != null)
            {
                waveDirector.Init(this, entityManager, levelBlueprint);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            // Programmatically attach Level1WaveDirector for Level 1 scene
            if (SceneManager.GetActiveScene().name == "Level 1" || SceneManager.GetActiveScene().buildIndex == 1)
            {
                if (gameObject.GetComponent<Level1WaveDirector>() == null)
                {
                    gameObject.AddComponent<Level1WaveDirector>();
                }
            }

            Init(levelBlueprint);
        }

        // Update is called once per frame
        void Update()
        {
            bool isLevel1 = SceneManager.GetActiveScene().name == "Level 1" || SceneManager.GetActiveScene().buildIndex == 1;
            // Time
            levelTime += Time.deltaTime;
            gameTimer.SetTime(levelTime);

            // Check if Level1WaveDirector is active and handling spawning
            Level1WaveDirector waveDirector = GetComponent<Level1WaveDirector>();
            bool waveDirectorActive = waveDirector != null && waveDirector.IsActive(levelTime);

            // Monster spawning timer
            if (levelTime < levelBlueprint.levelTime)
            {
                if (!waveDirectorActive)
                {
                    timeSinceLastMonsterSpawned += Time.deltaTime;
                    float spawnRate = levelBlueprint.monsterSpawnTable.GetSpawnRate(levelTime/levelBlueprint.levelTime);
                    float monsterSpawnDelay = spawnRate > 0 ? 1.0f/spawnRate : float.PositiveInfinity;
                    if (timeSinceLastMonsterSpawned >= monsterSpawnDelay)
                    {
                        (int monsterIndex, float hpMultiplier) = levelBlueprint.monsterSpawnTable.SelectMonsterWithHPMultiplier(levelTime/levelBlueprint.levelTime);
                        (int poolIndex, int blueprintIndex) = levelBlueprint.MonsterIndexMap[monsterIndex];
                        MonsterBlueprint monsterBlueprint = levelBlueprint.monsters[poolIndex].monsterBlueprints[blueprintIndex];
                        entityManager.SpawnMonsterRandomPosition(poolIndex, monsterBlueprint, monsterBlueprint.hp * hpMultiplier);
                        timeSinceLastMonsterSpawned = Mathf.Repeat(timeSinceLastMonsterSpawned, monsterSpawnDelay);
                    }
                }
            }
            // Boss spawning
            if (!miniBossSpawned && levelTime > levelBlueprint.miniBosses[0].spawnTime)
            {
                miniBossSpawned = true;
                entityManager.SpawnMonsterRandomPosition(levelBlueprint.monsters.Length, levelBlueprint.miniBosses[0].bossBlueprint);
            }
            // Boss spawning
            if (!finalBossSpawned && levelTime > levelBlueprint.levelTime)
            {
                //entityManager.KillAllMonsters();
                finalBossSpawned = true;
                Monster finalBoss = entityManager.SpawnMonsterRandomPosition(levelBlueprint.monsters.Length, levelBlueprint.finalBoss.bossBlueprint);
                finalBoss.OnKilled.AddListener(LevelPassed);
            }
            // Chest spawning timer (Bypassed for Level 1 to focus on shelter defense)
            if (!isLevel1)
            {
                timeSinceLastChestSpawned += Time.deltaTime;
                if (timeSinceLastChestSpawned >= levelBlueprint.chestSpawnDelay)
                {
                    for (int i = 0; i < levelBlueprint.chestSpawnAmount; i++)
                    {
                        entityManager.SpawnChest(levelBlueprint.chestBlueprint);
                    }
                    timeSinceLastChestSpawned = Mathf.Repeat(timeSinceLastChestSpawned, levelBlueprint.chestSpawnDelay);
                }
            }
        }

        public void GameOver()
        {
            Time.timeScale = 0;
            int coinCount = PlayerPrefs.GetInt("Coins");
            PlayerPrefs.SetInt("Coins", coinCount + statsManager.CoinsGained);
            gameOverDialog.Open(false, statsManager);
        }

        public void LevelPassed(Monster finalBossKilled)
        {
            Time.timeScale = 0;
            int coinCount = PlayerPrefs.GetInt("Coins");
            PlayerPrefs.SetInt("Coins", coinCount + statsManager.CoinsGained);
            gameOverDialog.Open(true, statsManager);
        }

        public void Restart()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void ReturnToMainMenu()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
    }
}

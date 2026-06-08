using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
        private PistolAbility cachedPistolAbility = null;

        private PistolAbility GetPistolAbility()
        {
            if (cachedPistolAbility != null)
            {
                return cachedPistolAbility;
            }

            var pistols = Object.FindObjectsByType<PistolAbility>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            if (pistols != null && pistols.Length > 0)
            {
                cachedPistolAbility = pistols[0];
                if (pistols.Length > 1)
                {
                    Debug.Log($"[SkillHUD] Multiple PistolAbilities found. Using first active one: {cachedPistolAbility.gameObject.name}");
                }
            }

            if (cachedPistolAbility == null)
            {
                Debug.Log("[SkillHUD] PistolAbility not found; burst cancelled.");
            }

            return cachedPistolAbility;
        }

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

            // Set Player and Shelter start positions for Level 1 layout contract
            var player = FindFirstObjectByType<Character>();
            if (player != null)
            {
                player.transform.position = new Vector3(-2f, -1.2f, 0f);
                var playerRb = player.GetComponent<Rigidbody2D>();
                if (playerRb != null) playerRb.position = new Vector2(-2f, -1.2f);
            }
            
            var shelter = FindFirstObjectByType<PatiliKosk.Shelter>();
            if (shelter != null)
            {
                shelter.transform.position = new Vector3(-5f, -3f, 0f);
            }

            SetupHUD();
        }

        void Start()
        {
            SetupHUD();
        }

        private void SetupHUD()
        {
            var invButtons = GameObject.Find("Inventory Buttons");
            if (invButtons != null)
            {
                bool rightButtonActivated = false;
                foreach (Transform child in invButtons.transform)
                {
                    if (child.name == "Button [Right]" && !rightButtonActivated)
                    {
                        child.gameObject.SetActive(true);
                        rightButtonActivated = true;
                        
                        // Disable the InventorySlot component to prevent HUD visual clash in Level 1
                        var slot = child.gameObject.GetComponent<InventorySlot>();
                        if (slot != null)
                        {
                            slot.enabled = false;
                        }

                        // Hide any sub graphics (like Health, Bomb, Magnet icons) under the active button
                        foreach (Transform sub in child)
                        {
                            if (sub.name != "CooldownOverlay" && sub.name != "CooldownText")
                            {
                                sub.gameObject.SetActive(false);
                            }
                        }

                        var hud = child.gameObject.GetComponent<Level1SkillHUD>();
                        if (hud == null)
                        {
                            hud = child.gameObject.AddComponent<Level1SkillHUD>();
                        }
                        hud.Init(8f, 8f, TriggerMultiShotBurst); // cooldown: 8s, start ready
                    }
                    else
                    {
                        // Hide all other duplicate or inactive buttons (Top, Bottom, Left, etc.)
                        child.gameObject.SetActive(false);
                    }
                }
                Debug.Log("[SkillHUD] Disabled inactive buttons: top/bottom/left");
            }
        }

        private void EnforceHUDDeactivation()
        {
            var invButtons = GameObject.Find("Inventory Buttons");
            if (invButtons != null)
            {
                bool rightButtonActivated = false;
                foreach (Transform child in invButtons.transform)
                {
                    if (child.name == "Button [Right]" && !rightButtonActivated)
                    {
                        child.gameObject.SetActive(true);
                        rightButtonActivated = true;
                    }
                    else
                    {
                        if (child.gameObject.activeSelf)
                        {
                            child.gameObject.SetActive(false);
                        }
                    }
                }
            }
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
            
            // Enforce HUD buttons deactivation periodically to prevent any external reactivation
            if (Time.frameCount % 30 == 0)
            {
                EnforceHUDDeactivation();
            }

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
                
                int maxAllowed = 2;
                float cooldownTime = 4.5f;

                if (currentPeriod < 30f)
                {
                    maxAllowed = 2;
                    cooldownTime = 4.5f;
                }
                else if (currentPeriod < 75f)
                {
                    maxAllowed = 3;
                    cooldownTime = 3.5f;
                }
                else
                {
                    maxAllowed = 5;
                    cooldownTime = 2.5f;
                }

                if (activeCount < maxAllowed)
                {
                    // Always spawn from the Right side
                    SpawnNormalMeleeEnemyAtDirection(Vector2.right);
                    spawnCooldown = cooldownTime;
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
            if (waveTimer >= 0f && waveTimer < 30f && !shownWave1)
            {
                shownWave1 = true;
                EliteWarningUI.CreateProcedural("DUSMAN SAG USTTEN GELIYOR", 3.5f);
            }
            else if (waveTimer >= 30f && waveTimer < 75f && !shownWave2)
            {
                shownWave2 = true;
                EliteWarningUI.CreateProcedural("YENI DALGA SAGDAN GELIYOR", 3.5f);
            }
            else if (waveTimer >= 75f && waveTimer < 120f && !shownWave3)
            {
                shownWave3 = true;
                EliteWarningUI.CreateProcedural("SON DALGA SAGDAN GELIYOR", 3.5f);
            }
        }

        private void SpawnNormalMeleeEnemyAtDirection(Vector2 direction)
        {
            if (levelBlueprint.monsters == null || levelBlueprint.monsters.Length == 0) return;
            
            // Gather all available blueprints by rank
            List<(int pool, MonsterBlueprint bp)> juniors = new List<(int, MonsterBlueprint)>();
            List<(int pool, MonsterBlueprint bp)> mediums = new List<(int, MonsterBlueprint)>();
            List<(int pool, MonsterBlueprint bp)> seniors = new List<(int, MonsterBlueprint)>();

            for (int p = 0; p < levelBlueprint.monsters.Length; p++)
            {
                var container = levelBlueprint.monsters[p];
                if (container.monsterBlueprints == null) continue;
                for (int b = 0; b < container.monsterBlueprints.Length; b++)
                {
                    var bp = container.monsterBlueprints[b];
                    if (bp == null) continue;
                    if (bp is RangedMonsterBlueprint || 
                        bp is ThrowingMonsterBlueprint || 
                        bp is BoomerangMonsterBlueprint || 
                        bp is BossMonsterBlueprint)
                    {
                        continue;
                    }

                    if (bp.hp <= 10f) juniors.Add((p, bp));
                    else if (bp.hp <= 30f) mediums.Add((p, bp));
                    else seniors.Add((p, bp));
                }
            }

            // Decide which group to spawn based on composition rules
            List<(int pool, MonsterBlueprint bp)> targetGroup = juniors;
            float roll = Random.value;

            if (waveTimer < 30f)
            {
                targetGroup = juniors;
            }
            else if (waveTimer < 75f)
            {
                if (roll < 0.70f) targetGroup = juniors;
                else targetGroup = mediums;
            }
            else
            {
                if (roll < 0.50f) targetGroup = juniors;
                else if (roll < 0.85f) targetGroup = mediums;
                else targetGroup = seniors;
            }

            // Fallback chain to ensure we always get a valid group if lists are empty
            if (targetGroup.Count == 0)
            {
                if (juniors.Count > 0) targetGroup = juniors;
                else if (mediums.Count > 0) targetGroup = mediums;
                else if (seniors.Count > 0) targetGroup = seniors;
            }

            if (targetGroup.Count == 0) return;

            var chosen = targetGroup[Random.Range(0, targetGroup.Count)];
            int poolIndex = chosen.pool;
            MonsterBlueprint blueprint = chosen.bp;

            // Differentiate HP strictly by taxonomy (constant throughout Level 1)
            float hpOverride = 12f;
            if (blueprint.hp <= 10f) hpOverride = 12f; // Junior (Crab)
            else if (blueprint.hp <= 30f) hpOverride = 20f; // Medium (Alien)
            else hpOverride = 30f; // Senior (PunchMonster)
            
            float hpValue = -hpOverride; // negative value sets absolute HP
            
            entityManager.SpawnMonsterAtDirection(poolIndex, blueprint, direction, hpValue);
        }

        private void TriggerMultiShotBurst()
        {
            StartCoroutine(MultiShotBurstCoroutine());
        }

        private IEnumerator MultiShotBurstCoroutine()
        {
            var player = FindFirstObjectByType<Character>();
            if (player == null || entityManager == null) yield break;
            
            var pistol = GetPistolAbility();
            if (pistol == null) yield break;
            
            var type = typeof(PistolAbility); // Use actual ability type to retrieve projectile details
            var projIndexField = type.GetField("projectileIndex", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance) 
                                 ?? typeof(ProjectileAbility).GetField("projectileIndex", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var damageField = type.GetField("damage", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                              ?? typeof(ProjectileAbility).GetField("damage", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var speedField = type.GetField("speed", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                             ?? typeof(ProjectileAbility).GetField("speed", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var knockbackField = type.GetField("knockback", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                                 ?? typeof(ProjectileAbility).GetField("knockback", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var layerField = type.GetField("monsterLayer", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                             ?? typeof(ProjectileAbility).GetField("monsterLayer", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            if (projIndexField == null || damageField == null || speedField == null || knockbackField == null || layerField == null) yield break;

            int projIndex = (int)projIndexField.GetValue(pistol);
            float dmg = ((UpgradeableDamage)damageField.GetValue(pistol)).Value * 0.7f;
            float spd = ((UpgradeableProjectileSpeed)speedField.GetValue(pistol)).Value;
            float kb = ((UpgradeableKnockback)knockbackField.GetValue(pistol)).Value;
            LayerMask mask = (LayerMask)layerField.GetValue(pistol);

            Vector3 spawnPos = player.CenterTransform.position;
            List<Monster> sortedMonsters = new List<Monster>();
            var nearby = entityManager.Grid.FindNearbyInRadius(spawnPos, 12f);
            if (nearby != null)
            {
                foreach (var client in nearby)
                {
                    if (client is Monster monster && monster.HP > 0)
                    {
                        sortedMonsters.Add(monster);
                    }
                }
            }

            sortedMonsters.Sort((a, b) => 
                Vector3.Distance(a.CenterTransform.position, spawnPos)
                .CompareTo(Vector3.Distance(b.CenterTransform.position, spawnPos))
            );

            int targetCount = sortedMonsters.Count;
            int totalBullets = 10;
            int targetSeekingCount = 0;
            int bulletsPerTarget = 1;
            List<Monster> targets = new List<Monster>();

            if (targetCount == 0)
            {
                totalBullets = 8;
            }
            else if (targetCount == 1)
            {
                totalBullets = 10;
                targetSeekingCount = 4;
                bulletsPerTarget = 4;
                targets.Add(sortedMonsters[0]);
            }
            else
            {
                totalBullets = 10;
                int maxTargets = Mathf.Min(targetCount, 5);
                for (int i = 0; i < maxTargets; i++)
                {
                    targets.Add(sortedMonsters[i]);
                }
                bulletsPerTarget = 2;
                targetSeekingCount = targets.Count * 2;
            }

            Debug.Log($"[SkillHUD] Multi Shot Burst fired. Projectile count: {totalBullets}");

            float delayBetweenShots = 0.05f;

            for (int i = 0; i < totalBullets; i++)
            {
                if (player == null) yield break;
                spawnPos = player.CenterTransform.position;

                bool isTargetSeeking = (i < targetSeekingCount);
                Monster targetMonster = null;
                if (isTargetSeeking)
                {
                    int targetIdx = i / bulletsPerTarget;
                    if (targetIdx < targets.Count)
                    {
                        targetMonster = targets[targetIdx];
                    }
                }

                Vector2 dir;
                if (isTargetSeeking && targetMonster != null && targetMonster.HP > 0)
                {
                    dir = ((Vector2)targetMonster.CenterTransform.position - (Vector2)spawnPos).normalized;
                }
                else
                {
                    float angle = (i * 45f) % 360f;
                    dir = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
                }

                // Add slight spread/chaos offset
                dir = (dir + new Vector2(UnityEngine.Random.Range(-0.08f, 0.08f), UnityEngine.Random.Range(-0.08f, 0.08f))).normalized;

                Projectile projectile = entityManager.SpawnProjectile(projIndex, spawnPos, dmg, kb, spd, mask);
                if (projectile != null)
                {
                    projectile.OnHitDamageable.AddListener(player.OnDealDamage.Invoke);
                    projectile.Launch(dir);
                }

                yield return new WaitForSeconds(delayBetweenShots);
            }
        }

        private IEnumerator WinSequence()
        {
            Debug.Log("[Level1WaveDirector] Win Condition met! 120s survived.");
            
            // Show Victory/Clear banner
            EliteWarningUI.CreateProcedural("BARINAK KORUNDU - SEVIYE TAMAMLANDI!", 4f);
            
            // Wait for 3 seconds of real time
            yield return new WaitForSeconds(3f);
            
            // Trigger victory screen in level manager
            if (levelManager != null)
            {
                levelManager.LevelPassed(null);
            }
        }
    }

    public class Level1SkillHUD : MonoBehaviour
    {
        private UnityEngine.UI.Image cooldownImage;
        private TMPro.TextMeshProUGUI cooldownText;
        private float cooldownDuration = 8f;
        private float currentTimer = 8f; // Start ready
        private System.Action onSkillReady;
        private bool wasReady = false;

        public void Init(float duration, float startTimer, System.Action onReady)
        {
            this.cooldownDuration = duration;
            this.currentTimer = startTimer;
            this.onSkillReady = onReady;
            this.wasReady = (currentTimer >= cooldownDuration);

            Debug.Log($"[SkillHUD] Found Button Right: {gameObject.name}");

            // Set up button visual
            var img = GetComponent<UnityEngine.UI.Image>();
            if (img != null)
            {
                img.sprite = CreateProceduralSkillIcon();
                img.color = Color.white;
            }

            // Remove persistent slot click listeners and bind custom click trigger
            var btn = GetComponent<UnityEngine.UI.Button>();
            if (btn != null)
            {
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(OnButtonClicked);
                btn.interactable = true;
                Debug.Log($"[SkillHUD] Button interactable: {btn.interactable}");
            }
            Debug.Log("[SkillHUD] Bound OnClick to Button Right");
            Debug.Log($"[SkillHUD] Cooldown ready: {wasReady}");
            
            // Setup count object if any to hide it
            var countObj = transform.Find("Count");
            if (countObj != null) countObj.gameObject.SetActive(false);
            
            // Set up cooldown overlay if not already present
            Transform overlayTransform = transform.Find("CooldownOverlay");
            if (overlayTransform == null)
            {
                GameObject overlayGo = new GameObject("CooldownOverlay");
                overlayGo.transform.SetParent(transform, false);
                var rect = overlayGo.AddComponent<RectTransform>();
                rect.anchorMin = Vector2.zero;
                rect.anchorMax = Vector2.one;
                rect.sizeDelta = Vector2.zero;
                
                cooldownImage = overlayGo.AddComponent<UnityEngine.UI.Image>();
                cooldownImage.color = new Color(0f, 0f, 0f, 0.65f); // dark semi-transparent
                cooldownImage.type = UnityEngine.UI.Image.Type.Filled;
                cooldownImage.fillMethod = UnityEngine.UI.Image.FillMethod.Radial360;
                cooldownImage.fillOrigin = (int)UnityEngine.UI.Image.Origin360.Top;
                cooldownImage.fillClockwise = false;
            }
            else
            {
                cooldownImage = overlayTransform.GetComponent<UnityEngine.UI.Image>();
            }

            // Set up cooldown text if not already present
            Transform textTransform = transform.Find("CooldownText");
            if (textTransform == null)
            {
                GameObject textGo = new GameObject("CooldownText");
                textGo.transform.SetParent(transform, false);
                var rect = textGo.AddComponent<RectTransform>();
                rect.anchorMin = Vector2.zero;
                rect.anchorMax = Vector2.one;
                rect.sizeDelta = Vector2.zero;

                cooldownText = textGo.AddComponent<TMPro.TextMeshProUGUI>();
                cooldownText.alignment = TMPro.TextAlignmentOptions.Center;
                cooldownText.fontSize = 28;
                cooldownText.fontStyle = TMPro.FontStyles.Bold;
                cooldownText.color = Color.white;
            }
            else
            {
                cooldownText = textTransform.GetComponent<TMPro.TextMeshProUGUI>();
            }

            // Ensure overlay image is active and does not block clicks
            if (cooldownImage != null)
            {
                cooldownImage.raycastTarget = false;
                cooldownImage.gameObject.SetActive(true);
            }

            // Ensure cooldown text is active, does not block clicks, and is brought to front
            if (cooldownText != null)
            {
                cooldownText.raycastTarget = false;
                cooldownText.gameObject.SetActive(true);
                cooldownText.transform.SetAsLastSibling(); // Make sure text renders on top of overlay/button
            }

            UpdateVisuals();
        }

        private void OnButtonClicked()
        {
            Debug.Log("[SkillHUD] Right skill clicked");
            if (currentTimer >= cooldownDuration)
            {
                currentTimer = 0f;
                wasReady = false;
                onSkillReady?.Invoke();
            }
            else
            {
                float remaining = cooldownDuration - currentTimer;
                Debug.Log($"[SkillHUD] Skill not ready. Remaining: {remaining:f1}s");
            }
        }

        private void Update()
        {
            if (currentTimer < cooldownDuration)
            {
                currentTimer += Time.deltaTime;
                if (currentTimer >= cooldownDuration)
                {
                    currentTimer = cooldownDuration;
                    if (!wasReady)
                    {
                        wasReady = true;
                        Debug.Log("[SkillHUD] Cooldown complete. Skill ready.");
                    }
                }
            }

            UpdateVisuals();
        }

        private void UpdateVisuals()
        {
            var btn = GetComponent<UnityEngine.UI.Button>();
            if (cooldownImage != null)
            {
                cooldownImage.fillAmount = Mathf.Clamp01(1f - (currentTimer / cooldownDuration));
            }

            if (cooldownText != null)
            {
                if (currentTimer >= cooldownDuration)
                {
                    cooldownText.text = "READY";
                    cooldownText.color = new Color(0f, 1f, 0.2f, 1f); // Vibrant green when ready
                    if (btn != null) btn.image.color = Color.white;
                }
                else
                {
                    float remaining = cooldownDuration - currentTimer;
                    cooldownText.text = $"{remaining:f1}";
                    cooldownText.color = new Color(1f, 0.3f, 0.3f, 1f); // Vibrant red/pink during cooldown
                    if (btn != null) btn.image.color = new Color(0.5f, 0.5f, 0.5f, 1f); // Dimmed button background
                }
            }
        }

        private Sprite CreateProceduralSkillIcon()
        {
            Texture2D tex = new Texture2D(32, 32);
            for (int x = 0; x < 32; x++)
            {
                for (int y = 0; y < 32; y++)
                {
                    float dist = Vector2.Distance(new Vector2(x, y), new Vector2(16, 16));
                    if (dist < 14f && dist > 11f)
                    {
                        tex.SetPixel(x, y, new Color(0.95f, 0.75f, 0.1f, 1f)); // Golden outer ring
                    }
                    else if (dist <= 11f)
                    {
                        if (Mathf.Abs(x - 16) < 3 || Mathf.Abs(y - 16) < 3)
                        {
                            tex.SetPixel(x, y, new Color(0.95f, 0.2f, 0.2f, 1f)); // Red cross/burst
                        }
                        else
                        {
                            tex.SetPixel(x, y, new Color(0.2f, 0.2f, 0.2f, 0.8f)); // Dark background
                        }
                    }
                    else
                    {
                        tex.SetPixel(x, y, Color.clear);
                    }
                }
            }
            tex.Apply();
            return Sprite.Create(tex, new Rect(0, 0, 32, 32), new Vector2(0.5f, 0.5f));
        }
    }
}

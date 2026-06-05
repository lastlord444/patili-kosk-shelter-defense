using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vampire
{
    public class PistolAbility : GunAbility
    {
        [Header("Pistol Settings")]
        [SerializeField] protected float targetRadius = 5f;

        public override string Name
        {
            get
            {
                if (!owned) return "Pistol";
                switch (level)
                {
                    case 1: return "Tabanca Hasari+";
                    case 2: return "Tabanca Atis Hizi+";
                    case 3: return "Tabanca Hasari+";
                    case 4: return "Tabanca Mermi Hizi+";
                    case 5: return "Tabanca Cift Atis+";
                    default: return "Pistol Upgrade+";
                }
            }
        }

        public override string Description
        {
            get
            {
                if (!owned)
                    return "En yakin canavari otomatik olarak hedefler ve ates eder.";
                switch (level)
                {
                    case 1: return "Tabanca hasari %25 artar.";
                    case 2: return "Tabanca %15 daha hizli ates eder.";
                    case 3: return "Tabanca hasari %25 artar.";
                    case 4: return "Tabanca mermileri %20 daha hizli gider.";
                    case 5: return "Fazladan 1 mermi atesler ve hasari %30 artar.";
                    default: return "Tabanca nitelikleri gelistirilir.";
                }
            }
        }

        protected GameObject pistolVisual;
        protected Vector3 gunDirection = Vector2.right;
        protected float theta = 0f;
        protected Vector3 initialGunScale = new Vector3(0.8f, 0.8f, 1f); // default scale for procedural gun

        private Sprite whitePixelSprite;

        public override void Init(AbilityManager abilityManager, EntityManager entityManager, Character playerCharacter)
        {
            ConfigurePistolUpgrades();
            base.Init(abilityManager, entityManager, playerCharacter);
        }

        private void ConfigurePistolUpgrades()
        {
            damage.ConfigureRuntimeUpgrades(new float[] { 0.25f, 0f, 0.25f, 0f, 0.30f });
            cooldown.ConfigureRuntimeUpgrades(new float[] { 0f, -0.15f, 0f, 0f, 0f });
            speed.ConfigureRuntimeUpgrades(new float[] { 0f, 0f, 0f, 0.20f, 0f });
            projectileCount.ConfigureRuntimeUpgrades(new int[] { 0, 0, 0, 0, 1 });
        }

        protected override void Use()
        {
            base.Use();
            CreateProceduralVisual();
        }

        protected void CreateProceduralVisual()
        {
            if (pistolVisual != null) return;

            // Generate 2x2 white sprite in memory if not already created
            if (whitePixelSprite == null)
            {
                Texture2D texture = new Texture2D(2, 2);
                for (int x = 0; x < 2; x++)
                {
                    for (int y = 0; y < 2; y++)
                    {
                        texture.SetPixel(x, y, Color.white);
                    }
                }
                texture.Apply();
                // Set pixelsPerUnit to 2 so the sprite base size is exactly 1 unit in world space
                whitePixelSprite = Sprite.Create(texture, new Rect(0, 0, 2, 2), new Vector2(0.5f, 0.5f), 2f);
            }

            // Create PistolVisual parent
            pistolVisual = new GameObject("PistolVisual");
            pistolVisual.transform.SetParent(transform, false);

            // Create Body
            GameObject body = new GameObject("Body");
            body.transform.SetParent(pistolVisual.transform, false);
            SpriteRenderer bodySR = body.AddComponent<SpriteRenderer>();
            bodySR.sprite = whitePixelSprite;
            bodySR.color = new Color(0.25f, 0.25f, 0.25f, 1f); // sleek dark grey
            body.transform.localScale = new Vector3(0.40f, 0.12f, 1f);
            body.transform.localPosition = new Vector3(0.12f, 0.05f, 0f);

            // Create Grip
            GameObject grip = new GameObject("Grip");
            grip.transform.SetParent(pistolVisual.transform, false);
            SpriteRenderer gripSR = grip.AddComponent<SpriteRenderer>();
            gripSR.sprite = whitePixelSprite;
            gripSR.color = new Color(0.2f, 0.2f, 0.2f, 1f); // slightly darker grey
            grip.transform.localScale = new Vector3(0.12f, 0.20f, 1f);
            grip.transform.localPosition = new Vector3(0.02f, -0.05f, 0f);

            // Create Muzzle (for visual alignment of bullet spawn)
            GameObject muzzle = new GameObject("Muzzle");
            muzzle.transform.SetParent(pistolVisual.transform, false);
            muzzle.transform.localPosition = new Vector3(0.32f, 0.05f, 0f);

            // Copy player sorting order to ensure it renders above character
            if (playerCharacter != null)
            {
                SpriteRenderer playerSR = playerCharacter.GetComponentInChildren<SpriteRenderer>();
                if (playerSR != null)
                {
                    bodySR.sortingOrder = playerSR.sortingOrder + 10;
                    gripSR.sortingOrder = playerSR.sortingOrder + 10;
                }
            }
        }

        protected override void Update()
        {
            base.Update();

            if (pistolVisual == null)
            {
                CreateProceduralVisual();
            }

            if (pistolVisual == null) return;

            // Find closest Monster target
            Monster closestMonster = FindClosestMonster();

            if (closestMonster != null)
            {
                gunDirection = ((Vector2)closestMonster.CenterTransform.position - (Vector2)playerCharacter.CenterTransform.position).normalized;
            }
            else
            {
                gunDirection = playerCharacter.LookDirection;
            }

            // Calculate rotation angle
            float targetTheta = Vector2.SignedAngle(Vector2.right, gunDirection);
            theta = Mathf.MoveTowardsAngle(theta, targetTheta, 720f * Time.deltaTime);
            pistolVisual.transform.rotation = Quaternion.Euler(0, 0, theta);

            // Anchor close to player body
            pistolVisual.transform.position = playerCharacter.CenterTransform.position + gunDirection * 0.15f + Vector3.up * 0.05f;

            // Flip Y-scale to prevent upside-down gun visual when looking left
            float normTheta = Mathf.Repeat(theta + 180f, 360f) - 180f;
            float targetYScale = (Mathf.Abs(normTheta) > 90f) ? -initialGunScale.y : initialGunScale.y;
            pistolVisual.transform.localScale = new Vector3(initialGunScale.x, targetYScale, initialGunScale.z);
        }

        protected override void LaunchProjectile()
        {
            Vector3 spawnPos = playerCharacter.CenterTransform.position;
            if (pistolVisual != null)
            {
                Transform muzzle = pistolVisual.transform.Find("Muzzle");
                if (muzzle != null) spawnPos = muzzle.position;
            }

            Projectile projectile = entityManager.SpawnProjectile(projectileIndex, spawnPos, damage.Value, knockback.Value, speed.Value, monsterLayer);
            if (projectile != null)
            {
                projectile.OnHitDamageable.AddListener(playerCharacter.OnDealDamage.Invoke);
                projectile.Launch(gunDirection);
            }
        }

        private Monster FindClosestMonster()
        {
            if (entityManager == null || entityManager.Grid == null || playerCharacter == null) return null;

            List<ISpatialHashGridClient> nearby = entityManager.Grid.FindNearbyInRadius(playerCharacter.CenterTransform.position, targetRadius);
            
            Monster closestMonster = null;
            float minDistance = float.MaxValue;
            
            foreach (var client in nearby)
            {
                // Only target valid, active, living Monster objects (exclude Player, Shelter, Coins, etc.)
                if (client is Monster monster && monster.HP > 0)
                {
                    float dist = Vector2.Distance(monster.Position, playerCharacter.CenterTransform.position);
                    if (dist < minDistance)
                    {
                        minDistance = dist;
                        closestMonster = monster;
                    }
                }
            }
            return closestMonster;
        }
    }
}

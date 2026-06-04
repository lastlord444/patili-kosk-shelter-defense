using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vampire
{
    public class BazookaGunAbility : GunAbility
    {
        [Header("Bazooka Gun Stats")]
        [SerializeField] protected GameObject bazookaGun;
        [SerializeField] protected Transform launchTransform;
        [SerializeField] protected ParticleSystem launchParticles;
        [SerializeField] protected UpgradeableAOE explosionAOE;
        [SerializeField] protected Vector2 hoverOffset;
        [SerializeField] protected float targetRadius = 5;
        protected Vector2 currHoverOffset;
        protected Vector3 gunDirection = Vector2.right;
        protected float theta = 0;

        protected Vector3 initialGunScale;
        protected bool isFiring = false;
        protected bool isInitialScaleStored = false;

        protected override void Use()
        {
            base.Use();
            if (bazookaGun != null && !isInitialScaleStored)
            {
                initialGunScale = bazookaGun.transform.localScale * 0.75f;
                isInitialScaleStored = true;
            }
        }

        protected override void Update()
        {
            base.Update();

            if (bazookaGun == null) return;

            if (!isInitialScaleStored)
            {
                initialGunScale = bazookaGun.transform.localScale * 0.75f;
                isInitialScaleStored = true;
            }

            // Aim in player's look direction when not firing
            if (!isFiring)
            {
                float targetTheta = Vector2.SignedAngle(Vector2.right, playerCharacter.LookDirection);
                theta = Mathf.MoveTowardsAngle(theta, targetTheta, 720f * Time.deltaTime);
                bazookaGun.transform.rotation = Quaternion.Euler(0, 0, theta);
            }

            // Anchor bazooka close to the player body (held in hand)
            bazookaGun.transform.position = (Vector2)playerCharacter.CenterTransform.position + playerCharacter.LookDirection * 0.15f + Vector2.up * 0.05f;

            // Flip Y-scale when looking/aiming left so the gun is not head down
            float normTheta = Mathf.Repeat(theta + 180f, 360f) - 180f;
            float targetYScale = (Mathf.Abs(normTheta) > 90f) ? -initialGunScale.y : initialGunScale.y;
            bazookaGun.transform.localScale = new Vector3(initialGunScale.x, targetYScale, initialGunScale.z);
        }

        protected override void LaunchProjectile()
        {
            StartCoroutine(LaunchProjecileAnimation());
        }

        protected IEnumerator LaunchProjecileAnimation()
        {
            isFiring = true;
            if (bazookaGun != null && !isInitialScaleStored)
            {
                initialGunScale = bazookaGun.transform.localScale;
                isInitialScaleStored = true;
            }

            ISpatialHashGridClient targetEntity = entityManager.Grid.FindClosestInRadius(bazookaGun.transform.position, targetRadius);
            
            Vector2 launchDirection = targetEntity == null ? Random.insideUnitCircle.normalized : (targetEntity.Position - (Vector2)bazookaGun.transform.position).normalized;

            float targetTheta = Vector2.SignedAngle(Vector2.right, launchDirection);
            float initialTheta = theta;

            float t = 0;
            float tMax = 1/firerate.Value*0.45f;
            while (t < tMax)
            {
                float tScaled = t / tMax;
                if (targetEntity != null)
                {
                    launchDirection = (targetEntity.Position - (Vector2)bazookaGun.transform.position).normalized;
                    targetTheta = Vector2.SignedAngle(Vector2.right, launchDirection);
                }
                theta = Mathf.Lerp(initialTheta, targetTheta, EasingUtils.EaseOutBack(t));
                bazookaGun.transform.rotation = Quaternion.Euler(0, 0, theta);
                
                // Flip Y scale dynamically during rotation lerping
                float normTheta = Mathf.Repeat(theta + 180f, 360f) - 180f;
                float targetYScale = (Mathf.Abs(normTheta) > 90f) ? -initialGunScale.y : initialGunScale.y;
                bazookaGun.transform.localScale = new Vector3(initialGunScale.x, targetYScale, initialGunScale.z);

                t += Time.deltaTime;
                yield return null;
            }
            if (targetEntity != null)
            {
                t = 0;
                while (t < tMax)
                {
                    float tScaled = t / tMax;
                    launchDirection = (targetEntity.Position - (Vector2)bazookaGun.transform.position).normalized;
                    targetTheta = Vector2.SignedAngle(Vector2.right, launchDirection);
                    bazookaGun.transform.rotation = Quaternion.Euler(0, 0, targetTheta);

                    // Flip Y scale dynamically during targeting tracking
                    float normTheta = Mathf.Repeat(targetTheta + 180f, 360f) - 180f;
                    float targetYScale = (Mathf.Abs(normTheta) > 90f) ? -initialGunScale.y : initialGunScale.y;
                    bazookaGun.transform.localScale = new Vector3(initialGunScale.x, targetYScale, initialGunScale.z);

                    t += Time.deltaTime;
                    yield return null;
                }
            }
            else
            {
                yield return new WaitForSeconds(tMax);
            }
            theta = targetTheta;
            bazookaGun.transform.rotation = Quaternion.Euler(0, 0, theta);

            // Final Y flip after target calculation
            float finalNormTheta = Mathf.Repeat(theta + 180f, 360f) - 180f;
            float finalTargetYScale = (Mathf.Abs(finalNormTheta) > 90f) ? -initialGunScale.y : initialGunScale.y;
            bazookaGun.transform.localScale = new Vector3(initialGunScale.x, finalTargetYScale, initialGunScale.z);

            ExplosiveProjectile projectile = (ExplosiveProjectile) entityManager.SpawnProjectile(projectileIndex, launchTransform.position, damage.Value, knockback.Value, speed.Value, monsterLayer);
            projectile.SetupExplosion(damage.Value, explosionAOE.Value, knockback.Value);
            projectile.OnHitDamageable.AddListener(playerCharacter.OnDealDamage.Invoke);
            projectile.Launch(launchDirection);
            launchParticles.Play();

            isFiring = false;
        }
    }
}

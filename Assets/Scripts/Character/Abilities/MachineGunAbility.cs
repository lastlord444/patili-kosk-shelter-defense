using System.Collections;
using UnityEngine;

namespace Vampire
{
    public class MachineGunAbility : GunAbility
    {
        [Header("Machine Gun Stats")]
        [SerializeField] protected GameObject machineGun;
        [SerializeField] protected Transform launchTransform;
        [SerializeField] protected UpgradeableRotationSpeed rotationSpeed;
        [SerializeField] protected float gunRadius;
        protected Vector3 gunDirection = Vector2.right;

        protected Vector3 initialGunScale;
        protected bool isInitialScaleStored = false;

        protected override void Use()
        {
            base.Use();
            if (machineGun != null && !isInitialScaleStored)
            {
                initialGunScale = machineGun.transform.localScale;
                isInitialScaleStored = true;
            }
        }

        protected override void Update()
        {
            base.Update();

            if (machineGun == null) return;

            if (!isInitialScaleStored)
            {
                initialGunScale = machineGun.transform.localScale;
                isInitialScaleStored = true;
            }

            // Rotate the gun if it is reloading
            float reloadRotation = 0;
            float t = timeSinceLastAttack/cooldown.Value;
            if (t > 0 && t < 1)
            {
                reloadRotation = t * 360;
            }

            float theta = Time.time*rotationSpeed.Value;
            gunDirection = new Vector3(Mathf.Cos(theta), Mathf.Sin(theta), 0);

            // Anchor close to player body (instead of orbiting far away)
            machineGun.transform.position = playerCharacter.CenterTransform.position + gunDirection * 0.15f + Vector3.up * 0.05f;

            float angleDeg = Mathf.Rad2Deg * theta;
            machineGun.transform.rotation = Quaternion.Euler(0, 0, angleDeg - reloadRotation);

            // Flip Y-scale when aiming left so the gun is not upside down
            float normTheta = Mathf.Repeat(angleDeg + 180f, 360f) - 180f;
            float targetYScale = (Mathf.Abs(normTheta) > 90f) ? -initialGunScale.y : initialGunScale.y;
            machineGun.transform.localScale = new Vector3(initialGunScale.x, targetYScale, initialGunScale.z);
        }

        protected override void LaunchProjectile()
        {
            Projectile projectile = entityManager.SpawnProjectile(projectileIndex, launchTransform.position, damage.Value, knockback.Value, speed.Value, monsterLayer);
            projectile.OnHitDamageable.AddListener(playerCharacter.OnDealDamage.Invoke);
            projectile.Launch(gunDirection);
        }
    }
}

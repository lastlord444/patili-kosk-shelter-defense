using UnityEngine;

namespace Vampire
{
    public abstract class IDamageable : MonoBehaviour
    {
        public abstract void TakeDamage(float damage, Vector2 knockback = default(Vector2), string damageSource = "enemy_attack");
        public abstract void Knockback(Vector2 knockback);
    }
}

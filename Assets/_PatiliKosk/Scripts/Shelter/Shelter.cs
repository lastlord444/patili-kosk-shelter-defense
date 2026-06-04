using UnityEngine;
using UnityEngine.Events;
using Vampire;

namespace PatiliKosk
{
    public class Shelter : IDamageable
    {
        [SerializeField]
        [Tooltip("Maximum health of the shelter.")]
        private float maxHealth = 100f;

        [SerializeField]
        [Tooltip("Current health of the shelter.")]
        private float currentHealth;

        public float MaxHealth => maxHealth;
        public float CurrentHealth => currentHealth;
        public bool IsDestroyed => currentHealth <= 0f;

        public UnityEvent OnDeath { get; } = new UnityEvent();

        private void Awake()
        {
            InitializeHealth();
        }

        private void OnValidate()
        {
            maxHealth = Mathf.Max(1f, maxHealth);
            currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        }

        /// <summary>
        /// Initializes the shelter's health to its maximum value.
        /// </summary>
        public void InitializeHealth()
        {
            currentHealth = maxHealth;
        }

        /// <summary>
        /// Applies damage to the shelter.
        /// </summary>
        /// <param name="amount">The amount of damage to apply. Must be positive.</param>
        /// <param name="knockback">Optional knockback vector (ignored for shelter).</param>
        public override void TakeDamage(float amount, Vector2 knockback = default(Vector2))
        {
            if (amount <= 0f || IsDestroyed) return;

            currentHealth -= amount;
            currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

            if (IsDestroyed)
            {
                OnDeath.Invoke();
            }
        }

        /// <summary>
        /// Apply knockback (no-op for static shelter).
        /// </summary>
        public override void Knockback(Vector2 knockback)
        {
            // Shelter is static, no knockback applied.
        }

        /// <summary>
        /// Heals the shelter.
        /// </summary>
        /// <param name="amount">The amount to heal. Must be positive.</param>
        public void Heal(float amount)
        {
            if (amount <= 0f || IsDestroyed) return;

            currentHealth += amount;
            currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        }
    }
}

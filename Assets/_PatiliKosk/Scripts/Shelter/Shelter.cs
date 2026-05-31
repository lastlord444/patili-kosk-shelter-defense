using UnityEngine;

namespace PatiliKosk
{
    public class Shelter : MonoBehaviour
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
        public void TakeDamage(float amount)
        {
            if (amount <= 0f || IsDestroyed) return;

            currentHealth -= amount;
            currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

            if (IsDestroyed)
            {
                // TODO: Trigger game over sequence or destruction event in future PR
            }
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

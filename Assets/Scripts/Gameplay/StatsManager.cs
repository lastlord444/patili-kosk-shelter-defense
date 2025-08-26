using UnityEngine;
using TMPro;

namespace Vampire
{
    public class StatsManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI monstersKilledText;
        [SerializeField] private TextMeshProUGUI coinsGainedText;

        private int monstersKilled = 0;
        private float damageDealt = 0;
        private float damageTaken = 0;
        private int coinsGained = 0;

        public int MonstersKilled { get => monstersKilled; }
        public float DamageDealt { get => damageDealt; }
        public float DamageTaken { get => damageTaken; }
        public int CoinsGained { get => coinsGained; }

        public void IncrementMonstersKilled()
        {
            monstersKilled++;
            monstersKilledText.text = monstersKilled.ToString();
            
            // Track performance milestone
            if (EventTracker.Instance != null)
            {
                EventTracker.Instance.TrackPerformance("monsters_killed", monstersKilled, "count");
            }
        }

        public void IncreaseCoinsGained(int amount)
        {
            coinsGained += amount;
            coinsGainedText.text = coinsGained.ToString();
            
            // Track resource collection event
            if (EventTracker.Instance != null)
            {
                EventTracker.Instance.TrackResourceCollected("Coins", amount, "level_progression");
            }
        }

        public void IncreaseDamageDealt(float damage)
        {
            damageDealt += damage; 
        }

        public void IncreaseDamageTaken(float damage)
        {
            damageTaken += damage;
        }
    }
}

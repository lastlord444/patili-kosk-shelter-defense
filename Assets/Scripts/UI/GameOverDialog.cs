using UnityEngine;
using TMPro;
using UnityEngine.Localization;
using System.Collections.Generic;

namespace Vampire
{
    public class GameOverDialog : DialogBox
    {
        [SerializeField] private TextMeshProUGUI statusText;
        [SerializeField] private TextMeshProUGUI coinsGained;
        [SerializeField] private TextMeshProUGUI enemiesRouted;
        [SerializeField] private TextMeshProUGUI damageDealt;
        [SerializeField] private TextMeshProUGUI damageTaken;
        [SerializeField] private GameObject background;
        [SerializeField] private LocalizedString levelPassedLocalization, levelLostLocalization;

        public void Open(bool levelPassed, StatsManager statsManager)
        {
            statusText.text = levelPassed ? levelPassedLocalization.GetLocalizedString() : levelLostLocalization.GetLocalizedString();
            coinsGained.text = "+" + statsManager.CoinsGained;
            enemiesRouted.text = statsManager.MonstersKilled.ToString();
            damageDealt.text = statsManager.DamageDealt.ToString();
            damageTaken.text = statsManager.DamageTaken.ToString();
            background.SetActive(true);
            
            // Track game over event
            if (EventTracker.Instance != null)
            {
                EventTracker.Instance.TrackEventSafely("game_over", new Dictionary<string, object>
                {
                    ["level_passed"] = levelPassed,
                    ["coins_gained"] = statsManager.CoinsGained,
                    ["enemies_killed"] = statsManager.MonstersKilled,
                    ["damage_dealt"] = statsManager.DamageDealt,
                    ["damage_taken"] = statsManager.DamageTaken,
                    ["timestamp"] = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                });
            }
            
            base.Open();
        }

        public override void Close()
        {
            base.Close();
            background.SetActive(false);
        }
    }
}

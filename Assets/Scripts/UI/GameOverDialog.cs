using UnityEngine;
using TMPro;
using UnityEngine.Localization;

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
            statusText.text = levelPassed ? "Barinak Korundu!" : levelLostLocalization.GetLocalizedString();
            coinsGained.text = "+" + statsManager.CoinsGained;
            enemiesRouted.text = statsManager.MonstersKilled.ToString();
            damageDealt.text = statsManager.DamageDealt.ToString();
            damageTaken.text = statsManager.DamageTaken.ToString();
            background.SetActive(true);

            // Find buttons dynamically
            Transform tryAgainBtn = null;
            Transform payContinueBtn = null;

            foreach (var child in GetComponentsInChildren<Transform>(true))
            {
                if (child.name == "再次挑戰") tryAgainBtn = child;
                else if (child.name == "付費繼續") payContinueBtn = child;
            }

            if (tryAgainBtn != null)
            {
                var txt = tryAgainBtn.GetComponentInChildren<TextMeshProUGUI>();
                if (txt != null)
                {
                    var localizer = txt.GetComponent<UnityEngine.Localization.Components.LocalizeStringEvent>();
                    if (localizer != null) localizer.enabled = false;
                    
                    txt.text = levelPassed ? "Continue" : "Try Again";
                }

                var buttonComp = tryAgainBtn.GetComponent<UnityEngine.UI.Button>();
                if (buttonComp != null && levelPassed)
                {
                    buttonComp.onClick.RemoveAllListeners();
                    var lm = FindFirstObjectByType<LevelManager>();
                    if (lm != null)
                    {
                        buttonComp.onClick.AddListener(lm.ReturnToMainMenu);
                    }
                }
            }

            if (payContinueBtn != null)
            {
                payContinueBtn.gameObject.SetActive(!levelPassed);
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

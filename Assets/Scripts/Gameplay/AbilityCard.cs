using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Vampire
{
    public class AbilityCard : MonoBehaviour
    {
        [SerializeField] private Image abilityImage;
        [SerializeField] private RectTransform abilityImageRect;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private TextMeshProUGUI buttonText;
        [SerializeField] private float appearSpeed = 3;
        [SerializeField] private LocalizedString selectLocalization, upgradeLocalization;
        private AbilitySelectionDialog levelUpMenu;
        private Ability ability;
        private bool initialized;

        private void OnEnable()
        {
            LocalizationSettings.SelectedLocaleChanged += HandleLocaleChanged;
        }
        
        private void OnDisable()
        {
            LocalizationSettings.SelectedLocaleChanged -= HandleLocaleChanged;
        }

        private void HandleLocaleChanged(Locale _)
        {
            SetText();
        }

        private void SetText()
        {
            if (!initialized) return;
            
            string name = ability.Name;
            string description = ability.Description;

            // Customize names for Level 1 / Pistol loadout to make them pistol-specific and clear in Turkish
            Character player = FindFirstObjectByType<Character>();
            bool hasPistol = player != null && player.GetComponentInChildren<PistolAbility>() != null;
            if (hasPistol)
            {
                if (ability is DamageUpgradeAbility)
                {
                    name = "Tabanca Hasari+";
                    description = "Tabanca hasari %10 artar.";
                }
                else if (ability is CooldownUpgradeAbility)
                {
                    name = "Tabanca Atis Hizi+";
                    description = "Tabanca %10 daha hizli ates eder.";
                }
                else if (ability is ProjectileSpeedAbilityUpgrade)
                {
                    name = "Tabanca Mermi Hizi+";
                    description = "Tabanca mermileri %10 daha hizli gider.";
                }
                else if (ability is ProjectileCountUpgradeAbility)
                {
                    name = "Tabanca Cift Atis+";
                    description = "Tabanca fazladan 1 mermi atar.";
                }
                else if (ability is ArmorUpgradeAbility)
                {
                    name = "Zirh+";
                    description = "Alinan hasari 1 azaltir.";
                }
                else if (ability is IceSkatesAbility)
                {
                    name = "Hareket Hizi+";
                    description = "Karakter hareket hizi %10 artar.";
                }
            }

            nameText.text = name;
            descriptionText.text = description;
            
            buttonText.text = !ability.Owned ? selectLocalization.GetLocalizedString() : upgradeLocalization.GetLocalizedString() + " (" + ability.Level + " -> " + (ability.Level+1) + ")";
        }

        public void Init(AbilitySelectionDialog levelUpMenu, Ability ability, float waitToAppear)
        {
            abilityImage.sprite = ability.Image;
            float yHeight = abilityImageRect.rect.height;
            float xWidth = ability.Image.textureRect.width / (float) ability.Image.textureRect.height * yHeight;
            if (xWidth > abilityImageRect.rect.width)
            {
                    xWidth = abilityImageRect.rect.width;
                    yHeight = ability.Image.textureRect.height / (float) ability.Image.textureRect.width * xWidth;
            }
            ((RectTransform)abilityImage.transform).sizeDelta = new Vector2(xWidth, yHeight);
            this.levelUpMenu = levelUpMenu;
            this.ability = ability;
            StartCoroutine(Appear(waitToAppear));
            initialized = true;
            SetText();
        }

        public IEnumerator Appear(float waitToAppear)
        {
            Vector3 initialScale = transform.localScale;
            transform.localScale = Vector3.zero;
            yield return new WaitForSecondsRealtime(waitToAppear);
            float t = 0;
            while (t < 1)
            {
                transform.localScale = Vector3.LerpUnclamped(Vector3.zero, initialScale, EasingUtils.EaseOutBack(t));
                t += Time.unscaledDeltaTime * appearSpeed;
                yield return null;
            }
            transform.localScale = initialScale;
        }

        public void Selected()
        {
            ability.Select();
            levelUpMenu.Close();
        }
    }
}

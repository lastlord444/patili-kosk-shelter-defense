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

            // Customize names and descriptions for Level 1 to be strictly pistol-specific in Turkish (no Turkish chars)
            bool isLevel1 = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Level 1" || UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 1;
            if (isLevel1)
            {
                if (ability is DamageUpgradeAbility)
                {
                    name = "Tabanca Hasari+";
                    description = "Tabanca hasarini artirir.";
                }
                else if (ability is CooldownUpgradeAbility)
                {
                    name = "Tabanca Atis Hizi+";
                    description = "Tabanca daha hizli ates eder.";
                }
                else if (ability is ProjectileSpeedAbilityUpgrade)
                {
                    name = "Tabanca Mermi Hizi+";
                    description = "Tabanca mermileri daha hizli gider.";
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

        private Sprite CreateProceduralUpgradeIcon(string type)
        {
            int width = 32;
            int height = 32;
            Texture2D tex = new Texture2D(width, height, TextureFormat.RGBA32, false);
            
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    tex.SetPixel(x, y, Color.clear);
                }
            }
            
            Color gripColor = new Color(0.45f, 0.25f, 0.1f, 1f); // Brown
            Color metalColor = new Color(0.3f, 0.3f, 0.35f, 1f); // Grey metal
            Color metalHighlight = new Color(0.5f, 0.5f, 0.55f, 1f); // Highlight
            
            // Grip
            for (int y = 8; y <= 13; y++)
            {
                for (int x = 8; x <= 10; x++)
                {
                    tex.SetPixel(x, y, gripColor);
                }
            }
            // Barrel
            for (int y = 14; y <= 17; y++)
            {
                for (int x = 6; x <= 18; x++)
                {
                    tex.SetPixel(x, y, (y == 17) ? metalHighlight : metalColor);
                }
            }
            // Trigger guard
            tex.SetPixel(11, 12, metalHighlight);
            tex.SetPixel(11, 11, metalColor);
            tex.SetPixel(12, 12, metalColor);

            if (type == "Damage")
            {
                // Tabanca Hasari+: Bullet impact explosion burst
                Color burstColor = new Color(0.95f, 0.2f, 0.2f, 1f);
                Color burstInner = new Color(0.95f, 0.75f, 0.1f, 1f);
                int centerX = 24;
                int centerY = 15;
                for (int x = 20; x <= 28; x++)
                {
                    for (int y = 11; y <= 19; y++)
                    {
                        float dist = Vector2.Distance(new Vector2(x, y), new Vector2(centerX, centerY));
                        if (dist < 2.2f)
                        {
                            tex.SetPixel(x, y, burstInner);
                        }
                        else if (dist < 4.2f)
                        {
                            if (x == centerX || y == centerY || Mathf.Abs(x - centerX) == Mathf.Abs(y - centerY))
                            {
                                tex.SetPixel(x, y, burstColor);
                            }
                        }
                    }
                }
            }
            else if (type == "Cooldown")
            {
                // Tabanca Atis Hizi+: Clock timer icon
                Color clockColor = new Color(0.95f, 0.75f, 0.1f, 1f);
                Color handColor = new Color(0.2f, 0.2f, 0.2f, 1f);
                int centerX = 23;
                int centerY = 22;
                for (int x = 18; x <= 28; x++)
                {
                    for (int y = 17; y <= 27; y++)
                    {
                        float dist = Vector2.Distance(new Vector2(x, y), new Vector2(centerX, centerY));
                        if (Mathf.Abs(dist - 4.5f) < 0.8f)
                        {
                            tex.SetPixel(x, y, clockColor);
                        }
                    }
                }
                tex.SetPixel(centerX, centerY, handColor);
                tex.SetPixel(centerX, centerY + 1, handColor);
                tex.SetPixel(centerX, centerY + 2, handColor);
                tex.SetPixel(centerX + 1, centerY, handColor);
                tex.SetPixel(centerX + 2, centerY, handColor);
            }
            else if (type == "Speed")
            {
                // Tabanca Mermi Hizi+: Flying bullet with motion streaks
                Color bulletColor = new Color(0.95f, 0.75f, 0.1f, 1f);
                Color streakColor = new Color(0.2f, 0.6f, 0.95f, 0.8f);
                tex.SetPixel(25, 15, bulletColor);
                tex.SetPixel(26, 15, bulletColor);
                tex.SetPixel(27, 15, bulletColor);
                tex.SetPixel(26, 16, bulletColor);
                tex.SetPixel(26, 14, bulletColor);
                for (int x = 19; x <= 23; x++)
                {
                    tex.SetPixel(x, 17, streakColor);
                    tex.SetPixel(x + 1, 13, streakColor);
                }
                for (int x = 17; x <= 22; x++)
                {
                    tex.SetPixel(x, 15, streakColor);
                }
            }
            
            tex.Apply();
            return Sprite.Create(tex, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f));
        }

        public void Init(AbilitySelectionDialog levelUpMenu, Ability ability, float waitToAppear)
        {
            this.levelUpMenu = levelUpMenu;
            this.ability = ability;
            initialized = true;

            Sprite cardSprite = ability.Image;
            bool isLevel1 = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Level 1" || UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 1;
            if (isLevel1)
            {
                if (ability is DamageUpgradeAbility)
                {
                    cardSprite = CreateProceduralUpgradeIcon("Damage");
                }
                else if (ability is CooldownUpgradeAbility)
                {
                    cardSprite = CreateProceduralUpgradeIcon("Cooldown");
                }
                else if (ability is ProjectileSpeedAbilityUpgrade)
                {
                    cardSprite = CreateProceduralUpgradeIcon("Speed");
                }
            }

            abilityImage.sprite = cardSprite;
            
            float yHeight = abilityImageRect.rect.height;
            float xWidth = cardSprite.textureRect.width / (float) cardSprite.textureRect.height * yHeight;
            if (xWidth > abilityImageRect.rect.width)
            {
                xWidth = abilityImageRect.rect.width;
                yHeight = cardSprite.textureRect.height / (float) cardSprite.textureRect.width * xWidth;
            }
            ((RectTransform)abilityImage.transform).sizeDelta = new Vector2(xWidth, yHeight);
            
            StartCoroutine(Appear(waitToAppear));
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

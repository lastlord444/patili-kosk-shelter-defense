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

        private SpriteRenderer rootSpriteRenderer;
        private GameObject shelterVisualObj;
        private SpriteRenderer visualSpriteRenderer;

        private GameObject healthBarGroup;
        private SpriteRenderer barBgRenderer;
        private SpriteRenderer barFillRenderer;
        private Transform barFillAnchor;
        private Sprite cachedWhiteSprite;

        private void Awake()
        {
            // Generate cached 1x1 white sprite once in Awake
            Texture2D tex = new Texture2D(1, 1);
            tex.SetPixel(0, 0, Color.white);
            tex.Apply();
            cachedWhiteSprite = Sprite.Create(tex, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f), 1f);

            InitializeHealth();
            CreateShelterVisual();
            CreateHealthBar();
        }

        private void CreateShelterVisual()
        {
            rootSpriteRenderer = GetComponent<SpriteRenderer>();
            if (rootSpriteRenderer != null)
            {
                shelterVisualObj = new GameObject("ShelterVisual");
                shelterVisualObj.transform.SetParent(transform, false);
                shelterVisualObj.transform.localPosition = Vector3.zero;
                shelterVisualObj.transform.localScale = Vector3.one;

                // 1. Body (Gövde) - Ahşap Kabin Kahverengisi
                GameObject bodyObj = new GameObject("Body");
                bodyObj.transform.SetParent(shelterVisualObj.transform, false);
                bodyObj.transform.localPosition = new Vector3(0f, 0f, 0f);
                bodyObj.transform.localScale = new Vector3(1.2f, 0.9f, 1f);
                var bodySR = bodyObj.AddComponent<SpriteRenderer>();
                bodySR.sprite = cachedWhiteSprite;
                bodySR.color = new Color(0.55f, 0.35f, 0.2f, 1f);
                bodySR.sortingLayerID = rootSpriteRenderer.sortingLayerID;
                bodySR.sortingLayerName = rootSpriteRenderer.sortingLayerName;
                bodySR.sortingOrder = rootSpriteRenderer.sortingOrder;

                // 2. Roof (Çatı) - Koyu Kiremit Kırmızısı/Kahvesi
                GameObject roofObj = new GameObject("Roof");
                roofObj.transform.SetParent(shelterVisualObj.transform, false);
                roofObj.transform.localPosition = new Vector3(0f, 0.525f, 0f);
                roofObj.transform.localScale = new Vector3(1.35f, 0.25f, 1f);
                var roofSR = roofObj.AddComponent<SpriteRenderer>();
                roofSR.sprite = cachedWhiteSprite;
                roofSR.color = new Color(0.35f, 0.15f, 0.1f, 1f);
                roofSR.sortingLayerID = rootSpriteRenderer.sortingLayerID;
                roofSR.sortingLayerName = rootSpriteRenderer.sortingLayerName;
                roofSR.sortingOrder = rootSpriteRenderer.sortingOrder + 1;

                // 3. Door (Kapı) - Koyu Ahşap Kahvesi
                GameObject doorObj = new GameObject("Door");
                doorObj.transform.SetParent(shelterVisualObj.transform, false);
                doorObj.transform.localPosition = new Vector3(0f, -0.25f, 0f);
                doorObj.transform.localScale = new Vector3(0.3f, 0.4f, 1f);
                var doorSR = doorObj.AddComponent<SpriteRenderer>();
                doorSR.sprite = cachedWhiteSprite;
                doorSR.color = new Color(0.25f, 0.15f, 0.08f, 1f);
                doorSR.sortingLayerID = rootSpriteRenderer.sortingLayerID;
                doorSR.sortingLayerName = rootSpriteRenderer.sortingLayerName;
                doorSR.sortingOrder = rootSpriteRenderer.sortingOrder + 2;

                // 4. Window (Pencere) - Işıklı Sıcak Sarı
                GameObject windowObj = new GameObject("Window");
                windowObj.transform.SetParent(shelterVisualObj.transform, false);
                windowObj.transform.localPosition = new Vector3(0.35f, 0.15f, 0f);
                windowObj.transform.localScale = new Vector3(0.2f, 0.2f, 1f);
                var windowSR = windowObj.AddComponent<SpriteRenderer>();
                windowSR.sprite = cachedWhiteSprite;
                windowSR.color = new Color(0.95f, 0.85f, 0.4f, 1f);
                windowSR.sortingLayerID = rootSpriteRenderer.sortingLayerID;
                windowSR.sortingLayerName = rootSpriteRenderer.sortingLayerName;
                windowSR.sortingOrder = rootSpriteRenderer.sortingOrder + 2;

                // 5. Window horizontal divider
                GameObject hLine = new GameObject("HLine");
                hLine.transform.SetParent(windowObj.transform, false);
                hLine.transform.localPosition = Vector3.zero;
                hLine.transform.localScale = new Vector3(1f, 0.15f, 1f);
                var hSR = hLine.AddComponent<SpriteRenderer>();
                hSR.sprite = cachedWhiteSprite;
                hSR.color = new Color(0.25f, 0.15f, 0.08f, 1f);
                hSR.sortingLayerID = rootSpriteRenderer.sortingLayerID;
                hSR.sortingLayerName = rootSpriteRenderer.sortingLayerName;
                hSR.sortingOrder = rootSpriteRenderer.sortingOrder + 3;

                // 6. Window vertical divider
                GameObject vLine = new GameObject("VLine");
                vLine.transform.SetParent(windowObj.transform, false);
                vLine.transform.localPosition = Vector3.zero;
                vLine.transform.localScale = new Vector3(0.15f, 1f, 1f);
                var vSR = vLine.AddComponent<SpriteRenderer>();
                vSR.sprite = cachedWhiteSprite;
                vSR.color = new Color(0.25f, 0.15f, 0.08f, 1f);
                vSR.sortingLayerID = rootSpriteRenderer.sortingLayerID;
                vSR.sortingLayerName = rootSpriteRenderer.sortingLayerName;
                vSR.sortingOrder = rootSpriteRenderer.sortingOrder + 3;

                visualSpriteRenderer = bodySR;
                rootSpriteRenderer.enabled = false;
            }
        }

        private void CreateHealthBar()
        {
            healthBarGroup = new GameObject("HealthBar");
            healthBarGroup.transform.SetParent(transform, false);
            healthBarGroup.transform.localPosition = new Vector3(0f, 0.85f, -0.1f);
            healthBarGroup.transform.localScale = Vector3.one;

            // Background
            GameObject bgObj = new GameObject("Background");
            bgObj.transform.SetParent(healthBarGroup.transform, false);
            bgObj.transform.localPosition = Vector3.zero;
            bgObj.transform.localScale = new Vector3(1.25f, 0.12f, 1f);
            barBgRenderer = bgObj.AddComponent<SpriteRenderer>();
            barBgRenderer.sprite = cachedWhiteSprite;
            barBgRenderer.color = new Color(0.1f, 0.1f, 0.1f, 0.9f);
            if (visualSpriteRenderer != null)
            {
                barBgRenderer.sortingLayerID = visualSpriteRenderer.sortingLayerID;
                barBgRenderer.sortingLayerName = visualSpriteRenderer.sortingLayerName;
                barBgRenderer.sortingOrder = visualSpriteRenderer.sortingOrder + 30;
            }

            // Fill Anchor
            GameObject anchorObj = new GameObject("FillAnchor");
            anchorObj.transform.SetParent(healthBarGroup.transform, false);
            anchorObj.transform.localPosition = new Vector3(-0.575f, 0f, -0.01f);
            barFillAnchor = anchorObj.transform;

            // Fill Child
            GameObject fillObj = new GameObject("Fill");
            fillObj.transform.SetParent(barFillAnchor, false);
            fillObj.transform.localPosition = new Vector3(0.575f, 0f, 0f);
            fillObj.transform.localScale = new Vector3(1.15f, 0.08f, 1f);
            barFillRenderer = fillObj.AddComponent<SpriteRenderer>();
            barFillRenderer.sprite = cachedWhiteSprite;
            barFillRenderer.color = new Color(0.15f, 0.75f, 0.15f, 1f);
            if (visualSpriteRenderer != null)
            {
                barFillRenderer.sortingLayerID = visualSpriteRenderer.sortingLayerID;
                barFillRenderer.sortingLayerName = visualSpriteRenderer.sortingLayerName;
                barFillRenderer.sortingOrder = visualSpriteRenderer.sortingOrder + 31;
            }

            UpdateHealthBar();
        }

        private void UpdateHealthBar()
        {
            if (barFillAnchor == null || barFillRenderer == null) return;
            float pct = Mathf.Clamp01(currentHealth / maxHealth);
            barFillAnchor.localScale = new Vector3(pct, 1f, 1f);

            if (pct >= 0.6f)
            {
                barFillRenderer.color = new Color(0.15f, 0.75f, 0.15f, 1f);
            }
            else if (pct >= 0.3f)
            {
                barFillRenderer.color = new Color(0.85f, 0.75f, 0.1f, 1f);
            }
            else
            {
                barFillRenderer.color = new Color(0.75f, 0.15f, 0.15f, 1f);
            }
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
            UpdateHealthBar();
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

            UpdateHealthBar();

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

            UpdateHealthBar();
        }
    }
}

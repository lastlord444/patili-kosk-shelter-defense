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
                shelterVisualObj.transform.localScale = new Vector3(2.5f, 2.5f, 1f);

                visualSpriteRenderer = shelterVisualObj.AddComponent<SpriteRenderer>();
                visualSpriteRenderer.sprite = rootSpriteRenderer.sprite;
                visualSpriteRenderer.color = new Color(0.85f, 0.65f, 0.5f, 1f); // Warm wood-cabin brown/tan color block
                visualSpriteRenderer.material = rootSpriteRenderer.material;
                visualSpriteRenderer.sortingLayerID = rootSpriteRenderer.sortingLayerID;
                visualSpriteRenderer.sortingLayerName = rootSpriteRenderer.sortingLayerName;
                visualSpriteRenderer.sortingOrder = rootSpriteRenderer.sortingOrder;

                rootSpriteRenderer.enabled = false;
            }
        }

        private void CreateHealthBar()
        {
            healthBarGroup = new GameObject("HealthBar");
            healthBarGroup.transform.SetParent(transform, false);
            healthBarGroup.transform.localPosition = new Vector3(0f, 1.5f, -0.1f);
            healthBarGroup.transform.localScale = Vector3.one;

            // Generate cached white sprite once
            Texture2D tex = new Texture2D(2, 2);
            Color[] colors = new Color[4];
            for (int i = 0; i < colors.Length; i++) colors[i] = Color.white;
            tex.SetPixels(colors);
            tex.Apply();
            cachedWhiteSprite = Sprite.Create(tex, new Rect(0, 0, 2, 2), new Vector2(0.5f, 0.5f));

            // Background
            GameObject bgObj = new GameObject("Background");
            bgObj.transform.SetParent(healthBarGroup.transform, false);
            bgObj.transform.localPosition = Vector3.zero;
            bgObj.transform.localScale = new Vector3(1.54f, 0.19f, 1f);
            barBgRenderer = bgObj.AddComponent<SpriteRenderer>();
            barBgRenderer.sprite = cachedWhiteSprite;
            barBgRenderer.color = Color.black;
            if (visualSpriteRenderer != null)
            {
                barBgRenderer.sortingLayerID = visualSpriteRenderer.sortingLayerID;
                barBgRenderer.sortingLayerName = visualSpriteRenderer.sortingLayerName;
                barBgRenderer.sortingOrder = visualSpriteRenderer.sortingOrder + 10;
            }

            // Fill Anchor
            GameObject anchorObj = new GameObject("FillAnchor");
            anchorObj.transform.SetParent(healthBarGroup.transform, false);
            anchorObj.transform.localPosition = new Vector3(-0.75f, 0f, -0.01f);
            barFillAnchor = anchorObj.transform;

            // Fill Child
            GameObject fillObj = new GameObject("Fill");
            fillObj.transform.SetParent(barFillAnchor, false);
            fillObj.transform.localPosition = new Vector3(0.75f, 0f, 0f);
            fillObj.transform.localScale = new Vector3(1.5f, 0.15f, 1f);
            barFillRenderer = fillObj.AddComponent<SpriteRenderer>();
            barFillRenderer.sprite = cachedWhiteSprite;
            barFillRenderer.color = Color.green;
            if (visualSpriteRenderer != null)
            {
                barFillRenderer.sortingLayerID = visualSpriteRenderer.sortingLayerID;
                barFillRenderer.sortingLayerName = visualSpriteRenderer.sortingLayerName;
                barFillRenderer.sortingOrder = visualSpriteRenderer.sortingOrder + 11;
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
                barFillRenderer.color = Color.green;
            }
            else if (pct >= 0.3f)
            {
                barFillRenderer.color = Color.yellow;
            }
            else
            {
                barFillRenderer.color = Color.red;
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

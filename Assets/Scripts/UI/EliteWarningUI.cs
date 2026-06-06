using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Vampire
{
    public class EliteWarningUI : MonoBehaviour
    {
        private CanvasGroup canvasGroup;
        private GameObject spawnedArrow = null;

        public static void CreateProcedural(string messageText, float duration)
        {
            Canvas canvas = FindFirstObjectByType<Canvas>();
            if (canvas == null)
            {
                Debug.LogWarning("[EliteWarningUI] Canvas not found in scene!");
                return;
            }

            // Create warning UI container
            GameObject warningGo = new GameObject("EliteWarningUI");
            warningGo.transform.SetParent(canvas.transform, false);
            
            EliteWarningUI warning = warningGo.AddComponent<EliteWarningUI>();
            warning.InitializeAndShow(duration, messageText);
        }

        public static void CreateProcedural(float duration)
        {
            CreateProcedural("ELIT DUSMAN YAKLASIYOR", duration);
        }

        public void ShowWarning(string text, float duration)
        {
            InitializeAndShow(duration, text);
        }

        private void InitializeAndShow(float duration, string messageText = "ELIT DUSMAN YAKLASIYOR")
        {
            // Set up RectTransform for top center alignment
            RectTransform rect = gameObject.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 1f);
            rect.anchorMax = new Vector2(0.5f, 1f);
            rect.pivot = new Vector2(0.5f, 1f);
            rect.anchoredPosition = new Vector2(0f, -120f); // Positioned below header HUD
            rect.sizeDelta = new Vector2(450f, 65f);

            // Add background Image (Sleek Dark Red Glassmorphism)
            Image bgImage = gameObject.AddComponent<Image>();
            bgImage.color = new Color(0.18f, 0.02f, 0.02f, 0.85f); // Elegant deep red/black

            // Add border outline panel
            GameObject borderGo = new GameObject("Border");
            borderGo.transform.SetParent(transform, false);
            RectTransform borderRect = borderGo.AddComponent<RectTransform>();
            borderRect.anchorMin = Vector2.zero;
            borderRect.anchorMax = Vector2.one;
            borderRect.sizeDelta = new Vector2(2f, 2f); // slight outline padding
            Image borderImage = borderGo.AddComponent<Image>();
            borderImage.color = new Color(0.85f, 0.15f, 0.15f, 0.6f); // soft glowing red border
            borderRect.SetAsFirstSibling();

            // Add Text
            GameObject textGo = new GameObject("Text");
            textGo.transform.SetParent(transform, false);
            RectTransform textRect = textGo.AddComponent<RectTransform>();
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.one;
            textRect.sizeDelta = Vector2.zero;

            TextMeshProUGUI tmpText = textGo.AddComponent<TextMeshProUGUI>();
            tmpText.text = messageText;
            tmpText.fontSize = 22;
            tmpText.alignment = TextAlignmentOptions.Center;
            tmpText.color = new Color(1f, 0.85f, 0.85f, 1f); // light pinkish white
            tmpText.fontStyle = FontStyles.Bold;

            // If message contains direction "SAG" or "RIGHT", spawn arrow at right side of the screen
            if (messageText.Contains("SAG") || messageText.Contains("RIGHT"))
            {
                Canvas canvas = FindFirstObjectByType<Canvas>();
                if (canvas != null)
                {
                    spawnedArrow = new GameObject("DirectionArrow");
                    spawnedArrow.transform.SetParent(canvas.transform, false);
                    RectTransform arrowRect = spawnedArrow.AddComponent<RectTransform>();
                    arrowRect.anchorMin = new Vector2(0.9f, 0.5f);
                    arrowRect.anchorMax = new Vector2(0.9f, 0.5f);
                    arrowRect.pivot = new Vector2(0.5f, 0.5f);
                    arrowRect.anchoredPosition = Vector2.zero;
                    arrowRect.sizeDelta = new Vector2(100f, 100f);

                    TextMeshProUGUI arrowText = spawnedArrow.AddComponent<TextMeshProUGUI>();
                    arrowText.text = "\u2794"; // ➔
                    arrowText.fontSize = 72;
                    arrowText.alignment = TextAlignmentOptions.Center;
                    arrowText.color = new Color(0.95f, 0.75f, 0.1f, 1f); // Warning Gold Yellow
                    arrowText.fontStyle = FontStyles.Bold;

                    var cg = spawnedArrow.AddComponent<CanvasGroup>();
                    cg.alpha = 0f;
                }
            }

            // Add CanvasGroup for fading
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
            canvasGroup.alpha = 0f;

            // Start fade in/out animation
            StartCoroutine(FadeSequence(duration));
        }

        private IEnumerator FadeSequence(float duration)
        {
            CanvasGroup arrowCg = spawnedArrow != null ? spawnedArrow.GetComponent<CanvasGroup>() : null;

            // Fade in (0.4s)
            float t = 0f;
            while (t < 0.4f)
            {
                t += Time.unscaledDeltaTime;
                float alpha = Mathf.Clamp01(t / 0.4f);
                canvasGroup.alpha = alpha;
                if (arrowCg != null) arrowCg.alpha = alpha;
                yield return null;
            }
            canvasGroup.alpha = 1f;
            if (arrowCg != null) arrowCg.alpha = 1f;

            // Wait and Flash (duration minus fade times)
            float waitDuration = Mathf.Max(0.1f, duration - 0.8f);
            float elapsed = 0f;
            while (elapsed < waitDuration)
            {
                elapsed += Time.unscaledDeltaTime;
                if (arrowCg != null)
                {
                    // Flash effect between 0.4 and 1.0 alpha
                    arrowCg.alpha = 0.4f + Mathf.PingPong(elapsed * 4f, 0.6f);
                }
                yield return null;
            }

            // Fade out (0.4s)
            t = 0f;
            while (t < 0.4f)
            {
                t += Time.unscaledDeltaTime;
                float alpha = Mathf.Clamp01(1f - (t / 0.4f));
                canvasGroup.alpha = alpha;
                if (arrowCg != null) arrowCg.alpha = alpha;
                yield return null;
            }
            canvasGroup.alpha = 0f;
            if (arrowCg != null) arrowCg.alpha = 0f;

            // Clean up
            if (spawnedArrow != null) Destroy(spawnedArrow);
            Destroy(gameObject);
        }
    }
}

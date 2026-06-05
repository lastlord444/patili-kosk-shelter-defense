using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Vampire
{
    public class EliteWarningUI : MonoBehaviour
    {
        private CanvasGroup canvasGroup;

        public static void CreateProcedural(float duration)
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
            warning.InitializeAndShow(duration);
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

            // Add CanvasGroup for fading
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
            canvasGroup.alpha = 0f;

            // Start fade in/out animation
            StartCoroutine(FadeSequence(duration));
        }

        private IEnumerator FadeSequence(float duration)
        {
            // Fade in (0.4s)
            float t = 0f;
            while (t < 0.4f)
            {
                t += Time.unscaledDeltaTime;
                canvasGroup.alpha = Mathf.Clamp01(t / 0.4f);
                yield return null;
            }
            canvasGroup.alpha = 1f;

            // Wait (duration minus fade times)
            yield return new WaitForSecondsRealtime(Mathf.Max(0.1f, duration - 0.8f));

            // Fade out (0.4s)
            t = 0f;
            while (t < 0.4f)
            {
                t += Time.unscaledDeltaTime;
                canvasGroup.alpha = Mathf.Clamp01(1f - (t / 0.4f));
                yield return null;
            }
            canvasGroup.alpha = 0f;

            // Clean up
            Destroy(gameObject);
        }
    }
}

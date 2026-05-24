using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsSlideMenu : MonoBehaviour
{
    public Button settingsButton;                      // The main Settings button
    public RectTransform[] optionButtons;              // Buttons: Music, Sound, Info
    public float slideDistance = 100f;                 // Distance to slide
    public float slideDuration = 0.3f;                 // How long the slide takes
    public float staggerDelay = 0.1f;                  // Delay between each button animation

    private bool isOpen = false;
    private Vector2[] originalPositions;               // Store default positions of each button

    void Start()
    {
        // Store original positions and move offscreen
        originalPositions = new Vector2[optionButtons.Length];

        for (int i = 0; i < optionButtons.Length; i++)
        {
            originalPositions[i] = optionButtons[i].anchoredPosition;
            optionButtons[i].anchoredPosition = originalPositions[i] - new Vector2(0, slideDistance);
        }

        // Attach the Toggle method to the settings button
        settingsButton.onClick.AddListener(ToggleSettings);
    }

    public void ToggleSettings()
    {
        isOpen = !isOpen;
        StopAllCoroutines();  // Cancel ongoing animations

        if (isOpen)
        {
            // Slide IN with staggered delay
            for (int i = 0; i < optionButtons.Length; i++)
            {
                StartCoroutine(SlideButton(
                    optionButtons[i],
                    originalPositions[i],
                    slideDuration,
                    i * staggerDelay
                ));
            }
        }
        else
        {
            // Slide OUT in reverse order with staggered delay
            for (int i = optionButtons.Length - 1; i >= 0; i--)
            {
                StartCoroutine(SlideButton(
                    optionButtons[i],
                    originalPositions[i] - new Vector2(0, slideDistance),
                    slideDuration,
                    (optionButtons.Length - 1 - i) * staggerDelay
                ));
            }
        }
    }

    private IEnumerator SlideButton(RectTransform button, Vector2 targetPos, float duration, float delay)
    {
        yield return new WaitForSecondsRealtime(delay);

        Vector2 startPos = button.anchoredPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            button.anchoredPosition = Vector2.Lerp(startPos, targetPos, t);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        button.anchoredPosition = targetPos;
    }
}
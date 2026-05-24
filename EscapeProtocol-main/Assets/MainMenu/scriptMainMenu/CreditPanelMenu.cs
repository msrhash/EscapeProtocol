using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class CreditPanelMenu : MonoBehaviour
{
    [Header("Credits Panel Animation")]
    public RectTransform creditsPanel;
    public float slideDuration = 0.5f;
    public Vector2 onScreenPosition = Vector2.zero;       // Visible position
    public Vector2 offScreenPosition = new Vector2(0, -1080); // Hidden position

    [Header("Floating Effect")]
    public bool enableFloating = true;
    public float floatAmplitude = 6f;    // how far it moves
    public float floatSpeed = 1f;         // how fast it floats

    private bool isCreditsOpen = false;
    private Coroutine floatingCoroutine;

    void Start()
    {
        // Start hidden
        creditsPanel.anchoredPosition = offScreenPosition;
        creditsPanel.gameObject.SetActive(false);
    }

    public void ToggleCredits()
    {
        StopAllCoroutines();  // stop sliding and floating
        if (floatingCoroutine != null) floatingCoroutine = null;

        isCreditsOpen = !isCreditsOpen;
        creditsPanel.gameObject.SetActive(true);

        if (isCreditsOpen)
        {
            StartCoroutine(SlidePanel(creditsPanel, onScreenPosition, false, true)); // start floating after slide in
        }
        else
        {
            StartCoroutine(SlidePanel(creditsPanel, offScreenPosition, true, false)); // stop floating on slide out
        }
    }

    private IEnumerator SlidePanel(RectTransform panel, Vector2 target, bool hideAfter, bool startFloating = false)
    {
        Vector2 startPos = panel.anchoredPosition;
        float elapsed = 0f;

        while (elapsed < slideDuration)
        {
            float t = elapsed / slideDuration;
            panel.anchoredPosition = Vector2.Lerp(startPos, target, t);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        panel.anchoredPosition = target;

        if (hideAfter)
        {
            panel.gameObject.SetActive(false);
        }
        else if (startFloating && enableFloating)
        {
            floatingCoroutine = StartCoroutine(FloatEffect());
        }
    }

    private IEnumerator FloatEffect()
    {
        Vector2 basePos = creditsPanel.anchoredPosition;

        while (true)
        {
            float yOffset = Mathf.Sin(Time.unscaledTime * floatSpeed) * floatAmplitude;
            creditsPanel.anchoredPosition = basePos + new Vector2(0, yOffset);
            yield return null;
        }
    }
}

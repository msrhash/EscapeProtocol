using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [Header("Settings Panel & Animation")]
    public GameObject settingsPanel;
    public RectTransform[] optionButtons;
    public float slideDistance = 100f;
    public float slideDuration = 0.3f;
    public float staggerDelay = 0.1f;

    [Header("Sound & Music Buttons")]
    public Button musicButton;
    public Button soundButton;
    public Sprite musicOnIcon;
    public Sprite musicOffIcon;
    public Sprite soundOnIcon;
    public Sprite soundOffIcon;

    [Header("Audio Sources")]
    public AudioSource musicSource;   // Background music
    public AudioSource sfxSource;     // Shared SFX (click + gameplay)
    public AudioClip clickSound;      // Click SFX

    private bool isOpen = false;
    private bool isMusicOn = true;
    private bool isSoundOn = true;
    private Vector2[] originalPositions;

    void Start()
    {
        // Hide panel and shift buttons down
        settingsPanel.SetActive(false);

        originalPositions = new Vector2[optionButtons.Length];
        for (int i = 0; i < optionButtons.Length; i++)
        {
            originalPositions[i] = optionButtons[i].anchoredPosition;
            optionButtons[i].anchoredPosition = originalPositions[i] - new Vector2(0, slideDistance);
        }

        // Set initial button icons
        UpdateMusicButtonIcon();
        UpdateSoundButtonIcon();
    }

    // ========================
    // SETTINGS PANEL TOGGLE
    // ========================
    public void ToggleSettings()
    {
        PlayClickSound(); // Play SFX

        StopAllCoroutines();
        isOpen = !isOpen;

        if (isOpen)
        {
            settingsPanel.SetActive(true);

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
            for (int i = optionButtons.Length - 1; i >= 0; i--)
            {
                StartCoroutine(SlideButton(
                    optionButtons[i],
                    originalPositions[i] - new Vector2(0, slideDistance),
                    slideDuration,
                    (optionButtons.Length - 1 - i) * staggerDelay,
                    i == 0 // Hide panel after last button slides out
                ));
            }
        }
    }

    private IEnumerator SlideButton(RectTransform button, Vector2 targetPos, float duration, float delay, bool hidePanelAtEnd = false)
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

        if (hidePanelAtEnd)
        {
            yield return new WaitForSecondsRealtime(0.05f);
            settingsPanel.SetActive(false);
        }
    }

    // ========================
    // MUSIC & SOUND TOGGLE
    // ========================
    public void ToggleMusic()
    {
        PlayClickSound();

        isMusicOn = !isMusicOn;
        musicSource.mute = !isMusicOn;
        UpdateMusicButtonIcon();
    }

    private void UpdateMusicButtonIcon()
    {
        musicButton.image.sprite = isMusicOn ? musicOnIcon : musicOffIcon;
    }

    public void ToggleSound()
    {
        PlayClickSound();

        isSoundOn = !isSoundOn;

        // Mute all SFX including button clicks
        if (sfxSource != null) sfxSource.mute = !isSoundOn;

        UpdateSoundButtonIcon();
    }

    private void UpdateSoundButtonIcon()
    {
        soundButton.image.sprite = isSoundOn ? soundOnIcon : soundOffIcon;
    }

    // ========================
    // BUTTON CLICK SFX
    // ========================
    public void PlayClickSound()
    {
        if (isSoundOn && clickSound != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(clickSound);
        }
    }

    // ========================
    // FOR OTHER GAMEPLAY SFX
    // ========================
    public void PlaySFX(AudioClip clip)
    {
        if (isSoundOn && clip != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}



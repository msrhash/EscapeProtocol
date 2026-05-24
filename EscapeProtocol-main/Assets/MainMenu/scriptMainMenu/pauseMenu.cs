using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
    [Header("Pause Panel Animation")]
    public RectTransform pausePanel;
    public float slideDuration = 0.5f;
    public Vector2 onScreenPosition = Vector2.zero;
    public Vector2 offScreenPosition = new Vector2(0, -1080);

    [Header("Floating Effect")]
    public bool enableFloating = true;
    public float floatAmplitude = 6f;
    public float floatSpeed = 1f;

    [Header("Audio Settings")]
    public AudioSource musicSource;   // Background music source
    public AudioSource sfxSource;     // SFX source for clicks
    public AudioClip clickSound;      // Click sound effect
    public Sprite musicOnIcon;
    public Sprite musicOffIcon;
    public Sprite soundOnIcon;
    public Sprite soundOffIcon;
    public Button musicButton;
    public Button soundButton;

    [Header("Buttons")]
    public Button resumeButton;
    public Button restartButton;
    public Button homeButton;

    private bool isPaused = false;
    private bool isMusicOn = true;
    private bool isSoundOn = true;
    private Coroutine floatingCoroutine;

    void Start()
    {
        // Start hidden
        pausePanel.anchoredPosition = offScreenPosition;
        pausePanel.gameObject.SetActive(false);

        // Hook up button listeners
        if (resumeButton != null) resumeButton.onClick.AddListener(TogglePause);
        if (restartButton != null) restartButton.onClick.AddListener(RestartLevel);
        if (homeButton != null) homeButton.onClick.AddListener(GoHome);
        if (musicButton != null) musicButton.onClick.AddListener(ToggleMusic);
        if (soundButton != null) soundButton.onClick.AddListener(ToggleSound);

        // Hook SFX for all buttons in this panel
        Button[] allButtons = pausePanel.GetComponentsInChildren<Button>();
        foreach (Button btn in allButtons)
        {
            btn.onClick.AddListener(PlayClickSound);
        }

        UpdateMusicButtonIcon();
        UpdateSoundButtonIcon();
    }

    // -------------------- Pause Menu Toggle --------------------
    public void TogglePause()
    {
        StopAllCoroutines();

        if (floatingCoroutine != null)
        {
            StopCoroutine(floatingCoroutine);
            floatingCoroutine = null;
        }

        isPaused = !isPaused;

        if (isPaused)
        {
            pausePanel.gameObject.SetActive(true);
            pausePanel.SetAsLastSibling();
            Time.timeScale = 0f;
            StartCoroutine(SlidePanel(pausePanel, onScreenPosition, false, true));
        }
        else
        {
            StartCoroutine(SlidePanel(pausePanel, offScreenPosition, true, false));
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
            Time.timeScale = 1f;
            isPaused = false;
        }
        else if (startFloating && enableFloating)
        {
            floatingCoroutine = StartCoroutine(FloatEffect());
        }
    }

    private IEnumerator FloatEffect()
    {
        Vector2 basePos = onScreenPosition;

        while (true)
        {
            float yOffset = Mathf.Sin(Time.unscaledTime * floatSpeed) * floatAmplitude;
            pausePanel.anchoredPosition = basePos + new Vector2(0, yOffset);
            yield return null;
        }
    }

    // -------------------- Audio Controls --------------------
    public void ToggleMusic()
    {
        isMusicOn = !isMusicOn;
        musicSource.mute = !isMusicOn;   // Only affects music
        UpdateMusicButtonIcon();
    }

    public void ToggleSound()
    {
        isSoundOn = !isSoundOn;
        sfxSource.mute = !isSoundOn;     // Only affects SFX
        UpdateSoundButtonIcon();
    }

    private void UpdateMusicButtonIcon()
    {
        if (musicButton != null)
            musicButton.image.sprite = isMusicOn ? musicOnIcon : musicOffIcon;
    }

    private void UpdateSoundButtonIcon()
    {
        if (soundButton != null)
            soundButton.image.sprite = isSoundOn ? soundOnIcon : soundOffIcon;
    }

    // -------------------- Level Controls --------------------
    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoHome()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); // Change to your menu scene name
    }

    // -------------------- SFX --------------------
    private void PlayClickSound()
    {
        if (sfxSource != null && clickSound != null && isSoundOn)
        {
            sfxSource.PlayOneShot(clickSound);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is quitting..."); // This will log in the console when running in the editor
    }

    
}

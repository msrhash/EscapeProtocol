using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelFinishManager : MonoBehaviour
{
    [Header("Win Panel Animation")]
    public RectTransform winPanel;
    public float popDuration = 0.3f;    // Time to pop in
    public float floatAmplitude = 6f;   // How far it floats
    public float floatSpeed = 1f;       // How fast it floats

    [Header("Buttons")]
    public Button restartButton;
    public Button nextLevelButton;
    public Button homeButton;

    private bool samAtDoor = false;
    private bool catAtDoor = false;
    private Coroutine floatCoroutine;

    private bool samCollectedClue = false;
    private bool catCollectedClue = false;

    public void SamCollectedClue()
    {
        samCollectedClue = true;
        CheckWinCondition();
    }

    public void CatCollectedClue()
    {
        catCollectedClue = true;
        CheckWinCondition();
    }


    void Start()
    {
        if (winPanel != null)
        {
            winPanel.gameObject.SetActive(false);
            winPanel.localScale = Vector3.zero; // Start invisible
        }

        // Hook up button listeners
        if (restartButton != null) restartButton.onClick.AddListener(RestartLevel);
        if (nextLevelButton != null) nextLevelButton.onClick.AddListener(NextLevel);
        if (homeButton != null) homeButton.onClick.AddListener(GoHome);
    }

    public void SamReachedDoor()
    {
        samAtDoor = true;
        CheckWinCondition();
    }

    public void CatReachedDoor()
    {
        catAtDoor = true;
        CheckWinCondition();
    }

    private void CheckWinCondition()
    {
        if (samAtDoor && catAtDoor && samCollectedClue && catCollectedClue)
        {
            ShowWinPanel();
        }
    }


    public void ShowWinPanel()
    {
        if (winPanel == null) return;

        winPanel.gameObject.SetActive(true);
        Time.timeScale = 0f; // Pause game

        // Pop in animation
        StartCoroutine(PopInPanel());

        // Start floating after pop
        if (floatCoroutine != null) StopCoroutine(floatCoroutine);
        floatCoroutine = StartCoroutine(FloatEffect());
    }

    private IEnumerator PopInPanel()
    {
        float elapsed = 0f;
        Vector3 startScale = Vector3.zero;
        Vector3 endScale = Vector3.one;

        while (elapsed < popDuration)
        {
            float t = elapsed / popDuration;
            t = Mathf.Sin(t * Mathf.PI * 0.5f); // Ease-out effect
            winPanel.localScale = Vector3.Lerp(startScale, endScale, t);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        winPanel.localScale = endScale;
    }

    private IEnumerator FloatEffect()
    {
        Vector3 basePos = winPanel.anchoredPosition;

        while (true)
        {
            float yOffset = Mathf.Sin(Time.unscaledTime * floatSpeed) * floatAmplitude;
            winPanel.anchoredPosition = basePos + new Vector3(0, yOffset, 0);
            yield return null;
        }
    }

    // -------------------- Buttons --------------------
    private void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void NextLevel()
    {
        Time.timeScale = 1f;
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No more levels. Going to Main Menu...");
            SceneManager.LoadScene("MainMenu");
        }
    }

    private void GoHome()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
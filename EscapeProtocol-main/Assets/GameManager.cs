using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Clue Tracking")]
    public int totalSamClues = 0;
    public int totalCatClues = 0;

    private int samCluesCollected = 0;
    private int catCluesCollected = 0;

    private bool samAtDoor = false;
    private bool catAtDoor = false;

    private LevelFinishManager levelFinishManager;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        levelFinishManager = FindAnyObjectByType<LevelFinishManager>();
    }

    public void CollectClue(string characterTag)
    {
        if (characterTag == "Sam")
            samCluesCollected++;
        else if (characterTag == "Cat")
            catCluesCollected++;
    }

    public bool HasCollectedAllClues(string characterTag)
    {
        if (characterTag == "Sam")
            return samCluesCollected >= totalSamClues;
        else if (characterTag == "Cat")
            return catCluesCollected >= totalCatClues;
        return false;
    }

    public void SetPlayerAtDoor(string characterTag, bool isAtDoor)
    {
        if (characterTag == "Sam")
            samAtDoor = isAtDoor;
        else if (characterTag == "Cat")
            catAtDoor = isAtDoor;

        CheckLevelComplete();
    }

    public void CheckLevelComplete()
    {
        if (samAtDoor && catAtDoor &&
            samCluesCollected >= totalSamClues &&
            catCluesCollected >= totalCatClues)
        {
            Debug.Log(" Level Completed!");
            if (levelFinishManager != null)
                levelFinishManager.ShowWinPanel();
        }
    }
}


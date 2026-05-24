using UnityEngine;

public class BlueClue : MonoBehaviour
{
    private LevelFinishManager levelFinishManager;

    private void Start()
    {
        levelFinishManager = FindFirstObjectByType<LevelFinishManager>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sam"))
        {
            if (levelFinishManager != null)
            {
                levelFinishManager.SamCollectedClue(); // You'll add this method next
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("LevelFinishManager not found in scene.");
            }
        }
    }
}
using UnityEngine;

public class PinkClue : MonoBehaviour
{
    private LevelFinishManager levelFinishManager;

    private void Start()
    {
        levelFinishManager = FindFirstObjectByType<LevelFinishManager>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Cat"))
        {
            if (levelFinishManager != null)
            {
                levelFinishManager.CatCollectedClue(); // You'll add this method next
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("LevelFinishManager not found in scene.");
            }
        }
    }
}
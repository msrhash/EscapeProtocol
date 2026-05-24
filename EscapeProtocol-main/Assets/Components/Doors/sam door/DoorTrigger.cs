using UnityEngine;

public class DoorTrigger : MonoBehaviour
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
            levelFinishManager?.SamReachedDoor();
        }
        else if (other.CompareTag("Cat"))
        {
            levelFinishManager?.CatReachedDoor();
        }
    }
}


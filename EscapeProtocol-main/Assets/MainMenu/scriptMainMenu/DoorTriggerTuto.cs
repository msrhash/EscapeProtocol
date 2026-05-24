using UnityEngine;

public class DoorTriggerTuto : MonoBehaviour
{
    private LevelFinishManagerTuto levelFinishManager;

    private void Start()
    {
        levelFinishManager = FindFirstObjectByType<LevelFinishManagerTuto>();
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



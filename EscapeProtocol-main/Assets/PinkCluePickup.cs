using UnityEngine;

public class PinkCluePickup : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Cat"))
        {
            GameManager.Instance.CollectClue("Cat");
            Destroy(gameObject);
        }
    }
}

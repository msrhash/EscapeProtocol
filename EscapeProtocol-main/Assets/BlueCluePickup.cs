using UnityEngine;

public class BlueCluePickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sam"))
        {
            GameManager.Instance.CollectClue("Sam");
            Destroy(gameObject);
        }
    }
}

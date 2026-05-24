using UnityEngine;

public class Pipe : MonoBehaviour
{
    public Transform Exit;           // Assign this in the Inspector
    public string allowedTag = "Cat";     // Only Cat can teleport

    private bool canTeleport = true;      // Prevent instant re-teleportation
    public float teleportCooldown = 0.5f; // Time to allow teleporting again

    void OnTriggerEnter2D(Collider2D other)
    {
        if (canTeleport && other.CompareTag(allowedTag))
        {
            StartCoroutine(Teleport(other.gameObject));
        }
    }

    System.Collections.IEnumerator Teleport(GameObject player)
    {
        canTeleport = false;

        // Optional: Play animation or hide player briefly
        player.transform.position = Exit.position;

        yield return new WaitForSeconds(teleportCooldown);
        canTeleport = true;
    }
}

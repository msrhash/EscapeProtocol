using System.Collections;
using UnityEngine;

public class pipe : MonoBehaviour
{
    public Transform exitPoint;              // Assign in Inspector
    public string allowedTag = "Cat";        // Tag for Cat only
    public float teleportCooldown = 0.5f;    // Time to wait before allowing another teleport

    private bool canTeleport = true;         // ✅ This is declared at class-level

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (canTeleport && other.CompareTag(allowedTag) && exitPoint != null)
        {
            StartCoroutine(Teleport(other.gameObject));
        }
    }

    private IEnumerator Teleport(GameObject player)
    {
        canTeleport = false;  // ✅ Uses class-level variable

        // Teleport the player
        player.transform.position = exitPoint.position;

        // Wait before allowing next teleport
        yield return new WaitForSeconds(teleportCooldown);

        canTeleport = true;
    }
}

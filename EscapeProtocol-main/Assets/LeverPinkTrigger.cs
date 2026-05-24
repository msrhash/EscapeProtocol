using UnityEngine;

public class LeverPinkTrigger : MonoBehaviour
{
    public Animator animator;
    public MovingPlatformPink1 platform; // Link your platform script
    private bool isDown = false;
    private bool canTrigger = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.CompareTag("Sam") || other.CompareTag("Cat")) && canTrigger)
        {
            canTrigger = false; // Prevent retriggering while still inside

            if (!isDown)
            {
                animator.Play("leverPushRight");
                platform.MoveDown();
                isDown = true;
            }
            else
            {
                animator.Play("LeverPushLeft");
                platform.MoveUp();
                isDown = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Sam") || other.CompareTag("Cat"))
        {
            canTrigger = true; // Allow trigger again after exit
        }
    }
}

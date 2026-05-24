using UnityEngine;

public class ButtonTriggerPink : MonoBehaviour
{
    public Animator animator; // Assign OrangeButtonVisual's Animator
    public MovingPlatformPink1 platform; // Assign MovingPlatformOrange here

    private int objectCount = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sam") || other.CompareTag("Cat") || other.CompareTag("HeavyObject"))
        {
            objectCount++;
            if (objectCount == 1) // First object stepped on
            {
                animator.SetBool("IsPressed", true);
                platform.MoveDown();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Sam") || other.CompareTag("Cat") || other.CompareTag("HeavyObject"))
        {
            objectCount--;
            if (objectCount <= 0)
            {
                animator.SetBool("IsPressed", false);
                platform.MoveUp();
                objectCount = 0;
            }
        }
    }
}
using UnityEngine;

public class ButtonTriggerGreen : MonoBehaviour
{
    public Animator buttonAnimator;
    public MovingPlatformGreen platform;

    private int objectsOnButton = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Sam") || collision.collider.CompareTag("Cat") || collision.collider.CompareTag("Box"))
        {
            objectsOnButton++;
            if (objectsOnButton == 1)
            {
                buttonAnimator.SetBool("IsPressed", true);
                platform.MoveDown(); // Replace with your own method
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Sam") || collision.collider.CompareTag("Cat") || collision.collider.CompareTag("Box"))
        {
            objectsOnButton--;
            if (objectsOnButton == 0)
            {
                buttonAnimator.SetBool("IsPressed", false);
                platform.MoveUp(); // Replace with your own method
            }
        }
    }
}


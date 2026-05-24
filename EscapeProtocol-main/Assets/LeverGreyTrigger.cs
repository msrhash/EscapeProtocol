using UnityEngine;

public class LeverGreyTrigger : MonoBehaviour
{
    public Animator leverAnimator;
    public MovingPlatformGrey platformToControl;

    private bool isPushed = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sam") || other.CompareTag("Cat"))
        {
            isPushed = !isPushed;
            leverAnimator.SetTrigger(isPushed ? "PushRight" : "PushLeft");
            platformToControl.TogglePlatform();
        }
    }
}

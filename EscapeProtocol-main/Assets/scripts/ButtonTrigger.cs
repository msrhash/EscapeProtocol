using UnityEngine;

public class ButtonTriger : MonoBehaviour
{
    public PlatformMover platform; // Link to the platform
    private bool Pressed = false;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sam") || other.CompareTag("Cat"))
        {
            Pressed = true;
            platform.MoveDown();

            if (anim != null)
            {
                anim.SetTrigger("Press");
            }
        }
    }
}
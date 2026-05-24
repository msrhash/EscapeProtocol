using UnityEngine;

public class MovingPlatformGrey : MonoBehaviour
{
    public Transform topPosition;
    public Transform bottomPosition;
    public float moveSpeed = 2f;

    private bool moveDown = false;

    void Start()
    {
        transform.position = topPosition.position; // Lock to top at start
    }

    void Update()
    {
        Transform target = moveDown ? bottomPosition : topPosition;
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }

    public void TogglePlatform()
    {
        moveDown = !moveDown;
    }
}

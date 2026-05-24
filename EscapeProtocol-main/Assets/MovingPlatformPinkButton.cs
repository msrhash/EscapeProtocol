using UnityEngine;

public class MovingPlatformPink : MonoBehaviour
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
        if (topPosition == null || bottomPosition == null)
        {
            Debug.LogWarning("Top or Bottom position not assigned.");
            return;
        }

        Vector3 target = moveDown ? bottomPosition.position : topPosition.position;
        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
    }

    public void MoveDown()
    {
        Debug.Log(gameObject.name + " is moving DOWN");
        moveDown = true;
    }

    public void MoveUp()
    {
        moveDown = false;
    }
}


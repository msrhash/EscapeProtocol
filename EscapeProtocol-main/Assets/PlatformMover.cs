using System;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    public Transform Top;
    public Transform Bottom;
    public float moveSpeed = 2f;

    private Vector3 nextPosition;
    private bool isMoving = false;

    public void MoveDown()
    {
        isMoving = true;
        nextPosition = Bottom.position;
    }

    private void Start()
    {
        
        nextPosition = transform.position;
    }

    private void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextPosition, moveSpeed * Time.deltaTime);

            if (transform.position == nextPosition)
            {
                isMoving = false; 
            }
        }
    }

    
    internal void TriggerMove()
    {
        MoveDown(); 
    }

    internal void MoveUp()
    {
        throw new NotImplementedException();
    }
}

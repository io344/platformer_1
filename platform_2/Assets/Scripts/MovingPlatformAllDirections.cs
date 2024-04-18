using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformAllDirections : MonoBehaviour
{
    public Transform up;
    public Transform down;
    public Transform left;
    public Transform right;

    public float speed = 5f;

    public bool isMovingRight;
    public bool isMovingHorizontal;
    public bool isMovingUp;

    public bool isMovingVertical;

    void Start()
    {
        isMovingRight = true;
        isMovingHorizontal = false;
        isMovingUp = true;
    }

    void Update()
    {
        if (isMovingHorizontal)
        {
            HorizontalMove();
        }
        else
        {
            VerticalMove();
        }
    }

    void HorizontalMove()
    {
        if (Mathf.Approximately(transform.position.x, left.position.x))
        {
            isMovingRight = true;
            isMovingHorizontal = false;
        }
        else if (Mathf.Approximately(transform.position.x, right.position.x))
        {
            isMovingRight = false;
            isMovingHorizontal = true;
        }

        if (isMovingRight)
        {
            transform.position = Vector2.MoveTowards(transform.position, right.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, left.position, speed * Time.deltaTime);
        }
    }

    void VerticalMove()
    {
        if (Mathf.Approximately(transform.position.y, down.position.y))
        {
            isMovingUp = true;
            isMovingHorizontal = true;
        }
        else if (Mathf.Approximately(transform.position.y, up.position.y))
        {
            isMovingUp = false;
            isMovingVertical = false;
        }

        if (isMovingUp)
        {
            transform.position = Vector2.MoveTowards(transform.position, up.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, down.position, speed * Time.deltaTime);
        }
    }

    void OnDrawGizmos()
    {
        // Drawing for left and right
        Gizmos.color = Color.red;
        Gizmos.DrawLine(left.position, right.position);

        Gizmos.DrawSphere(left.position, 0.3f);
        Gizmos.DrawSphere(right.position, 0.3f);

        // Drawing for up and down
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(up.position, down.position);

        Gizmos.DrawSphere(up.position, 0.3f);
        Gizmos.DrawSphere(down.position, 0.3f);
    }
}

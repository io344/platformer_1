using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformHorizontal : MonoBehaviour
{
    public float speed = 5f;

    public Transform left;
    public Transform right;

    public bool isMovingRight;

    public virtual void Start()
    {
        isMovingRight = true;
    }

    void Update()
    {
        HorizontalMove();
    }

    void HorizontalMove()
    {
        if (Mathf.Approximately(transform.position.x, left.position.x))
        {
            isMovingRight = true;
        }
        else if (Mathf.Approximately(transform.position.x, right.position.x))
        {
            isMovingRight = false;
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

    void OnDrawGizmos()
    {
        // Drawing for left and right
        Gizmos.color = Color.red;
        Gizmos.DrawLine(left.position, right.position);

        Gizmos.DrawSphere(left.position, 0.3f);
        Gizmos.DrawSphere(right.position, 0.3f);
    }
}

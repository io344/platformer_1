using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public float leftAngle;
    public float rightAngle;

    public bool isMovingClockwise = true;

    void Update()
    {
        Move();
    }
    public void ChangeinMoveDirection()
    {
        if (transform.rotation.z > rightAngle)
        {
            isMovingClockwise = false;
        }
        if (transform.rotation.z < leftAngle)
        {
            isMovingClockwise = true;
        }
    }

    public void Move()
    {
        ChangeinMoveDirection();

        if (isMovingClockwise)
        {
            rb.angularVelocity = speed;
        }

        if (!isMovingClockwise)
        {
            rb.angularVelocity = -1 * speed;
        }
    }
}

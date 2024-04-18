using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_Movement : MonoBehaviour
{
    public float radius = 2f; 
    public float speed = 2f;
    float angle = 0f;
    float x;
    float y;
    void Update()
    {
        // formula for position
        x = Mathf.Cos(angle) * radius;
        y = Mathf.Sin(angle) * radius;

        // pinpoint the position
        transform.position = new Vector3(x, y, 0f);

        // angle will be based on speed 
        angle += speed * Time.deltaTime;

        if (angle > 360f)
        {
            angle -= 360f;
        }
    }
}

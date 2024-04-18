using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thwomp : MonoBehaviour
{
    public float fallSpeed = 5f;      
    public float riseSpeed = 2f;
    public bool isSmashing = false;

    public Transform pos_up;
    public Transform pos_down;

    private void Update()
    {
        if(transform.position.y >= pos_up.position.y)
        {
            isSmashing = true;
        }
        else if(transform.position.y <= pos_down.position.y)
        {
            isSmashing = false;
        }

        if (isSmashing)
        {
            transform.position = Vector2.MoveTowards(transform.position, pos_down.position, fallSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, pos_up.position, riseSpeed * Time.deltaTime);
        }
    }

    private void OnDrawGizmos()
    {
        if(pos_up != null && pos_down != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(pos_up.position, pos_down.position);

            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(pos_up.position, 0.2f);
            Gizmos.DrawSphere(pos_down.position, 0.2f);
        }
    }

}

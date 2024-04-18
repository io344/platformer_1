using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastDeath : MonoBehaviour
{
    public float deathDistance = 5f;
    public LayerMask groundMask;
    public Vector2 rayDirection = Vector2.down;
    public BoxCollider2D bc;

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, deathDistance, groundMask);

        if(hit.collider != null)
        {
            Debug.Log("Grounded mah Indio!");
            bc.enabled = true;
        }
        else
        {
            bc.enabled = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + ((Vector3)rayDirection * deathDistance));
    }
}

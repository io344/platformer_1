using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingDiamond : MonoBehaviour
{
    public float RaycastLength = 3f;
    public LayerMask playerLayer; // Physic2D.queriesStartinColliders alt
    public Rigidbody2D rb;
    public bool isFalling = false;

    void Update()
    {
        if (isFalling == false)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, RaycastLength, playerLayer);
            // check if we hit something
            if (hit.transform != null)
            {
                Debug.Log("Gumagana gagu!");
                isFalling = true;
                rb.gravityScale = 5;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Ground")
        {
            rb.gravityScale = 0;
            rb.bodyType = RigidbodyType2D.Static;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3.down * RaycastLength));
    }
}

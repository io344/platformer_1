using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondBill : MonoBehaviour
{
    public Vector2 billDirection = Vector2.left; // direction where it shoots
    public Vector2 rayDirection = Vector2.left;
    public float RaycastLength = 3f;
    public LayerMask playerLayer;
    public float shootingSpeed = 5f;
    public bool isShooting;
    public Rigidbody2D rb;
   

    void Start()
    {
        isShooting = false;
    }

    
    void Update()
    {
        if (!isShooting)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, RaycastLength, playerLayer);
            if (hit.collider != null)
            {
                isShooting = true;
                rb.AddForce(billDirection * shootingSpeed, ForceMode2D.Impulse);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3.left * RaycastLength));
    }
}

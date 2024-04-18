using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float bounceForce = 20f;
    public Vector2 bounceDir = Vector2.up; // only up and down i'm stoopid

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(bounceDir * bounceForce, ForceMode2D.Impulse);
        }
    }
}

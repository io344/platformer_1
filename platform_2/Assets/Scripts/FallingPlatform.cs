using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float fall_delay = 0.5f;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            StartCoroutine(Fall());
        } 
    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(fall_delay);
        rb.bodyType = RigidbodyType2D.Dynamic;
        Destroy(gameObject, 2f);
    }
}

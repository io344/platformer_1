using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_death : MonoBehaviour
{
    public BoxCollider2D bc;
    public SpriteRenderer sr;
    public Rigidbody2D rb;
    public Behaviour playerMovement;

    public bool isDead = false;

    Vector3 respawnPoint;
    //For not trigger objects
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            KillPlayer();
        }
    }

    //For Trigger Objects
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            KillPlayer();
        }
        if (other.CompareTag("Checkpoint"))
        {
            respawnPoint = transform.position;
        }
    }

    public void KillPlayer()
    {
        StartCoroutine(deathnalive());
    }

    void dead()
    {
        isDead = true;
        sr.enabled = false;
        bc.enabled = false;
        rb.bodyType = RigidbodyType2D.Static;
        playerMovement.enabled = false;
    }

    void respawn()
    {
        isDead = false;
        transform.position = respawnPoint;
        sr.enabled = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        bc.enabled = true;
        playerMovement.enabled = true;
    }

    // Please edit the name i don't know what to name this appropriately.
    IEnumerator deathnalive()
    {
        dead();
        yield return new WaitForSeconds(0.5f);
        respawn();
    }
}

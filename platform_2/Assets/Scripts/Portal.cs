using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform exitPortal;
    public CircleCollider2D collider_of_exit; // collider of the exit portal

    void teleport_player(GameObject player)
    {
        player.transform.position = exitPortal.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            teleport_player(other.gameObject);
            StartCoroutine(nonGlitchable());
        }
    }
    IEnumerator nonGlitchable() // so the player wont glitch out if teleported back and forth
    {
        collider_of_exit.enabled = false;
        yield return new WaitForSeconds(1.5f);
        collider_of_exit.enabled = true;
    }
}

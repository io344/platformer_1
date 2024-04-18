using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformTrigger : MonoBehaviour
{
    Transform playerParent;
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Interactable")
        {
            playerParent = transform.parent;
            // Set the moving platform as the parent of the player
            transform.parent = other.transform;
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.tag == "Interactable")
        {
            transform.parent = playerParent;
        }
    }
}

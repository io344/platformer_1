using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeObjDisappear : MonoBehaviour
{ 
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entered OnTriggerEnter2D");

        if (other.CompareTag("Bermuda"))
        {
            Debug.Log("Collided with Bermuda. Destroying GameObject.");
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Collided with something other than Bermuda.");
        }
    }

}

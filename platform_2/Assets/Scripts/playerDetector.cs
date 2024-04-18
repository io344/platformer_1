using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDetector : MonoBehaviour
{
    [System.Serializable]
    public class RaycastInfo
    {
        public Vector2 direction;
        public float length;
        public LayerMask layerMask;
    }

    public RaycastInfo[] raycasts;
    public Behaviour enemyScript;
    public bool playerdetected;

    void Update()
    {
        playerdetected = false;

        foreach (RaycastInfo raycast in raycasts)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, raycast.direction, raycast.length, raycast.layerMask);
            if (hit.collider != null)
            {
                Debug.Log("Raycast hit: " + hit.collider.gameObject.name);
                playerdetected = true;
            }
            
        }
        enemyScript.enabled = playerdetected;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (RaycastInfo raycast in raycasts)
        {
            Gizmos.DrawLine(transform.position, transform.position + ((Vector3)raycast.direction * raycast.length));
        }
    }

}

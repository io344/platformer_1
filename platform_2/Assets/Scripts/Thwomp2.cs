using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thwomp2 : MonoBehaviour
{
    public float distance = 4f;
    public float smashSpeed = 15f;
    public LayerMask playerMask;
    public Transform up;
    public Transform down;
    // Stores the player's fucking position: thnks chatgpt!
    private Vector2 playerPosition;

    void Start()
    {
        playerPosition = transform.position;
    }

    void Update()
    {
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down, distance, playerMask);
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up, distance, playerMask);
        if (hitDown.collider != null)
        {
            Debug.Log("Wow! Tumama sa baba");
            // Thwomp moves downward
            playerPosition = down.position;
        }

        if(hitUp.collider != null)
        {
            Debug.Log("Wow! Tumama sa taas!");
            // Thwomp moves upward
            playerPosition = up.position;
        }
        // Code below makes the movetowards continous
        // makes the movetowards a fucking value!!!
        float moveY = Mathf.MoveTowards(transform.position.y, playerPosition.y, smashSpeed * Time.deltaTime);
        transform.position = new Vector2(transform.position.x, moveY);
    }
    void drawPlayerdetection()
    {
        // Drawing pababa
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + ((Vector3)Vector2.down * distance));

        //Drawing pataas
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + ((Vector3)Vector2.up * distance));
    }

    void drawThwompMovement()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(up.position, down.position);

        Gizmos.color = Color.black;
        Gizmos.DrawSphere(up.position, 0.2f);
        Gizmos.DrawSphere(down.position, 0.2f);
        
    }

    void OnDrawGizmos()
    {
        drawPlayerdetection();
        drawThwompMovement();
    }
}

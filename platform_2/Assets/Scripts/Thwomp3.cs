using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thwomp3 : MonoBehaviour
{
    public float smashSpeed = 5f;
    public float riseSpeed = 4f;
    public float waitTime = 2f;
    public LayerMask playerLayer;
    public Transform smash_dist;

    private bool playerDetected = false;
    private Vector2 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        if (playerDetected)
        {
            Smash();
        }
        else
        {
            MoveUp();
            DetectPlayer();
        }
    }

    void Smash()
    {
        transform.position = Vector2.MoveTowards(transform.position, smash_dist.position, smashSpeed * Time.deltaTime);
    }

    void MoveUp()
    {
        transform.position = Vector2.MoveTowards(transform.position, initialPosition, riseSpeed * Time.deltaTime);
    }

    void DetectPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, playerLayer);

        if (hit.collider != null)
        {
            playerDetected = true;
            Invoke("ResetDetection", waitTime);
        }
    }

    void ResetDetection()
    {
        playerDetected = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, smash_dist.position);

        Gizmos.color = Color.black;
        Gizmos.DrawSphere(smash_dist.position, 0.2f);
    }
}

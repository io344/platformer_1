using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BooDetector : MonoBehaviour
{
    public float raycastLength = 10f;
    public LayerMask booMask;
    public bool isFacingRight = true;
    public bool booToMove; 
    public Behaviour booMovement;

    Vector2[] directions;
    RaycastHit2D[] hits;

    void Start()
    {

        directions = new Vector2[3];

        float angle = isFacingRight ? 30f : 150f;
        directions[0] = isFacingRight ? Vector2.right : Vector2.left;
        directions[1] = Quaternion.Euler(0, 0, angle) * Vector2.right;
        directions[2] = Quaternion.Euler(0, 0, -angle) * Vector2.right;

        hits = new RaycastHit2D[3];
    }

    void Update()
    {
        booToMove = true;

        for (int i = 0; i < directions.Length; i++)
        {
            hits[i] = Physics2D.Raycast(transform.position, directions[i], raycastLength, booMask);

            if (hits[i].collider != null)
            {
                Debug.Log("Raycast hit " + hits[i].collider.gameObject.name);
                booToMove = false;
                break; 
            }
        }

        booMovement.enabled = booToMove;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + ((Vector3)directions[0] * raycastLength));

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + ((Vector3)directions[1] * raycastLength));

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + ((Vector3)directions[2] * raycastLength));
    }

    public void Flip()
    {
        isFacingRight = !isFacingRight;

        directions[0] = isFacingRight ? Vector2.right : Vector2.left;

        float angle = isFacingRight ? 30f : 150f;
        directions[1] = Quaternion.Euler(0, 0, angle) * Vector2.right;
        directions[2] = Quaternion.Euler(0, 0, -angle) * Vector2.right;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target;
    public float followSpeed = 2f;

    void Update()
    {
        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            transform.Translate(direction.normalized * followSpeed * Time.deltaTime);
        }
    }

}

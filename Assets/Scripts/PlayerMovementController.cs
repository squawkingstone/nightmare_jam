using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] Rigidbody rigidbody;
    [SerializeField] Transform head;
    [SerializeField] float speed;

    private Vector3 dir;
    private float h, v, f;

    private bool flying;

    private void Start()
    {
        flying = false;
    }

    private void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        f = Input.GetAxis("Fly");

        if (flying)
        {
            dir = ((Vector3.ProjectOnPlane(head.forward, Vector3.up).normalized * v) + (head.right * h) + (Vector3.up * f)).normalized * speed;
            if (Physics.Raycast(transform.position, Vector3.down, 0.1f))
            {
                flying = false;
                rigidbody.useGravity = true;
            }
        }
        else
        {
            dir = ((Vector3.ProjectOnPlane(head.forward, Vector3.up).normalized * v) + (head.right * h)).normalized * speed;
            dir.Set(dir.x, rigidbody.velocity.y, dir.z);
            if (f > 0f)
            {
                transform.position += Vector3.up;
                flying = true;
                rigidbody.useGravity = false;
            }
        }

        rigidbody.velocity = dir;
    }

}

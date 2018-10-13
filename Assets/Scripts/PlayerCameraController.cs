using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [Header("Object References")]
    [SerializeField] Camera camera;
    [SerializeField] Transform head;

    [Header("Camera Parameters")]
    [SerializeField] float orbit_distance;
    [SerializeField] float sensitivity;
    [SerializeField] bool invert_x;
    [SerializeField] bool invert_y;

    private float mx, my;
    private RaycastHit hit;

    private void Update()
    {
        mx += Input.GetAxis("Mouse X") * sensitivity * ((invert_x) ? -1f : 1f);
        my += Input.GetAxis("Mouse Y") * sensitivity * ((invert_y) ? -1f : 1f);


        head.localRotation = Quaternion.Euler(my, mx, 0f);

        if (Physics.Raycast(head.position, -head.forward, out hit, orbit_distance))
        {
            camera.transform.position = hit.point;
        }
        else
        {
            camera.transform.position = (-head.forward * orbit_distance) + head.position;
        }

        camera.transform.LookAt(head);
    }

}

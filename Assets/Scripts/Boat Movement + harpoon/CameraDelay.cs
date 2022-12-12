using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDelay : MonoBehaviour
{
    public Transform player;
    public Transform behindBoat;
    public Vector3 offset;
    public Vector3 lookOffset;
    public float turnSpeed = 0.5f;

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 desiredPosition = player.position + offset;
        transform.LookAt(player.position + lookOffset);
        transform.position = Vector3.Lerp(transform.position, behindBoat.position, Time.deltaTime * turnSpeed);
    }
}

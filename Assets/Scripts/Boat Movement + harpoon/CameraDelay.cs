using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDelay : MonoBehaviour
{
    public Transform player;
    public Transform behindBoat;
    public Transform frontBoat;
    public Vector3 offset;
    public Vector3 lookOffset;
    public float turnSpeed = 0.5f;
    public BoatMovement moveBackwards;

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 desiredPosition = player.position + offset;
        transform.LookAt(player.position + lookOffset);
        if (moveBackwards.moveBackward == false)
        {
            transform.position = Vector3.Lerp(transform.position, behindBoat.position, Time.deltaTime * turnSpeed);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, frontBoat.position, Time.deltaTime * turnSpeed);
        }

    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatControls : MonoBehaviour
{
    
    public float moveSpeedForward;
    public float moveSpeedBackwards;
    public float speedClamp = 100f;
    public float turnSpeed;
    public Vector3 boatTurning;
    public float moveMaxSpeed = 70f; // Change this for max speed, along with the drag value in the rigidbody
    public float cameraDistanceFromPlayer = 5f;
    public float cameraHeight = 5f;
    public Rigidbody boatBody;

    [Header("Camera")]
    public bool cameraOn;

    private Transform cameraTransform;
    // Start is called before the first frame update
    void Start()
    {
        boatBody = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;
       // moveSpeedForward = 0f;
       // moveSpeedBackwards = 0f;
        
       // turnSpeed = 50f; // Change this for turning speed, along with the angular drag value in the rigidbody
        boatTurning = new Vector3(0, turnSpeed, 0);
    }
    private void Update()
    {
        float force = Input.GetAxis("Vertical");
        boatBody.AddForce((force * transform.forward).normalized * moveSpeedForward * Time.deltaTime, ForceMode.Acceleration);
        if (boatBody.velocity.magnitude >= moveMaxSpeed)
            boatBody.velocity = boatBody.velocity.normalized * moveMaxSpeed;


    }
    // FixedUpdate is called once per frame
    void FixedUpdate()
    {

        Vector3 velocity = boatBody.velocity;
        //boatBody.velocity = transform.forward * Mathf.Clamp(velocity.magnitude, -speedClamp, speedClamp);
/*
        if (Input.GetKey("w")) // Registers the input
            {
            if(moveSpeedForward <= moveMaxSpeed)
            {
                moveSpeedForward += 25f; // moveSpeed is multiplied over time to the cap
            }
            float force = Input.GetAxis("Vertical");
            boatBody.AddForce(transform.forward * moveSpeedForward * force);
        }
        else
        {
            if (moveSpeedForward > 0f)
            {
                moveSpeedForward -= 25f; // moveSpeed is subtracted over time to zero
            } 
        }

        if (Input.GetKey("s")) // Registers the input
        {
            if (moveSpeedBackwards <= moveMaxSpeed)
            {
                moveSpeedBackwards -= 25f; // moveSpeed is added over time to the cap
            }
            float force = Input.GetAxis("Vertical");
            boatBody.AddForce(velocity * moveSpeedBackwards * force);
            
        }
        else
        {
            if (moveSpeedBackwards > 0f)
            {
                moveSpeedBackwards -= 25f; // moveSpeed is subtracted over time to zero
            }
        }
*/
       // print(velocity);

        if (Input.GetKey("a")) // Registers the input
        {
            Quaternion boatRotation = Quaternion.Euler(-boatTurning * Time.fixedDeltaTime); // Quaternion formed to create the rotation possible
            boatBody.MoveRotation(boatBody.rotation * boatRotation); // Rotates the boat when prompted
        }

        if (Input.GetKey("d")) // Registers the input
        {
            Quaternion boatRotation = Quaternion.Euler(boatTurning * Time.fixedDeltaTime); // Quaternion formed to create the rotation possible
            boatBody.MoveRotation(boatBody.rotation * boatRotation); // Rotates the boat when prompted
        }

        if (cameraOn)
        {
            cameraTransform.position = transform.position + transform.rotation * new Vector3(0, cameraHeight, -cameraDistanceFromPlayer);
            cameraTransform.rotation = Quaternion.LookRotation(transform.position - cameraTransform.position);
        }

    }
}


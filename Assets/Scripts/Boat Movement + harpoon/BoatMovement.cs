using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    private Rigidbody _rb;

    [Header("Boat Properties")]
    [SerializeField] private float turnSpeed;
    public float moveSpeed;
    [SerializeField] private float maxMoveSpeed;

    private bool moveForward = false;
    private bool moveBackward = false;
    private bool turnLeft = false;
    private bool turnRight = false;

    public bool wonRace = false;
    public PurpleBoatRacing race;

    public GameObject rubble;
    public bool rubbleTriggered = false;

    [Header("Key Inputs")]
    [SerializeField] private KeyCode forwardKey;
    [SerializeField] private KeyCode backKey;
    [SerializeField] private KeyCode rightKey;
    [SerializeField] private KeyCode leftKey;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        MyInput();
        CheckSpeed();
    }

    private void MyInput()
    {
        if (race.startCounter == false)
        {
            if (Input.GetKey(forwardKey))
                moveForward = true;
            else
                moveForward = false;
            if (Input.GetKey(backKey))
                moveBackward = true;
            else
                moveBackward = false;
        }
        
        if (Input.GetKey(leftKey))
            turnLeft = true;
        else
            turnLeft = false;
        if (Input.GetKey(rightKey))
            turnRight = true;
        else
            turnRight = false;
    }

    private void FixedUpdate()
    {
        Movement();
        Turning();
    }

    private void Movement()
    {
        if (moveForward)
        {
            _rb.AddForce(moveSpeed * transform.forward, ForceMode.Acceleration);
        }
        if (moveBackward)
        {
            _rb.AddForce(moveSpeed * -transform.forward, ForceMode.Acceleration);
        }
    }

    private void Turning()
    {
        if (turnLeft)
        {
            transform.Rotate(transform.up * -turnSpeed);
        }

        if (turnRight)
        {
            transform.Rotate(transform.up * turnSpeed);
        }    
    }

    /// <summary>
    /// Checks current speed 
    /// If over max
    /// Reset speed to max
    /// </summary>
    private void CheckSpeed()
    {
        if (_rb.velocity.magnitude > maxMoveSpeed && moveForward)
            _rb.velocity = transform.forward * maxMoveSpeed;
        else if (_rb.velocity.magnitude > maxMoveSpeed && moveBackward)
            _rb.velocity = -transform.forward * maxMoveSpeed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Goal")
        {
            if (race.raceAllowed == true)
            {
                GetComponent<DialogueTrigger>().TriggerDialogue();
                wonRace = true;
            }
           
        }

        if (other.tag == "Rubble Trigger")
        {
            if(rubbleTriggered == false)
            {
                rubbleTriggered = true;
                rubble.SetActive(true);
            }
            
        }
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    private Rigidbody _rb;
    public DialogueManager triggeredDialogue;

    [Header("Boat Properties")]
    [SerializeField] private float turnSpeed;
    public float moveSpeed;
    [SerializeField] private float maxMoveSpeed;

    [Header("Key Checks")]
    private bool moveForward = false;
    public bool moveBackward = false;
    private bool turnLeft = false;
    private bool turnRight = false;

    public bool wonRace = false;
    public PurpleBoatRacing race;

    public GameObject[] rubble;
    public bool rubbleTriggered = false;

    [Header("Key Inputs")]
    [SerializeField] private KeyCode forwardKey;
    [SerializeField] private KeyCode backKey;
    [SerializeField] private KeyCode rightKey;
    [SerializeField] private KeyCode leftKey;

    [Header("Audio")]
    [SerializeField] private AudioSource boatMovementAudio;

    [Header("Pause Menu")]
    public GameObject pauseMenu;
    public GameObject optionsMenu;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        for (int i = 0; i < rubble.Length; i++)
        {
            rubble[i].SetActive(false);
        }
        
    }

    private void Update()
    {
        MyInput();
        CheckSpeed();
        PauseMenu();
    }

    private void MyInput()
    {
        if (race.startCounter == false)
        {
            if (Input.GetKey(forwardKey))
            {
                moveForward = true;
                boatMovementAudio.Play();
            }
            else
            {
                moveForward = false;
                boatMovementAudio.Stop();
            }

            if (Input.GetKey(backKey))
                moveBackward = true;
            else
                moveBackward = false;


            if (Input.GetKey(leftKey))
                turnLeft = true;
            else
                turnLeft = false;
            if (Input.GetKey(rightKey))
                turnRight = true;
            else
                turnRight = false;
        }
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

    void PauseMenu()
    {
        if (Input.GetKeyDown("escape") && triggeredDialogue.dialogueOn == false)
        {
            if (pauseMenu.activeSelf == true) //Unpause.
            {              
                pauseMenu.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                if (optionsMenu.activeSelf == true) //Pause menu closes form options menu.
                {
                    optionsMenu.SetActive(false);
                    Time.timeScale = 1;
                }
                else
                {
                    pauseMenu.SetActive(true);
                    Time.timeScale = 0;
                }           
            }
        }
    }

    /// <summary>
    /// Checks current speed 
    /// If over max
    /// Reset speed to max
    /// </summary>
    private void CheckSpeed()
    {
        _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, maxMoveSpeed);

        /*if (_rb.velocity.magnitude > maxMoveSpeed && moveForward)
            _rb.velocity = transform.forward * maxMoveSpeed;
        else if (_rb.velocity.magnitude > maxMoveSpeed && moveBackward)
            _rb.velocity = -transform.forward * maxMoveSpeed;*/
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
                for (int i = 0; i < rubble.Length; i++)
                {
                    rubble[i].SetActive(true);
                }
            }
            
        }

        if (other.tag == "Bounce")
        {
            print("working");
            _rb.AddForce(100 * (moveSpeed * transform.up), ForceMode.Acceleration);
        }
    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harpoon : MonoBehaviour
{
    [Header("Returning To Boat")]
    [SerializeField] private GameObject launcher;
    public float returnSpeed;
    public float distance;
    private float maxDistance;
    private bool returning;
    

    [Header("Hitting Rocks")]
    [SerializeField] private bool timerOn = true;
    private bool stopLooking;
    public Rigidbody rb;
    public GameObject currentRock;

    [Header("Collecting Iteams")]
    [SerializeField] bool collected;

    [Header("Rope")]
    public Transform ropePosition;

    private void Awake()
    {
        FindBoat();
        returnSpeed = launcher.GetComponent<CameraAim>().returnSpeed;
        maxDistance = launcher.GetComponent<CameraAim>().harpoonMaxDist;
    }

    void timer()
    {
        distance = Vector3.Distance(launcher.transform.position, transform.position);

        if (distance > maxDistance)
        {
            rb.velocity = Vector3.zero;
            ReturnToBoat();
        }

    }

    private void Update()
    {
        timer();
        transform.up = -(launcher.transform.position - transform.position);

        if (stopLooking == true)
        {
            rb.velocity = Vector3.zero;
            transform.position = Vector3.MoveTowards(transform.position, launcher.transform.position, returnSpeed * Time.deltaTime);
        }
    }

    public void FindBoat()
    {
        launcher = GameObject.FindGameObjectWithTag("HarpoonLauncher"); 
    }

    private void OnTriggerEnter(Collider other) //Hitiing the boats collection collider.
    {
        if (other.tag == "PlayerBoat" & returning == true|| stopLooking == true)
        {
            DestroyHarpoon();
        }
     
    }

    private void OnCollisionEnter(Collision collision) //Hitting rocks and other harpoonable objects.
    {
        if (collision.gameObject.tag == "Hookable" & stopLooking == false)
        {
            timerOn = false;
            Destroy(rb);
            launcher.GetComponent<CameraAim>().HarpoonHit(); //change for differnt aiming system 
            currentRock = collision.gameObject;
        }
        else if (collision.gameObject.tag == "Collectable" & collected == false)
        {
            timerOn = false;
            transform.SetParent(collision.transform);
        }
        
    }

    public void ReturnToBoat()
    {
        if (timerOn == false)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.useGravity = false;
            stopLooking = true;
            returning = true;
        }
        else
        {
            stopLooking = true;
        }
    }

    public void DestroyHarpoon()
    {
        //launcher.GetComponent<HarpoonAim>().HarpoonDead();
        launcher.GetComponent<CameraAim>().HarpoonDead();
        Destroy(gameObject);
    }

    
}


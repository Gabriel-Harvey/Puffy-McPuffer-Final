

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    public Transform boat;
    [SerializeField] public bool hooked;
    private float speed;
    [SerializeField] Harpoon harpoon;
    [SerializeField] Transform lockPosition;
    [SerializeField] private bool locked;
    [SerializeField] private float rotateSpeed;
    public int questScore;
    private SelectionManager selection;

    private void Awake()
    {
        selection = gameObject.GetComponent<SelectionManager>();
    }
    private void Update()
    {
        if (hooked == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, boat.position, speed * Time.deltaTime);
        }

    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Harpoon")
        {
            harpoon = collision.gameObject.GetComponentInParent<Harpoon>();
            speed = harpoon.returnSpeed;
            hooked = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerBoat" & hooked == true)
        {
           
            if (other.GetComponentInChildren<CameraAim>().Stored == false)
            {
                ConnectToBoat(other.attachedRigidbody, other.gameObject);

                questScore += 1;
                print(questScore);
                other.GetComponentInChildren<CameraAim>().Stored = true;
            }
            else 
            {
                questScore += 1;
                print(questScore);

                harpoon.DestroyHarpoon();
                Invisble();
            }

            
        }
        
    }

    public void ConnectToBoat(Rigidbody boatRB, GameObject boat)
    {
        //Harpoon
        hooked = false;
        harpoon.DestroyHarpoon();

        //Pull Position
        lockPosition = boat.GetComponentInChildren<CameraAim>().collecionArea;
        transform.position = lockPosition.position;
        transform.LookAt(new Vector3(boat.transform.position.x, transform.position.y, boat.transform.position.z));


        //Selection Marker
        if (selection.active == true)
        {
            selection.selectedMarker.SetActive(false);
        }
        Destroy(selection);

        //Rigidbody
        Rigidbody RB = gameObject.GetComponent<Rigidbody>();
        RB.isKinematic = false;

        //Adding Hinge
        HingeMangement(boatRB);
    }

    public void HingeMangement(Rigidbody boat)
    {
        HingeJoint hinge = gameObject.GetComponent<HingeJoint>();

        //Boat Connection
        hinge.connectedBody = boat;
    }

    private void Invisble()
    {
        gameObject.SetActive(false);
    }
}




using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    public GameObject boat;
    [SerializeField] public bool hooked;
    private float speed;
    [SerializeField] Harpoon harpoon;
    [SerializeField] Transform lockPosition;
    [SerializeField] private bool locked;
    [SerializeField] private float rotateSpeed;
    public bool questScore;
    private SelectionManager selection;
    public CollectableBoat interacted;
    public GameObject cargoBoatBody;

    [Header("Hooking onto Boat")]
    private float distance;
    public float collectDist;

    private void Awake()
    {
        cargoBoatBody = GetComponent<GameObject>();
        selection = gameObject.GetComponent<SelectionManager>();
    }
    private void Update()
    {
        if (interacted.interactedCollectibles == true)
        {
            cargoBoatBody.SetActive(true);
        }
        if (hooked == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, boat.transform.position, speed * Time.deltaTime);
            distance = Vector3.Distance(boat.transform.position, transform.position);

            if (distance < collectDist && hooked == true)
            {
                if (boat.GetComponentInChildren<CameraAim>().Stored == false)
                {
                    ConnectToBoat(boat.GetComponent<Rigidbody>(), boat.gameObject);

                    questScore = true;
                    print(questScore);
                    boat.gameObject.GetComponentInChildren<CameraAim>().Stored = true;
                    boat.gameObject.GetComponentInChildren<CameraAim>().target = null;
                }
                else
                {
                    boat.gameObject.GetComponentInChildren<CameraAim>().target = null;

                    questScore = true;
                    print(questScore);

                    harpoon.DestroyHarpoon();
                    Invisble();
                }
            }        
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


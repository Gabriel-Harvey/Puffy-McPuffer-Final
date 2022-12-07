using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAim : MonoBehaviour
{

    [Header("Harpoon Aiming")]
    public Transform target;
    public Transform DefaultTarget;

    [Header("Firing")]
    public GameObject harpoonPrefab;
    [SerializeField] private GameObject currentHarpoon;
    public Transform spawnPoint;
    public bool readyToFire = true;

    [Header("Harpoon Speed Values")]
    public float launchSpeed;
    public float returnSpeed;


    [Header("Moving Towards Harpoon")]
    [SerializeField] private bool hit;
    public HarpoonBoatMovement movement;

    //[Header("Rope")]
    //private LineRenderer lr;

    [Header("Collectables")]
    public Transform collecionArea;
    public bool Stored;



    private void Awake()
    {
        //lr = GetComponent<LineRenderer>();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0) & readyToFire == true)
        {
            Fire();
            readyToFire = false;
        }
        else if (Input.GetMouseButtonUp(0) & readyToFire == false)
        {
            currentHarpoon.GetComponent<Harpoon>().ReturnToBoat();
        }


        if (target != null)
        {
            //transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
            transform.LookAt(target);
        }
        else
        {
            transform.LookAt(new Vector3(DefaultTarget.position.x, transform.position.y, DefaultTarget.position.z));
        }

        
    }

    private void LateUpdate()
    {
        //DrawRope();
    }

    public void Fire()
    {
        var harpoon = Instantiate(harpoonPrefab, spawnPoint.position, spawnPoint.rotation);
        harpoon.GetComponent<Rigidbody>().velocity = spawnPoint.forward * launchSpeed;
        currentHarpoon = harpoon;
    }

    public void HarpoonHit()
    {
        movement.Actived(currentHarpoon);
    }

    /*void DrawRope()
    {
        if (readyToFire) return;

        lr.positionCount = 2;
        lr.SetPosition(0, spawnPoint.position);
        lr.SetPosition(1, currentHarpoon.GetComponent<Harpoon>().ropePosition.position);


    }*/

    public void HarpoonDead()
    {
        readyToFire = true;
        //lr.positionCount = 0;
        movement.moving = false;
    }
    


}

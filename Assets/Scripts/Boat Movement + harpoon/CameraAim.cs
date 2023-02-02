using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAim : MonoBehaviour
{

    [Header("Harpoon Aiming")]
    public Transform target;
    public Transform defaultTarget;
    private Camera cam;
    public float rayDistance;
    public LayerMask layer;
    public SelectionManager currentTarget;

    [Header("Firing")]
    public GameObject harpoonPrefab;
    [SerializeField] private GameObject currentHarpoon;
    public Transform spawnPoint;
    public bool readyToFire = true;
    public float harpoonMaxDist;

    [Header("Harpoon Speed Values")]
    public float launchSpeed;
    public float returnSpeed;


    [Header("Moving Towards Harpoon")]
    [SerializeField] private bool hit;
    public HarpoonBoatMovement movement;

    [Header("Collectables")]
    public Transform collecionArea;
    public bool Stored;



    private void Awake()
    {
        cam = Camera.main;
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
            transform.LookAt(new Vector3(defaultTarget.position.x, transform.position.y, defaultTarget.position.z));
        }

        
    }

    private void FixedUpdate()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, rayDistance, layer))
        {
            currentTarget = hitInfo.collider.GetComponent<SelectionManager>();
            currentTarget.selectedMarker.SetActive(true);
            target = hitInfo.collider.GetComponent<SelectionManager>().harpoonLockPos;
        }
        else if (currentTarget != null)
        {
            target = null;
            currentTarget.selectedMarker.SetActive(false);
            currentTarget = null;
        }
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

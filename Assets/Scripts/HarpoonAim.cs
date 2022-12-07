

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class HarpoonAim : MonoBehaviour
{
    [Header("Aiming")]
    public Transform facing;
    public Camera cam;
    private Color color = Color.red;

    [Header("Firing")]
    public GameObject harpoonPrefab;
    [SerializeField] private GameObject currentHarpoon;
    public Transform spawnPoint;
    public float speed = 10f;
    public float returnSpeed = 10f;
    public bool readyToFire = true;

    [Header("Moving Towards Harpoon")]
    [SerializeField] private bool hit;
    public HarpoonBoatMovement movement;

    [Header("Items")]
    public Transform collecionArea;
    

    [Header("Rope")]
    private LineRenderer lr;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) & readyToFire == true)
        {
            Fire();
            readyToFire = false;
        }
        else if (Input.GetKeyDown(KeyCode.Space) & readyToFire == false)
        {
            currentHarpoon.GetComponent<Harpoon>().ReturnToBoat();
        }

        if (readyToFire == false)
        {
            transform.LookAt(transform.position -(currentHarpoon.transform.position - transform.position));
        }
        else if (readyToFire == true)
        {
            transform.LookAt(new Vector3(facing.position.x, transform.position.y, facing.position.z));
        }
    }

    private void LateUpdate()
    {
        DrawRope();
    }

    public void Fire()
    {
        var harpoon = Instantiate(harpoonPrefab, spawnPoint.position, spawnPoint.rotation);
        harpoon.GetComponent<Rigidbody>().velocity = spawnPoint.up * speed;
        currentHarpoon = harpoon;
    }

    public void HarpoonDead()
    {
        readyToFire = true;
        lr.positionCount = 0;
        movement.moving = false;
    }

    public void HarpoonHit()
    {
        Debug.Log("HarpoonHit called");
        movement.Actived(currentHarpoon);
    }

    void DrawRope()
    {
        if (readyToFire) return;

        lr.positionCount = 2;
        lr.SetPosition(0, spawnPoint.position);
        lr.SetPosition(1, currentHarpoon.GetComponent<Harpoon>().ropePosition.position);

        
    }
}


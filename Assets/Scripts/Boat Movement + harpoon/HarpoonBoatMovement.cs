

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonBoatMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private GameObject harpoon;
    [SerializeField] private float speed;
    [SerializeField] private float pullSpeed;
    [SerializeField] private float maxMoveSpeed;
    public bool moving;
    public float rotateSpeed = 5f;
    public Rigidbody rb;

    [Header("Collider Management")]
    public CameraAim cameraAim;
    private new CapsuleCollider collider;
    private bool created;
    [SerializeField] private float radius;
    [SerializeField] private float height;


    void Update()
    {
        if (moving == true) //Moving towards target.
        {
            Vector3 target = new Vector3(harpoon.transform.position.x, transform.position.y , harpoon.transform.position.z);
            //transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime); //*harpoon.transform.position
            Vector3 direction = target - transform.position;
            rb.AddForce(direction/100 * pullSpeed, ForceMode.Impulse);

            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxMoveSpeed);


            var targetRotation = Quaternion.LookRotation(target - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        }

        if (cameraAim.readyToFire == true && created == true)
        {
            Destroy(GetComponent<CapsuleCollider>());
            created = false;
        }
        else if (cameraAim.readyToFire == false && created == false)
        {
            collider = gameObject.AddComponent<CapsuleCollider>();
            collider.direction = 2;
            collider.isTrigger = true;
            collider.radius = radius;
            collider.height = height;
            created = true;
        }
    }

    public void Actived(GameObject poon)
    {
        harpoon = poon;
        moving = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hookable" & moving == true)
        {
            moving = false;
            harpoon.GetComponent<Harpoon>().DestroyHarpoon();
        }
    }
}


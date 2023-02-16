using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectBoatMovement : MonoBehaviour
{
    [Header("Locating Target")]
    [SerializeField] private int currentLocation;
    [SerializeField] private int currentTarget;
    [SerializeField] private int tempTarget;
    [SerializeField] private int lastHit;
    [SerializeField] private Transform[] waypoints;

    [Header("Moving Boat")]
    public float speed;
    public float rSpeed;
    [SerializeField] private bool moveForwards;
    [SerializeField] private bool moveBackwards;

    [Header("UI Comunication")]
    public LevelSelectUI ui;

    private void Update()
    {
        if (moveForwards)
        {
            if (lastHit == currentTarget)
            {
                moveForwards = false;
                currentLocation = lastHit;
                tempTarget = 0;
                ui.ActivateButtons();
                Debug.Log("Reached destination");
            }
            else 
            {
                float distance = Vector3.Distance(transform.position, waypoints[tempTarget].position);

                if (distance > 0.5)
                {
                    transform.position = Vector3.MoveTowards(transform.position, waypoints[tempTarget].position, speed * Time.deltaTime);
                }
                else
                {
                    lastHit = tempTarget;
                    tempTarget++;
                }
            }   
        }

        if (moveBackwards)
        {
            if (lastHit == currentTarget )
            {
                moveBackwards = false;
                currentLocation = lastHit;
                tempTarget = 0;
                ui.ActivateButtons();
                Debug.Log("Reached destination");
            }
            else
            {
                float distance = Vector3.Distance(transform.position, waypoints[tempTarget].position);

                if (distance > 0.5)
                {
                    transform.position = Vector3.MoveTowards(transform.position, waypoints[tempTarget].position, speed * Time.deltaTime);
                }
                else
                {
                    lastHit = tempTarget;
                    tempTarget--;
                }
            }
        } 
    }

    public void setTarget(int target)
    {
        currentTarget = target;

        if (currentTarget == currentLocation) //At location so stay.
        {
            Debug.Log("At location");
        }
        else if (currentTarget != currentLocation) //Not at location so move.
        {
            ui.DisableButtons();

            if (currentTarget > currentLocation) //Fowards
            {
                transform.position = waypoints[currentLocation].position;
                tempTarget = currentLocation + 1;
                moveForwards = true;
            }
            else if (currentTarget < currentLocation) //Backwards
            {
                transform.position = waypoints[currentLocation].position;
                tempTarget = currentLocation;
                moveBackwards = true;
            }
        }
            
    }
}

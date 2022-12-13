using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBoat : MonoBehaviour
{
    public Collectables[] collected;
    public GameObject boat;
    public bool interactedCollectibles = false;
    public bool allCollected;
    public bool boatMove = false;
    public bool boatWaypointActivated = false;
    public bool cargoWaypointActivated = false;
    public bool isActive;
    public bool questFinished = false;
    public GameObject interactImage;
    public GameObject cargoBoatDialogue;
    public GameObject cargoBoatBody;
    public PaintQuest collectedCheck;

    [SerializeField]
    public GreenBoatFollow followBoatCheck;
    [SerializeField]
    public PurpleBoatRacing raceBoatCheck;

    public Waypoint waypointMark;
    public GameObject[] waypoints;
    int current = 0;
    float speed = 20f;
    float turnSpeed = 20f;
    float waypointRadius = 5f;


    [SerializeField]
    private KeyCode interactButton;

    [Header("Cargo Activation")]
    public GameObject cargo1;
    public GameObject cargo2;
    private bool isCargoActive;

    private void Start()
    {
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (collectedCheck.isCollected == true)
        {
            if (raceBoatCheck.isActive == false && followBoatCheck.isActive == false)
            {
                boatWaypointActivated = true;
                waypointMark.CollectBoatWaypoint();
            }
            else
            {
                boatWaypointActivated = false;
                waypointMark.CollectBoatWaypoint();
            }


            if (interactedCollectibles == false)
            {
                cargoBoatBody.SetActive(false);
            }

            if (Vector3.Distance(transform.position, boat.transform.position) < 30 && interactedCollectibles == false)
            {
                interactImage.SetActive(true);
                if (Input.GetKeyDown(interactButton))
                {
                    GetComponent<DialogueTrigger>().TriggerDialogue();
                    interactedCollectibles = true;
                }
            }
        }

        if (interactedCollectibles == true)
        {
            cargoWaypointActivated = true;
            waypointMark.CollectableWaypoints();
            boatWaypointActivated = false;
            waypointMark.CollectBoatWaypoint();
            interactImage.SetActive(false);
            cargoBoatBody.SetActive(true);
            allCollected = true;
            isActive = true;
            for (int i = 0; i < collected.Length; i++)
            {
                if (collected[i].questScore == true)
                {
                    cargoWaypointActivated = false;
                    waypointMark.CollectableWaypoints();
                }
                if (collected[i].questScore == false)
                {
                    allCollected = false;
                    break;
                }
            }
        }

        if (allCollected == true)
        {
            boatWaypointActivated = true;
            waypointMark.CollectBoatWaypoint();
            cargoWaypointActivated = false;
            waypointMark.CollectableWaypoints();
        }

        if (Vector3.Distance(transform.position, boat.transform.position) < 30 && interactedCollectibles == true)
        {
            if (allCollected == true)
            {
                interactImage.SetActive(true);
                if (Input.GetKeyDown(interactButton))
                {
                    cargoBoatDialogue.GetComponent<DialogueTrigger>().TriggerDialogue();
                    interactImage.SetActive(false);
                    boatMove = true;
                    boat.GetComponentInChildren<CameraAim>().Stored = false;
                    for (int i = 0; i < collected.Length; i++)
                    {
                        collected[i].gameObject.SetActive(false);
                    }
                }
            }
        }

        if (boatMove == true)
        {
            boatWaypointActivated = false;
            waypointMark.CollectBoatWaypoint();
            StartCoroutine(GoToEnd());
        }

        if(questFinished == true)
        {
            boatWaypointActivated = false;
            waypointMark.CollectBoatWaypoint();
            cargoWaypointActivated = false;
            waypointMark.CollectableWaypoints();
            isActive = false;
        }
    }

    IEnumerator GoToEnd()
    {
        yield return new WaitForSeconds(0.1f);

        if (Vector3.Distance(waypoints[current].transform.position, transform.position) < waypointRadius)
        {
            current++;
            if (current == waypoints.Length)
            {
                current = 0;
                questFinished = true;
                boatMove = false;
                StopCoroutine(GoToEnd());
            }
        }
        if (questFinished == false)
        {
            if (Vector3.Distance(waypoints[current].transform.position, transform.position) > waypointRadius)
            {
                transform.position = Vector3.MoveTowards(transform.position, waypoints[current].transform.position, Time.deltaTime * speed);
                transform.rotation = Quaternion.Slerp(transform.rotation
                        , Quaternion.LookRotation(waypoints[current].transform.position - transform.position)
                        , turnSpeed * Time.deltaTime);
            }
        }
    }
}

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
    public bool waypointActivated = false;
    public bool isActive;
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

    private void Start()
    {
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(collectedCheck.isCollected == true)
        {
            waypointActivated = true;
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

        if (interactedCollectibles == true)
        {
            waypointMark.CollectableWaypoints();
            interactImage.SetActive(false);
            cargoBoatBody.SetActive(true);
            allCollected = true;
            isActive = true;
            for (int i = 0; i < collected.Length; i++)
            {
                if (collected[i].questScore == false)
                {
                    allCollected = false;
                    break;
                }
            }
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
                    cargoBoatBody.SetActive(false);
                }
            }
        }

        if (boatMove == true)
        {
            StartCoroutine(GoToEnd());
        }

        if(raceBoatCheck.isActive == true || followBoatCheck.isActive == true)
        {
            waypointActivated = false;
            waypointMark.CollectBoatWaypoint();
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
                StopCoroutine(GoToEnd());
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, waypoints[current].transform.position, Time.deltaTime * speed);
        transform.rotation = Quaternion.Slerp(transform.rotation
                , Quaternion.LookRotation(waypoints[current].transform.position - transform.position)
                , turnSpeed * Time.deltaTime);
    }
}

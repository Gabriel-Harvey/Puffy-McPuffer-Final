using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleBoatRacing : MonoBehaviour
{
    public bool raceAllowed = false;
    public GameObject interactImage;
    public Transform transformPlayer;
    public BoatMovement playerRaceCondition;
    public Waypoint waypointMark;
    public PaintQuest collectedCheck;
    public float raceSpeed = 30f;
    public float raceTurnSpeed = 20f;
    public float countdown = 5f;
    public float waypointRadius;
    public int countdownInt;
    public int current = 0;
    public bool reachedPoint = false;
    public bool finishedQuest = false;
    public bool startCounter = false;
    public bool checkpointPassed = false;
    public bool boatWaypointActivated;
    public bool goalWaypointActivated;
    public bool isActive;
    public GameObject raceGoal;
    public GameObject startPos;
    public GameObject[] waypoints;
    public GameObject[] raceColliders;
    Vector3 velocityBoat;

    [SerializeField]
    GreenBoatFollow followBoatCheck;
    [SerializeField]
    CollectableBoat collectBoatCheck;

    [SerializeField]
    private KeyCode raceButton;
    public UiManager raceUI;

    // Start is called before the first frame update
    void Start()
    {
        // raceGoal.SetActive(false);
        boatWaypointActivated = false;
        goalWaypointActivated = false;
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (collectedCheck.isCollected == true)
        {
            if (followBoatCheck.isActive == false && collectBoatCheck.isActive == false)
            {
                boatWaypointActivated = true;
                waypointMark.RaceWaypoint();
            }
            else
            {
                boatWaypointActivated = false;
                waypointMark.RaceWaypoint();
            }

            if (raceAllowed == true)
            {
                raceGoal.SetActive(true);
                boatWaypointActivated = false;
                goalWaypointActivated = true;
                isActive = true;
                waypointMark.RaceWaypoint();
                waypointMark.RaceGoalWaypoint();
                if (reachedPoint == false)
                {
                    transform.position = Vector3.MoveTowards(transform.position, raceGoal.transform.position, Time.deltaTime * raceSpeed);
                    transform.rotation = Quaternion.Slerp(transform.rotation
                    , Quaternion.LookRotation(raceGoal.transform.position - transform.position)
                    , raceTurnSpeed * Time.deltaTime);
                }
            }

            if (isActive == true)
            {
                for (int i = 0; i < raceColliders.Length; i++)
                {
                    raceColliders[i].SetActive(true);
                }
            }
            else
            {
                for (int i = 0; i < raceColliders.Length; i++)
                {
                    raceColliders[i].SetActive(false);
                }
            }
            
            if (raceAllowed == false && Vector3.Distance(transform.position, transformPlayer.position) < 20)
            {
                if (playerRaceCondition.wonRace == false)
                {
                    interactImage.SetActive(true);
                    if (Input.GetKeyDown(raceButton))
                    {
                        gameObject.transform.position = new Vector3(-390, 25, 260);
                        gameObject.transform.eulerAngles = new Vector3(0, 115, 0);
                        transformPlayer.position = new Vector3(-406, 25, 240);
                        transformPlayer.eulerAngles = new Vector3(0, 110, 0);
                        startCounter = true;

                        interactImage.SetActive(false);
                        StartCoroutine(Wait());
                    }
                    if (startCounter == true)
                    {
                        raceUI.countdownRace.enabled = true;
                        if (countdown >= 0)

                            countdown -= Time.deltaTime;
                        countdownInt = Mathf.RoundToInt(countdown);
                        raceUI.countdownRace.text = "Race starts in... " + countdownInt.ToString() + "!";
                    }
                    else
                    {


                        countdown = 5;
                    }
                }
            }

            if (raceAllowed == true || Vector3.Distance(transform.position, transformPlayer.position) > 20)
            {
                interactImage.SetActive(false);
                boatWaypointActivated = false;
            }

            if (reachedPoint == true)
            {
                raceGoal.SetActive(false);
                goalWaypointActivated = false;
                waypointMark.RaceGoalWaypoint();
                StartCoroutine(StartAgain());
            }
            if (playerRaceCondition.wonRace == true)
            {
                raceGoal.SetActive(false);
                raceAllowed = false;
                isActive = false;
                boatWaypointActivated = false;
                waypointMark.RaceWaypoint();
                goalWaypointActivated = false;
                waypointMark.RaceGoalWaypoint();
                StartCoroutine("Lost");
            }
        }
    }

    public IEnumerator Wait()
    {
        GetComponent<DialogueTrigger>().TriggerDialogue();
        yield return new WaitForSeconds(5);
        raceAllowed = true;
        startCounter = false;
        raceUI.countdownRace.enabled = false;
    }
    public IEnumerator Lost()
    {
        yield return new WaitForSeconds(0.1f);
        if (Vector3.Distance(waypoints[current].transform.position, transform.position) < waypointRadius)
        {
            current++;
            if (current == waypoints.Length)
            {
                current = 0;
                StopCoroutine("Lost");
                finishedQuest = true;
            }
        }
        if (finishedQuest == false)
        {
            if (Vector3.Distance(waypoints[current].transform.position, transform.position) > waypointRadius)
            {
                transform.position = Vector3.MoveTowards(transform.position, waypoints[current].transform.position, Time.deltaTime * raceSpeed);
                transform.rotation = Quaternion.Slerp(transform.rotation
                        , Quaternion.LookRotation(waypoints[current].transform.position - transform.position)
                        , raceTurnSpeed * Time.deltaTime);
            }
        }
    }

    public IEnumerator StartAgain()
    {

        transform.position = Vector3.MoveTowards(transform.position, startPos.transform.position, Time.deltaTime * raceSpeed);
        transform.rotation = Quaternion.Slerp(transform.rotation
                , Quaternion.LookRotation(startPos.transform.position - transform.position)
                , raceTurnSpeed * Time.deltaTime);
        yield return new WaitForSeconds(0.1f);

        if (Vector3.Distance(startPos.transform.position, transform.position) < waypointRadius)
        {
            {
                reachedPoint = false;
                raceAllowed = false;
                boatWaypointActivated = false;
                transform.rotation = Quaternion.Slerp(transform.rotation
                    , Quaternion.LookRotation(raceGoal.transform.position - transform.position)
                    , raceTurnSpeed * Time.deltaTime);
                yield return new WaitForSeconds(0.1f);
                StopCoroutine(StartAgain());
            }

        }
    }
    

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Goal")
        {
            other.gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();
            reachedPoint = true;
        }
    }
}

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
    public bool waypointActivated;
    public GameObject raceGoal;
    public GameObject raceIndicator;
    public GameObject startPos;
    public GameObject[] waypoints;
    Vector3 velocityBoat;

    [SerializeField]
    private KeyCode raceButton;
    public UiManager raceUI;

    // Start is called before the first frame update
    void Start()
    {
        // raceGoal.SetActive(false);
        waypointActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerRaceCondition.rubbleTriggered == true)
        {
            waypointActivated = true;
            waypointMark.RaceWaypoint();

            if (raceAllowed == true)
            {
                raceGoal.SetActive(true);
                raceIndicator.SetActive(true);
                if (reachedPoint == false)
                {
                    transform.position = Vector3.MoveTowards(transform.position, raceGoal.transform.position, Time.deltaTime * raceSpeed);
                    transform.rotation = Quaternion.Slerp(transform.rotation
                    , Quaternion.LookRotation(raceGoal.transform.position - transform.position)
                    , raceTurnSpeed * Time.deltaTime);
                }
            }
            
            if (raceAllowed == false && Vector3.Distance(transform.position, transformPlayer.position) < 20)
            {
                if (playerRaceCondition.wonRace == false)
                {
                    interactImage.SetActive(true);
                    if (Input.GetKeyDown(raceButton))
                    {
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
                waypointActivated = false;
            }

            if (reachedPoint == true)
            {
                raceGoal.SetActive(false);
                raceIndicator.SetActive(false);
                StartCoroutine(StartAgain());
            }
            if (playerRaceCondition.wonRace == true)
            {
                raceGoal.SetActive(false);
                raceAllowed = false;
                StartCoroutine(Lost());
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
        //insert point for where boat should go when done
        if (Vector3.Distance(waypoints[current].transform.position, transform.position) < waypointRadius)
        {
            current++;
            if (current == waypoints.Length)
            {
                StopCoroutine(Lost());
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, waypoints[current].transform.position, Time.deltaTime * raceSpeed);
        transform.rotation = Quaternion.Slerp(transform.rotation
                , Quaternion.LookRotation(waypoints[current].transform.position - transform.position)
                , raceTurnSpeed * Time.deltaTime);
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
                waypointActivated = false;
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

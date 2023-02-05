using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBoatFollow : MonoBehaviour
{
    public float followerSpeed = 10f;
    public float followerTurningSpeed = 10f;
    public Transform transformPlayer;
    public bool followAllowed = false;
    public bool reachedPoint = false;
    public bool boatWaypointActivated;
    public bool isActive;
    public GameObject interactImage;
    public PaintQuest collectedCheck;

    [SerializeField]
    public PurpleBoatRacing raceBoatCheck;
    [SerializeField]
    CollectableBoat collectBoatCheck;

    public Waypoint waypointMark;

    [SerializeField]
    private KeyCode followButton;

    // Start is called before the first frame update
    void Start()
    {
        boatWaypointActivated = false;
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (collectedCheck.isCollected == true)
        {
            if (raceBoatCheck.isActive == false && collectBoatCheck.isActive == false)
            {
                boatWaypointActivated = true;
                waypointMark.FollowWaypoint();
            }
            else
            {
                boatWaypointActivated = false;
                waypointMark.FollowWaypoint();
            }

            if (followAllowed == true)
            {
                boatWaypointActivated = false;
                isActive = true;
                waypointMark.FollowWaypoint();
                if (Vector3.Distance(transform.position, transformPlayer.position) < 30 && Vector3.Distance(transform.position, transformPlayer.position) > 10)
                {
                    if (reachedPoint == false)
                    {
                        Follow();
                    }
                }
            }
            if (Vector3.Distance(transform.position, transformPlayer.position) < 30 && followAllowed == false)
            {
                    interactImage.SetActive(false);
                    GetComponent<DialogueTrigger>().TriggerDialogue();
                    followAllowed = true;

            }
            //if (vector3.distance(transform.position, transformplayer.position) > 20 || followallowed == true)
            //{
            //    interactimage.setactive(false);
            //}
        }

        if (reachedPoint == true)
        {
            isActive = false;
        }
    }

    void Follow()
    {
        // Look at Player
        transform.rotation = Quaternion.Slerp(transform.rotation
        , Quaternion.LookRotation(transformPlayer.position - transform.position)
        , followerTurningSpeed * Time.deltaTime);

        // Move at Player
        transform.position += transform.forward * followerSpeed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Point")
        {
            other.gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();
            reachedPoint = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Point")
        {
            reachedPoint = false;
        }
    }
}

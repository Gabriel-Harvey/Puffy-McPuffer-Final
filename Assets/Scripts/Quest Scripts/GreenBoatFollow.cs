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
    public bool waypointActivated;
    public GameObject interactImage;
    public PaintQuest collectedCheck;
    public Waypoint waypointMark;

    [SerializeField]
    private KeyCode followButton;

    // Start is called before the first frame update
    void Start()
    {
        waypointActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (collectedCheck.isCollected == true)
        {
            waypointActivated = true;
            waypointMark.FollowWaypoint();

            if (followAllowed == true)
            {
                waypointActivated = false;
                waypointMark.PaintWaypoint();
                if (Vector3.Distance(transform.position, transformPlayer.position) < 30 && Vector3.Distance(transform.position, transformPlayer.position) > 10)
                {
                    if (reachedPoint == false)
                    {
                        Follow();
                    }
                }
            }
            if (Vector3.Distance(transform.position, transformPlayer.position) < 20 && followAllowed == false)
            {
                interactImage.SetActive(true);
                if (Input.GetKeyDown(followButton))
                {
                    interactImage.SetActive(false);
                    GetComponent<DialogueTrigger>().TriggerDialogue();
                    followAllowed = true;
                }

            }
            if (Vector3.Distance(transform.position, transformPlayer.position) > 20 || followAllowed == true)
            {
                interactImage.SetActive(false);
            }
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

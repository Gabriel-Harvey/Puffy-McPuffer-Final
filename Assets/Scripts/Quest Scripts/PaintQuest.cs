using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintQuest : MonoBehaviour
{

    public bool isCollected;
    public Collectables paintBoat;
    public GameObject paintBoatBody;
    public GameObject playerBoat;
    public Waypoint paintWaypoint;

    public GameObject interactImage;
    public bool waypointActivated;

    [SerializeField]
    private KeyCode collectButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (paintBoat.questScore == false)
        {
            waypointActivated = true;
            paintWaypoint.PaintWaypoint();
        }

        if (paintBoat.questScore == true)
        {
            waypointActivated = false;
            paintWaypoint.PaintWaypoint();
        }

        if (paintBoat.questScore == true && Vector3.Distance(transform.position, playerBoat.transform.position) < 150)
        {
            interactImage.SetActive(true);
            if (Input.GetKeyDown(collectButton))
            {
                GetComponent<DialogueTrigger>().TriggerDialogue();
                isCollected = true;
            }
        }

        if (isCollected == true)
        {
            interactImage.SetActive(false);
            Destroy(paintBoatBody);
        }
    }
}

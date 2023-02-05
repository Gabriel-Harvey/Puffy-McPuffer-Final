using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintQuest : MonoBehaviour
{

    public bool isCollected;
    public Collectables paintBoat;
    public GameObject paintBoatBody;
    public GameObject playerBoat;
    public GameObject[] canalPartySetup;
    public Waypoint paintWaypoint;
    public GameObject boat;

    public GameObject interactImage;
    public bool waypointActivated;
    public bool quitGame = false; 
    public FinishQuest gameComplete;
    public GameObject winner;
    public GameObject tint;

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

        if (paintBoat.questScore == true && Vector3.Distance(transform.position, playerBoat.transform.position) < 150 && isCollected == false)
        {
                GetComponent<DialogueTrigger>().TriggerDialogue();
                isCollected = true;
                boat.GetComponentInChildren<CameraAim>().Stored = false;
        }

        if (isCollected == true)
        {
            interactImage.SetActive(false);
            canalPartySetup[0].SetActive(false);
            canalPartySetup[1].SetActive(true);
            Destroy(paintBoatBody);
        }

    
            if (gameComplete.rubbleCleared == true)
            {
                canalPartySetup[1].SetActive(false);
                canalPartySetup[2].SetActive(true);

                if (Vector3.Distance(transform.position, playerBoat.transform.position) < 150)
                {
                    interactImage.SetActive(true);
                    if (Input.GetKeyDown(collectButton))
                    {
                        canalPartySetup[2].GetComponent<DialogueTrigger>().TriggerDialogue();
                        interactImage.SetActive(false);
                        quitGame = true;
                    }
                }
            }

        if (quitGame == true)
        {
            StartCoroutine(QuitGame());
        }
    }

    IEnumerator QuitGame()
    {
        yield return new WaitForSeconds(0.5f);
        tint.SetActive(true);
        winner.SetActive(true);
    }
}

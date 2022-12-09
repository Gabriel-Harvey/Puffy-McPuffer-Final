using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBoat : MonoBehaviour
{
    public Collectables[] collected;
    public GameObject boat;
    public bool interactedCollectibles = false;
    public bool allCollected;
    public GameObject interactImage;
    public GameObject cargoBoat;
    public BoatMovement questTrigger;
    public Waypoint waypointMark;

    [SerializeField]
    private KeyCode interactButton;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        if (Vector3.Distance(transform.position, boat.transform.position) < 20 && interactedCollectibles == false)
        {
            interactImage.SetActive(true);
            if (Input.GetKeyDown(interactButton))
            {
                GetComponent<DialogueTrigger>().TriggerDialogue();
                interactedCollectibles = true;
                interactImage.SetActive(false);
            }
        }

        if (interactedCollectibles == true)
        {
            allCollected = true;
            for (int i = 0; i < collected.Length; i++)
            {
                if (collected[i].questScore == false)
                {
                    allCollected = false;
                    break;
                }
            }
        }

        if (Vector3.Distance(transform.position, boat.transform.position) < 20 && interactedCollectibles == true)
        {
                if (allCollected == true)
                {
                    interactImage.SetActive(true);
                    if (Input.GetKeyDown(interactButton))
                    {
                        cargoBoat.GetComponent<DialogueTrigger>().TriggerDialogue();
                        interactImage.SetActive(false);
                    }
                }
        }
    }
}

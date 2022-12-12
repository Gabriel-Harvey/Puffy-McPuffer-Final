using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishQuest : MonoBehaviour
{
    public PurpleBoatRacing purpleBoatFinish;
    public GreenBoatFollow greenBoatFinish;
    public CollectableBoat collectableFinish;
    public bool greenFinished = false;
    public bool purpleFinished = false;
    public bool collectableFinished = false;
    public Transform transformPlayer;
    public GameObject interactImage;
    public GameObject[] rubble;

    public bool everythingFinished = false;
    public bool rubbleCleared = false;
    [SerializeField]
    private KeyCode finishInteract;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (purpleBoatFinish.finishedQuest == true)
        {
            purpleFinished = true;
        }

        if (greenBoatFinish.reachedPoint == true)
        {
            greenFinished = true;
        }

        if(collectableFinish.questFinished == true)
        {
            collectableFinished = true;
        }

        if (purpleFinished == true && greenFinished == true && collectableFinished == true)
        {
            everythingFinished = true;
        }

        if (everythingFinished == true && Vector3.Distance(transform.position, transformPlayer.position) < 50)
        {
            interactImage.SetActive(true);
            if (Input.GetKey(finishInteract))
            {
                interactImage.SetActive(false);
                for (int i = 0; i < rubble.Length; i++)
                {
                    rubble[i].SetActive(false);
                    rubbleCleared = true;
                    Application.Quit();
                    //Insert animation script here
                }
                
            }
        }
    }
}


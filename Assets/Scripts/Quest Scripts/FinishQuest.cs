using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishQuest : MonoBehaviour
{
    public PurpleBoatRacing purpleBoatFinish;
    public GreenBoatFollow greenBoatFinish;
    public bool greenFinished = false;
    public bool purpleFinished = false;
    public Transform transformPlayer;
    public GameObject interactImage;

    public bool everythingFinished = false;
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

        if (purpleFinished == true && greenFinished == true)
        {
            everythingFinished = true;
        }

        if (everythingFinished == false && Vector3.Distance(transform.position, transformPlayer.position) < 50)
        {
            interactImage.SetActive(true);
            if (Input.GetKey(finishInteract))
            {
                interactImage.SetActive(false);
                gameObject.SetActive(false);
            }
        }
    }
}


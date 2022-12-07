using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UiManager : MonoBehaviour
{
    public GameObject questMenu;
    [SerializeField] bool menuUp;
    [SerializeField] Animator QuestMenuAnimator;
    [SerializeField] Collectables[] questScore;
    [SerializeField] Sprite[] questIconsArray;
    [SerializeField] Image[] questIconsImage;
    [SerializeField] public Text countdownRace;
    public GameObject[] crates;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            questScore[i] = crates[i].GetComponent<Collectables>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) & menuUp == false)
        {
            menuUp = true;
            QuestMenuAnimator.SetBool("MenuUp", true);
        }
        else if (Input.GetKeyDown(KeyCode.Tab) & menuUp == true)
        {
            menuUp = false;
            QuestMenuAnimator.SetBool("MenuUp", false);
        }

        for  (int i = 0; i < 3; i++)
        {
            if (questScore[i].questScore >= 1)
            {
                questIconsImage[i].sprite = questIconsArray[1];
            }
        }
    }
}


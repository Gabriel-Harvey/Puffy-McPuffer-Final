using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectUI : MonoBehaviour
{
    [Header("Button Identifing")]
    public Button[] buttons;
    private GameObject currentPlayButton;
    private int i;

    [Header("Comunicating With Boat")]
    public LevelSelectBoatMovement boat;
    public GameObject[] startButtons;

    public void Awake()
    {
        currentPlayButton = startButtons[0];
        Cursor.lockState = CursorLockMode.Confined; 
    }

    public void ButtonPresed(int button)
    {
        boat.setTarget(button);
    }

    public void DisableButtons()
    {
        //Disables level buttons.
        for(i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }

        //Disables start buttons.
        for (i = 0; i < startButtons.Length; i++)
        {
            if (startButtons[i].activeSelf == true)
                startButtons[i].SetActive(false);
        }
    }

    public void ActivateButtons()
    {
        for (i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = true;
        }

        switch(boat.currentLocation)
        {
            case 0:
                Debug.Log("Level 1");
                startButtons[0].SetActive(true);
                currentPlayButton = startButtons[0];
                break;

            case 2:
                Debug.Log("Level 2");
                startButtons[1].SetActive(true);
                break;

            case 4:
                Debug.Log("Level 3");
                startButtons[2].SetActive(true);
                break;

        }
    }

    public void MenuButtonPressed(string name)
    {
        SceneManager.LoadScene(name);
    }
}

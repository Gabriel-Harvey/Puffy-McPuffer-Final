using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectUI : MonoBehaviour
{
    [Header("Button Identifing")]
    public GameObject startButton;
    public Button[] buttons;
    private bool buttonPressed;
    private int i;

    [Header("Comunicating With Boat")]
    public LevelSelectBoatMovement boat;

    public void Update()
    {
        if (buttonPressed)
        {
            startButton.SetActive(false);
        }
    }

    public void ButtonPresed(int button)
    {
        boat.setTarget(button);
        buttonPressed = true;
    }

    public void DisableButtons()
    {
        for(i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
    }

    public void ActivateButtons()
    {
        for (i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = true;
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosePauseMenu : MonoBehaviour
{

    public GameObject pauseMenuobject;

    public void CloseMenu()
    {
        pauseMenuobject.SetActive(false);
        Time.timeScale = 1;
    }
}

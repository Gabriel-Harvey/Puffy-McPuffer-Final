

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public GameObject selectedMarker;
    private CameraAim cameraAim;
    public Transform harpoonLockPos;
    public bool active;

    public void Start()
    {
        cameraAim = GameObject.FindGameObjectWithTag("HarpoonLauncher").GetComponent<CameraAim>();
        selectedMarker.SetActive(false);
    }
    private void OnMouseEnter()
    {
        selectedMarker.SetActive(true);
        active = true;
        cameraAim.target = harpoonLockPos;
    }

    private void OnMouseExit()
    {
        selectedMarker.SetActive(false);
        active = false;
        cameraAim.target = null;
    }

    
}




using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public GameObject selectedMarker;
    public Transform harpoonLockPos;

    public void Start()
    {
        selectedMarker.SetActive(false);
    }
}


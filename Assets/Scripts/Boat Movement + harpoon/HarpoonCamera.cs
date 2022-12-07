

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonCamera : MonoBehaviour
{

    [SerializeField] private bool lockon;
    private Color color = Color.red;
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        if (lockon == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        Cursor.lockState = CursorLockMode.Confined;
    }
}

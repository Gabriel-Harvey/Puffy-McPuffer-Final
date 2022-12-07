using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Waypoint : MonoBehaviour
{
    public Image[] questImage;
    public Transform[] targetPosition;
    public Text[] distanceToTarget;
    public Camera cam;
    public Vector3 offset;
    public Collectables[] questObjects;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < questObjects.Length; i++)
        {
            float minX = questImage[i].GetPixelAdjustedRect().width / 2;
            float maxX = Screen.width - minX;

            float minY = questImage[i].GetPixelAdjustedRect().height / 2;
            float maxY = Screen.width - minY;

            // Issue with arrays here preventing multiple waypoints from working
            // Vector2[] pos = cam.WorldToScreenPoint(targetPosition[i].position);
            // This breaks the code

            Vector2 pos = cam.WorldToScreenPoint(targetPosition[i].position);

            if (Vector3.Dot((targetPosition[i].position - transform.position), transform.forward) < 0)
            {
                //Target is behind camera
                if (pos.x < Screen.width / 2)
                {
                    pos.x = maxX;
                }
                else
                {
                    pos.x = minX;
                }
            }

            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            pos.y = Mathf.Clamp(pos.y, minY, maxY);

        
            questImage[i].transform.position = pos;
            distanceToTarget[i].text = ((int)Vector3.Distance(targetPosition[i].position, transform.position)).ToString() + "m";

        
            if (questObjects[i].hooked == true)
            {
                questImage[i].enabled = false;
                distanceToTarget[i].enabled = false;
            }
        }
    }
}

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
    public PurpleBoatRacing raceBoat;
    public GreenBoatFollow followBoat;
    public CollectableBoat collectBoat;
    public PaintQuest paintBoat;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CollectableWaypoints()
    {
        for (int i = 0; i < questObjects.Length; i++)
        {
            if(collectBoat.cargoWaypointActivated == true)
            {
                questImage[i].enabled = true;
                distanceToTarget[i].enabled = true;
                float minX = questImage[i].GetPixelAdjustedRect().width / 2 * offset.x;
                float maxX = Screen.width - minX;

                float minY = questImage[i].GetPixelAdjustedRect().height / 2 * offset.y;
                float maxY = Screen.width - minY;

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

    public void RaceWaypoint()
    {
        for (int i = 2; i < 3; i++)
        {
            if (raceBoat.waypointActivated == true)
            {
                questImage[i].enabled = true;
                distanceToTarget[i].enabled = true;
                float minX = questImage[i].GetPixelAdjustedRect().width / 2 * offset.x;
                float maxX = Screen.width - minX;

                float minY = questImage[i].GetPixelAdjustedRect().height / 2 * offset.y;
                float maxY = Screen.width - minY;

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

            }
            else
            {
                questImage[i].enabled = false;
                distanceToTarget[i].enabled = false;
            }
        }
    }

    public void FollowWaypoint()
    {
        for (int i = 3; i < 4; i++)
        {
            if (followBoat.boatWaypointActivated == true)
            {
                questImage[i].enabled = true;
                distanceToTarget[i].enabled = true;
                float minX = questImage[i].GetPixelAdjustedRect().width / 2 * offset.x;
                float maxX = Screen.width - minX;

                float minY = questImage[i].GetPixelAdjustedRect().height / 2 * offset.y;
                float maxY = Screen.width - minY;

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

            }
            else
            {
                questImage[i].enabled = false;
                distanceToTarget[i].enabled = false;
            }
        }
    }

    public void CollectBoatWaypoint()
    {
        for (int i = 4; i < 5; i++)
        {
            if (collectBoat.boatWaypointActivated == true)
            {
                questImage[i].enabled = true;
                distanceToTarget[i].enabled = true;
                float minX = questImage[i].GetPixelAdjustedRect().width / 2 * offset.x;
                float maxX = Screen.width - minX;

                float minY = questImage[i].GetPixelAdjustedRect().height / 2 * offset.y;
                float maxY = Screen.width - minY;

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

            }
            else
            {
                questImage[i].enabled = false;
                distanceToTarget[i].enabled = false;
            }
        }
    }

    public void PaintWaypoint()
    {
        for (int i = 5; i < 6; i++)
        {
            if (paintBoat.waypointActivated == true)
            {
                questImage[i].enabled = true;
                distanceToTarget[i].enabled = true;
                float minX = questImage[i].GetPixelAdjustedRect().width / 2 * offset.x;
                float maxX = Screen.width - minX;

                float minY = questImage[i].GetPixelAdjustedRect().height / 2 * offset.y;
                float maxY = Screen.width - minY;

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

            }
            else
            {
                questImage[i].enabled = false;
                distanceToTarget[i].enabled = false;
            }
        }
    }

    public void RubbleWaypoint()
    {
        for (int i = 6; i < 7; i++)
        {
            if (followBoat.rubbleWaypointActivated == true)
            {
                questImage[i].enabled = true;
                distanceToTarget[i].enabled = true;
                float minX = questImage[i].GetPixelAdjustedRect().width / 2 * offset.x;
                float maxX = Screen.width - minX;

                float minY = questImage[i].GetPixelAdjustedRect().height / 2 * offset.y;
                float maxY = Screen.width - minY;

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

            }
            else
            {
                questImage[i].enabled = false;
                distanceToTarget[i].enabled = false;
            }
        }
    }
}




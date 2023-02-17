using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    private CheckpointArray checkpointArray;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerBoat")
        {
            checkpointArray.PlayerThroughCheckpointTransform(this);
            gameObject.SetActive(false);
        }
    }

    public void SetTrackCheckpoint(CheckpointArray checkpointArray)
    {
        this.checkpointArray = checkpointArray;
    }
}

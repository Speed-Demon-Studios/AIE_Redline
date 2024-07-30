using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacerDetails : MonoBehaviour
{
    public int currentLap = 0;
    public int placement = 0;
    public int currentCheckpoint = 0;
    private CheckpointHandler cHandler;

    private void Awake()
    {
    }

    public float NextCheckpointDistance()
    {
        cHandler = GameManager.gManager.checkpointParent.GetComponent<CheckpointHandler>();
        var nextCheckpoint = cHandler.GetCheckpoint(cHandler.GetNextIndex(currentCheckpoint));
        return Vector3.Distance(transform.position, nextCheckpoint.transform.position);
    }
}

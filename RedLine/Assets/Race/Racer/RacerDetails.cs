using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacerDetails : MonoBehaviour
{
    public int currentLap = 0;
    public int placement = 0;
    public int currentCheckpoint = 0;

    private CheckpointHandler m_cHandler;

    /// <summary>
    /// Calculates the distance to the next checkpoint
    /// </summary>
    public float NextCheckpointDistance()
    {
        m_cHandler = GameManager.gManager.checkpointParent.GetComponent<CheckpointHandler>();
        var nextCheckpoint = m_cHandler.GetCheckpoint(m_cHandler.GetNextIndex(currentCheckpoint));
        return Vector3.Distance(transform.position, nextCheckpoint.transform.position);
    }
}

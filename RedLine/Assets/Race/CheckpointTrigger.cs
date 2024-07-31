using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    private RacerDetails racer;
    
    private void Awake()
    {
        racer = GetComponentInParent<RacerDetails>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.ToLower() == "checkpoint")
        {
            CheckpointHandler cHandler = GameManager.gManager.checkpointParent.GetComponent<CheckpointHandler>();
            if (other.transform == cHandler.GetCheckpoint(racer.currentCheckpoint))
            {
                racer.currentCheckpoint = cHandler.GetNextIndex(racer.currentCheckpoint);
            }
        }
    }
}

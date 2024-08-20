using UnityEngine;

public class RacerDetails : MonoBehaviour
{
    public bool finishedRacing = false;
    public float distanceToCheckpoint;
    public int currentLap = 0;
    public int placement = 0;
    public int currentCheckpoint = 0;

    private CheckpointHandler m_cHandler;
    

    /// <summary>
    /// Calculates the distance to the next checkpoint
    /// </summary>
    public float NextCheckpointDistance()
    {
        m_cHandler = GameManager.gManager.checkpointParent;
        var nextCheckpoint = m_cHandler.GetCheckpoint(currentCheckpoint);
        distanceToCheckpoint = Vector3.Distance(transform.position, nextCheckpoint.transform.position);
        return distanceToCheckpoint;
    }

    private void Update()
    {
        if (finishedRacing == true)
        {
            PlayerInputScript playerInput = this.GetComponent<PlayerInputScript>();
            ShipsControls shipControls = this.GetComponent<ShipsControls>();
            if (playerInput != null)
            {
                if (playerInput.enabled == true)
                {
                    playerInput.enabled = false;
                }
            }
            if (shipControls != null)
            {
                if (shipControls.enabled == true)
                {
                    shipControls.enabled = false;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.ToLower() == "checkpoint")
        {
            CheckpointHandler cHandler = GameManager.gManager.checkpointParent;
            if (other.transform == cHandler.GetCheckpoint(currentCheckpoint))
            {
                currentCheckpoint = cHandler.GetNextIndex(currentCheckpoint);

                if (other.TryGetComponent(out CheckpointTrigger trigger))
                {
                    if (trigger.finalCheckpoint == true)
                    {
                        GameManager.gManager.rManager.LapComplete(this);
                    }
                    else
                    {
                        return;
                    }
                }
            }
            else
            {
                return;
            }
        }
    }

}

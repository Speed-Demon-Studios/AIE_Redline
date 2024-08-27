using UnityEngine;

public class RacerDetails : MonoBehaviour
{
    public bool finishedRacing = false;
    public float distanceToCheckpoint;
    public int currentLap = 0;
    public int placement = 0;
    public int currentCheckpoint = 0;
    public string RacerName = "";


    private CheckpointHandler m_cHandler;
    private bool nameSet = false;

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

    public void ResetRacerVariables()
    {
        finishedRacing = false;
        currentCheckpoint = 0;
        currentLap = 0;
        placement = 0;
    }

    private void Update()
    {
        if (GameManager.gManager.raceStarted == false && nameSet == false)
        {
            nameSet = true;
            for (int i = 0; i < GameManager.gManager.players.Count; i++)
            {
                if (GameManager.gManager.players[i] == this.gameObject)
                {
                    //Debug.Log("Player Index: " + i);
                    RacerName = ("Player" + (i + 1));
                    Debug.Log("Racer name: " + RacerName);
                    break;
                }
            }
        }


        //if (finishedRacing == true)
        //{
        //    PlayerInputScript playerInput = this.GetComponent<PlayerInputScript>();
        //    ShipsControls shipControls = this.GetComponent<ShipsControls>();
        //    if (playerInput != null)
        //    {
        //        if (playerInput.enabled == true)
        //        {
        //            playerInput.enabled = false;
        //        }
        //    }
        //    if (shipControls != null)
        //    {
        //        if (shipControls.enabled == true)
        //        {
        //            shipControls.enabled = false;
        //        }
        //    }
        //}
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacerDetails : MonoBehaviour
{
    public RedlineColliderSpawner rCS;
    public bool finishedRacing = false;
    public bool crossedFinishLine = false;
    public float distanceToCheckpoint;
    public int currentLap = 0;
    public int placement = 0;
    public int currentCheckpoint = 0;

    public List<float> lapTimesSECONDS = new List<float>();
    public List<float> lapTimesMINUTES = new List<float>();

    public float currentLapTimeSECONDS = 0;
    public float currentLapTimeMINUTES = 0;

    public float totalRaceTimeSeconds = 0;
    public float totalRaceTimeMinutes = 0;

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
        if (currentLap > 0 && finishedRacing == false)
        {
            currentLapTimeSECONDS += Time.deltaTime;
            totalRaceTimeSeconds += 1 * Time.deltaTime;

            if (totalRaceTimeSeconds >= 60.0f)
            {
                totalRaceTimeSeconds = 0.0f;
                totalRaceTimeMinutes += 1.0f;
            }

            if (currentLapTimeSECONDS >= 60.0f)
            {
                currentLapTimeSECONDS = 0.0f;
                currentLapTimeMINUTES += 1;
            }
        }

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
                        if (currentLap > 0)
                        {
                            GameManager.gManager.timingsListUpdated = false;
                            lapTimesSECONDS.Add(currentLapTimeSECONDS);
                            lapTimesMINUTES.Add(currentLapTimeMINUTES);
                            currentLapTimeMINUTES = 0;
                            currentLapTimeSECONDS = 0;
                            GameManager.gManager.timingsListUpdated = true;
                        } 
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

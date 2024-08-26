using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    [SerializeField] private int TotalLaps;

    private void Awake()
    {
        GameManager.gManager.rManager = this;
        GameManager.gManager.pHandler = this.gameObject.GetComponent<PositionHandler>();
        foreach (GameObject gObj in GameManager.gManager.playerObjects)
        {
            InitializeBeforeRace playerInit = gObj.GetComponent<InitializeBeforeRace>();

            playerInit.InitializeForRace();
        }
    }

    public void LapComplete(RacerDetails racer)
    {
        if (racer.currentLap >= TotalLaps)
        {
            racer.finishedRacing = true;
        }
        if (racer.currentLap < TotalLaps)
        {
            racer.currentLap += 1;
        }
        
        if (racer.currentLap > 0)
        {
            Debug.Log("Lap " + (racer.currentLap - 1) + " Completed!");
        }

        GameManager.gManager.raceFinisher.CheckAllRacersFinished();

        return;
    }

    private void Update()
    {
        if (GameManager.gManager.raceStarted == true && GameManager.gManager.raceFinished == false)
        {
            foreach (GameObject racer in GameManager.gManager.racerObjects)
            {
                RacerDetails rDeets = racer.GetComponent<RacerDetails>();
                InitializeBeforeRace ibr = racer.GetComponent<InitializeBeforeRace>();

                if (rDeets.finishedRacing == false)
                {
                    ibr.EnableRacerMovement();
                }
                else
                {
                    ibr.DisableShipControls();
                }
            }
        }
        else if (GameManager.gManager.raceStarted == false && GameManager.gManager.raceFinished == false)
        {
            foreach (GameObject racer in GameManager.gManager.racerObjects)
            {
                InitializeBeforeRace ibr = racer.GetComponent<InitializeBeforeRace>();

                ibr.DisableShipControls();
            }
        }
    }
}

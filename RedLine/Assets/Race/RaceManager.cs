using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    [SerializeField] private int TotalLaps;

    private void Awake()
    {
        GameManager.gManager.pHandler = this.gameObject.GetComponent<PositionHandler>();
        foreach (GameObject gObj in GameManager.gManager.playerObjects)
        {
            InitializeBeforeRace playerInit = gObj.GetComponent<InitializeBeforeRace>();

            playerInit.InitializeForRace();
        }
    }

    public void LapComplete(RacerDetails racer)
    {
        if (racer.currentLap < TotalLaps && racer.currentCheckpoint == 0)
        {
            racer.currentLap += 1;
        }
        

        if (racer.currentLap > 0)
        {
            Debug.Log("Lap " + (racer.currentLap - 1) + " Completed!");
        }
        return;
    }

    private void Update()
    {
        if (GameManager.gManager.raceStarted == true)
        {
            foreach (GameObject racer in GameManager.gManager.racerObjects)
            {
                InitializeBeforeRace ibr = racer.GetComponent<InitializeBeforeRace>();

                ibr.EnableRacerMovement();
                //if (ibr.movementEnabled == false)
                //{
                //}
            }
        }
    }
}

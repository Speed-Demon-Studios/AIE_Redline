using System.Collections;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    [SerializeField] private int TotalLaps;
    bool coroutineStarted = false;

    public float GetTotalLaps() { return TotalLaps; }

    private void Awake()
    {
        GameManager.gManager.rManager = this;
        GameManager.gManager.CurrentScene = "Race";
        GameManager.gManager.enablePlayerCams = true;

        GameManager.gManager.pHandler.OnRaceLoaded();

        if (coroutineStarted == false)
        {
            coroutineStarted = true;
            StartCoroutine(InitPlayers());
        }
    }

    IEnumerator InitPlayers()
    {
        yield return new WaitForEndOfFrame();

        foreach (GameObject gObj in GameManager.gManager.playerObjects)
        {
            if (gObj != null)
            {
                InitializeBeforeRace playerInit = gObj.GetComponent<InitializeBeforeRace>();

                if (playerInit != null)
                {

                    playerInit.InitializeForRace();
                }
                yield return new WaitForEndOfFrame();
            }
        }
        coroutineStarted = false;
        StopCoroutine(InitPlayers());
    }

    public void LapComplete(RacerDetails racer)
    {
        if (racer.currentLap >= TotalLaps)
        {
            racer.crossedFinishLine = true;
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
        //if (GameManager.gManager.raceStarted == true && GameManager.gManager.raceFinished == false)
        //{
        //    foreach (GameObject racer in GameManager.gManager.racerObjects)
        //    {
        //        RacerDetails rDeets = racer.GetComponent<RacerDetails>();
        //        InitializeBeforeRace ibr = racer.GetComponent<InitializeBeforeRace>();
        //
        //        if (rDeets.finishedRacing == false)
        //        {
        //            ibr.EnableRacerMovement();
        //        }
        //        else
        //        {
        //            ibr.DisableShipControls();
        //        }
        //    }
        //}
        //else if (GameManager.gManager.raceStarted == false && GameManager.gManager.raceFinished == false)
        //{
        //    foreach (GameObject racer in GameManager.gManager.racerObjects)
        //    {
        //        InitializeBeforeRace ibr = racer.GetComponent<InitializeBeforeRace>();
        //
        //        ibr.DisableShipControls();
        //    }
        //}
    }
}

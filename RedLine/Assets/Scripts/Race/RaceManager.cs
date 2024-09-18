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
        coroutineStarted = false;
        GameManager.gManager.enablePlayerCams = true;
        if (coroutineStarted == false)
        {
            coroutineStarted = true;
            StartCoroutine(InitPlayers());
        }
        //if (GameManager.gManager.pHandler == null)
        //{
        //    WaitForReference();
        //}
        //else
        //{



        //}
        //StartCoroutine(CheckForReference());
    }

    private void WaitForReference()
    {
        if (GameManager.gManager.pHandler != null)
        {
            GameManager.gManager.enablePlayerCams = true;

            GameManager.gManager.pHandler.OnRaceLoaded();


            if (coroutineStarted == false)
            {
                coroutineStarted = true;
                StartCoroutine(InitPlayers());
            }
        }
        else
        {
            WaitForReference();
        }
    }

    private IEnumerator CheckForReference()
    {
        
        yield return new WaitForFixedUpdate();
        StopCoroutine(CheckForReference());
    }

    public void StartRace()
    {
        GameManager gMAN = GameManager.gManager;
        gMAN.nRandomiser.AssignRacerNames();
        gMAN.raceStarted = true;
        
        for (int i = 0; i < gMAN.racerObjects.Count; i++)
        {
            ActionMappingControl AMC = gMAN.racerObjects[i].GetComponent<ActionMappingControl>();

            AMC.UpdateActionMapForRace();
        }

        gMAN.rActivator.ActivateRedline();
        gMAN.EnableRMovement();
    }

    public void FinishRace()
    {
        GameManager gMAN = GameManager.gManager;
        gMAN.DisableRMovement();
        gMAN.raceFinished = true;

        for (int i = 0; i < gMAN.racerObjects.Count; i++)
        {
            ActionMappingControl AMC = gMAN.racerObjects[i].GetComponent<ActionMappingControl>();
            

            AMC.UpdateActionMapForUI();
        }

        gMAN.raceFinisher.ShowFinalPlacements();
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

    public void DisableFinishedRacerMovement()
    {
        for (int i = 0; i < GameManager.gManager.players.Count; i++)
        {
            RacerDetails racerDeets = GameManager.gManager.players[i].GetComponent<RacerDetails>();

            if (racerDeets.finishedRacing == true && racerDeets.crossedFinishLine == true)
            {
                GameManager.gManager.DisableRMovement(GameManager.gManager.players[i]);
            }
        }
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

        if (GameManager.gManager.raceFinisher.AllRacersFinishedCheck() == true)
        {
            FinishRace();
        }

        DisableFinishedRacerMovement();

        //GameManager.gManager.raceFinisher.CheckAllRacersFinished();

        return;
    }
}

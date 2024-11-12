using System.Collections;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    [SerializeField] private int TotalLaps;
    bool coroutineStarted = false;
    public bool isTimeTrial = false;

    public int GetTotalLaps() { return TotalLaps; }

    private void Awake()
    {
        GameManager.gManager.rManager = this;
        GameManager.gManager.CurrentScene = "Race";
        GameManager.gManager.enablePlayerCams = true;

        if (coroutineStarted == false)
        {
            coroutineStarted = true;
            StartCoroutine(InitPlayers());
        }
    }

    public void StartRace()
    {
        GameManager gMAN = GameManager.gManager;
        gMAN.nRandomiser.AssignRacerNames();
        gMAN.raceStarted = true;
        
        for (int i = 0; i < gMAN.players.Count; i++)
        {
            ActionMappingControl AMC = gMAN.players[i].GetComponent<ActionMappingControl>();

            AMC.UpdateActionMapForRace();
        }

        gMAN.rActivator.ActivateRedline();
        gMAN.EnableRMovement();
    }

    public void FinishRace()
    {
        GameManager gMAN = GameManager.gManager;
        gMAN.DisableRMovement();

        for (int i = 0; i < gMAN.players.Count; i++)
        {
            ActionMappingControl AMC = gMAN.players[i].GetComponent<ActionMappingControl>();
            
            AMC.UpdateActionMapForUI();
        }

        Invoke(nameof(CallFinalPlacements), 2f);
    }

    public void CallFinalPlacements()
    {
        GameManager.gManager.raceFinished = true;
        GameManager.gManager.raceFinisher.ShowFinalPlacements();
    }

    IEnumerator InitPlayers()
    {
        yield return new WaitForEndOfFrame();

        foreach (GameObject gObj in GameManager.gManager.allRacers)
        {
            if (gObj != null)
            {
                InitializeBeforeRace playerInit = gObj.GetComponent<InitializeBeforeRace>();

                if (playerInit != null)
                {

                    playerInit.InitializeForRace();
                }
                yield return new WaitForEndOfFrame();


                if (gObj.GetComponent<RacerDetails>() != null)
                    gObj.GetComponent<RacerDetails>().rCS.CallSpawnCollider();
                {
                }
            }
        }
        coroutineStarted = false;
        StopCoroutine(InitPlayers());
    }

    public void DisableFinishedRacerMovement(RacerDetails racer = null)
    {
        if (racer == null)
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
        else
        {
            if(racer.finishedRacing == true && racer.crossedFinishLine == true)
            {
                GameManager.gManager.DisableRMovement(racer.gameObject);
            }
        }
    }

    public void LapComplete(RacerDetails racer)
    {
        if (isTimeTrial == false)
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
            
            DisableFinishedRacerMovement(racer);
        }
        else
        {
            if (racer.currentLap == 0)
            {
                racer.currentLap = 1;
            }
        }


        //GameManager.gManager.raceFinisher.CheckAllRacersFinished();

        return;
    }
}

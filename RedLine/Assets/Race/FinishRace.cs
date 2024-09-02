using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class FinishRace : MonoBehaviour
{
    public GameObject mainButton;
    [SerializeField] private GameObject[] placementTexts;
    [SerializeField] private GameObject placementWindow;
    private TextMeshProUGUI[] tempSortingTextList;
    private bool m_allRacersFinished = false;
    private bool readyToSetSelected = false;
    private bool readyToDisplay = false;
    private bool timingsListsUpdated = false;
    private bool textListSorted = false;

    private void Awake()
    {
        GameManager.gManager.raceFinisher = this;
    }

    public void DebugForFunction()
    {
        GameManager.gManager.mSL.InitializeForMainMenu();
        Debug.Log("button clicked.");
    }

    /// <summary>
    /// Enables and updates information for the placement window, and activates UI input for player 1.
    /// </summary>
    public void ShowFinalPlacements()
    {
        foreach (GameObject racer in GameManager.gManager.racerObjects)
        {
            if (GameManager.gManager.players.Contains(racer) == false)
            {
                RacerDetails rDeets = racer.GetComponent<RacerDetails>();

                rDeets.finishedRacing = true;
            }
        }
        // Enable the race placement display window and its child objects.
        if (placementWindow.activeSelf == false)
        {
            placementWindow.SetActive(true);
        }

        ActionMappingControl aMC = GameManager.gManager.players[0].GetComponent<ActionMappingControl>(); // Get a reference to player ones ActionMappingControl script.
        aMC.mES.firstSelectedGameObject = mainButton; // Set player ones MultiplayerEventSystem's firstSelectedGameObject to the mainButton object.
        aMC.mES.SetSelectedGameObject(mainButton); // Set player ones MultiplayerEventSystem's selectedGameObject to the mainButton object.

        // Iterate through all of the racer objects.
        for (int i = 0; i < GameManager.gManager.racerObjects.Count; i++)
        {
            placementTexts[i].SetActive(true); // Activate a text object in the placement window for each racer.
            TextMeshProUGUI placementText = placementTexts[i].GetComponent<TextMeshProUGUI>(); // Get a reference to the text object.
            //foreach (GameObject textOBJ in placementTexts)
            //{
            //
            //}

            // Iterate through all of the racer objects again, this time to update the text objects with each racer's respective name and placement.
            foreach (GameObject racerOBJ in GameManager.gManager.racerObjects)
            {

                RacerDetails racerDeets = racerOBJ.GetComponent<RacerDetails>();

                if ((i + 1) == racerDeets.placement)
                {
                    //float bestLapTimeSEC = 0f;
                    //float bestLapTimeMIN = 0f;
                    //if (racerDeets.lapTimesMINUTES.Count > 0 && racerDeets.lapTimesSECONDS.Count > 0)
                    //{
                    //    bestLapTimeSEC = racerDeets.lapTimesSECONDS[0];
                    //    bestLapTimeMIN = racerDeets.lapTimesMINUTES[0];
                    //}
                    //for (int a = 0; a < racerDeets.lapTimesSECONDS.Count; a++)
                    //{
                    //    foreach (float lapSECONDS in racerDeets.lapTimesSECONDS)
                    //    {
                    //        if (lapSECONDS > racerDeets.lapTimesSECONDS[a])
                    //        {
                    //            bestLapTimeSEC = lapSECONDS;
                    //        }
                    //    }
                    //}
                    //for (int a = 0; a < racerDeets.lapTimesMINUTES.Count; a++)
                    //{
                    //    foreach (float lapMINUTES in racerDeets.lapTimesMINUTES)
                    //    {
                    //        if (lapMINUTES > racerDeets.lapTimesMINUTES[a])
                    //        {
                    //            bestLapTimeMIN = lapMINUTES;
                    //        }
                    //    }
                    //
                    //}
                    readyToDisplay = true;

                    //if (readyToDisplay == true)
                    //{
                    //    while (GameManager.gManager.timingsListUpdated == false)
                    //    {
                    //        if (GameManager.gManager.timingsListUpdated == true)
                    //        {
                    //            break;
                    //        }
                    //    }
                    //}

                    if (GameManager.gManager.players.Contains(racerOBJ))
                    {
                        if (racerDeets.finishedRacing == true)
                        {
                            placementText.text = "(" + (racerDeets.placement) + ") " + racerDeets.RacerName + "    ||   " + racerDeets.totalRaceTimeMinutes + ":" + racerDeets.totalRaceTimeSeconds;
                        }
                    }
                    else
                    {
                        placementText.text = "(" + (racerDeets.placement) + ") " + racerDeets.RacerName + "||   DNF";
                    }
                }
            }

        }
    }


    public void CheckAllRacersFinished()
    {
        // Iterate through all of the PLAYER objects.
        foreach (GameObject racerOBJ in GameManager.gManager.players)
        {
            RacerDetails rDeets = racerOBJ.GetComponent<RacerDetails>();

            if (rDeets.finishedRacing == true)
            {
                m_allRacersFinished = true;
            }

            if (rDeets.finishedRacing == false)
            {
                m_allRacersFinished = false;
            }
        }

        if (m_allRacersFinished == true)
        {
            Debug.Log("Race has been completed! All racers have crossed the finish line!");
            GameManager.gManager.raceFinished = true;
            ShowFinalPlacements();
        }
    }
}

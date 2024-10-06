using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Net;

public class FinishRace : MonoBehaviour
{
    public GameObject mainButton;
    [SerializeField] private GameObject[] placementTexts;
    [SerializeField] private GameObject placementWindow;
    private TextMeshProUGUI[] tempSortingTextList;
    private bool m_allRacersFinished = false;
    private bool m_alreadyShowingPlacements = false;
    private bool m_allRacersCrosedLine = false;
    private bool m_checkingRacersFinished = false;
    private bool m_racersFinishedChecked = false;
    private bool readyToSetSelected = false;
    private bool readyToDisplay = false;
    private bool timingsListsUpdated = false;
    private bool textListSorted = false;


    private void Awake()
    {
        GameManager.gManager.raceFinisher = this;
        m_alreadyShowingPlacements = false;
    }

    public void DebugForFunction()
    {
        GameManager.gManager.mSL.InitializeForMainMenu();
    }

    private void Update()
    {
        //CheckAllRacersFinished();

    }

    /// <summary>
    /// Enables and updates information for the placement window, and activates UI input for player 1.
    /// </summary>
    public void ShowFinalPlacements()
    {
        if (m_allRacersFinished == true && m_allRacersCrosedLine == true)
        {
            m_alreadyShowingPlacements = true;
            GameManager.gManager.raceFinished = true;

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

                // Iterate through all of the racer objects again, this time to update the text objects with each racer's respective name and placement.
                foreach (GameObject racerOBJ in GameManager.gManager.racerObjects)
                {

                    RacerDetails racerDeets = racerOBJ.GetComponent<RacerDetails>();

                    if ((i + 1) == racerDeets.placement)
                    {
                        readyToDisplay = true;

                        if (GameManager.gManager.players.Contains(racerOBJ))
                        {
                            if (racerDeets.finishedRacing == true)
                            {
                                float totalMinutes = racerDeets.totalRaceTimeSeconds / 60f;
                                float totalSeconds = racerDeets.totalRaceTimeSeconds % 60f;
                                float quickestTime = 0;

                                foreach(float time in racerDeets.lapTimesSECONDS)
                                {
                                    if(quickestTime == 0)
                                    {
                                        quickestTime = time;
                                    }

                                    if(time < quickestTime)
                                    {
                                        quickestTime = time;
                                    }
                                }

                                float quickestMiuntes = quickestTime / 60f;
                                float quickestSeconds = quickestTime % 60f;

                                if (racerDeets.totalRaceTimeMinutes >= 10.0f)
                                {
                                    placementText.text = "(" + (racerDeets.placement) + ") " + racerDeets.RacerName + "<pos=15%>||" + "<pos=30%>" + string.Format("{0:00}", totalMinutes) + ":" + string.Format("{0:00.00}", totalSeconds + "<pos=45%>||" + "<pos=70%>" + string.Format("{0:00}", quickestMiuntes) + ":" + string.Format("{0:00.00}", quickestSeconds));
                                }
                                else if (racerDeets.totalRaceTimeMinutes < 10.0f)
                                {
                                    placementText.text = "(" + (racerDeets.placement) + ") " + racerDeets.RacerName + "<pos=15%>||" + "<pos=30%>" + string.Format("{0:00}", totalMinutes) + ":" + string.Format("{0:00.00}", totalSeconds + "<pos=45%>||" + "<pos=70%>" + string.Format("{0:00}", quickestMiuntes) + ":" + string.Format("{0:00.00}", quickestSeconds));
                                }
                            }
                        }
                        else
                        {
                            if (racerDeets.crossedFinishLine == false)
                            {
                                placementText.text = "(" + (racerDeets.placement) + ") " + racerDeets.RacerName + "<pos=15%>||" + "<pos=30%>" + "DNF" + "<pos=45%>||" + "<pos=70%>" + "DNF";
                            }
                            else
                            {
                                float totalMinutes = racerDeets.totalRaceTimeSeconds / 60f;
                                float totalSeconds = racerDeets.totalRaceTimeSeconds % 60f;
                                float quickestTime = 0;

                                foreach (float time in racerDeets.lapTimesSECONDS)
                                {
                                    if (quickestTime == 0)
                                    {
                                        quickestTime = time;
                                    }

                                    if (time < quickestTime)
                                    {
                                        quickestTime = time;
                                    }
                                }

                                float quickestMiuntes = quickestTime / 60f;
                                float quickestSeconds = quickestTime % 60f;

                                if (racerDeets.totalRaceTimeMinutes >= 10.0f)
                                {
                                    placementText.text = "(" + (racerDeets.placement) + ") " + racerDeets.RacerName + "<pos=15%>||" + "<pos=30%>" + string.Format("{0:00}", totalMinutes) + ":" + string.Format("{0:00.00}", totalSeconds + "<pos=45%>||" + "<pos=70%>" + string.Format("{0:00}", quickestMiuntes) + ":" + string.Format("{0:00.00}", quickestSeconds));
                                }
                                else if (racerDeets.totalRaceTimeMinutes < 10.0f)
                                {
                                    placementText.text = "(" + (racerDeets.placement) + ") " + racerDeets.RacerName + "<pos=15%>||" + "<pos=30%>" + string.Format("{0:00}", totalMinutes) + ":" + string.Format("{0:00.00}", totalSeconds + "<pos=45%>||" + "<pos=70%>" + string.Format("{0:00}", quickestMiuntes) + ":" + string.Format("{0:00.00}", quickestSeconds));
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public bool AllRacersFinishedCheck()
    {
        m_checkingRacersFinished = true;
        m_racersFinishedChecked = false;

        m_allRacersFinished = true;
        m_allRacersCrosedLine = true;

        // Iterate through all of the PLAYER objects.
        foreach (GameObject racerOBJ in GameManager.gManager.players)
        {
            RacerDetails rDeets = racerOBJ.GetComponent<RacerDetails>();
            if (rDeets.crossedFinishLine == false)
            {
                m_allRacersCrosedLine = false;
            }

            if (rDeets.finishedRacing == false)
            {
                m_allRacersFinished = false;
                break;
            }
        }

        for (int i = 0; i < GameManager.gManager.players.Count; i++)
        {
            m_allRacersFinished = true;
            RacerDetails rDeets = GameManager.gManager.players[i].GetComponent<RacerDetails>();

            if (rDeets.crossedFinishLine == false)
            {
                m_allRacersCrosedLine = false;
            }

            if (rDeets.finishedRacing == false)
            {
                m_allRacersFinished = false;
                break;
            }
        }

        m_racersFinishedChecked = true;
        m_checkingRacersFinished = false;

        if (m_allRacersFinished == true && m_alreadyShowingPlacements == false && m_allRacersCrosedLine == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void CheckAllRacersFinished()
    {
        m_checkingRacersFinished = true;
        m_racersFinishedChecked = false;

        m_allRacersFinished = true;
        m_allRacersCrosedLine = true;

        // Iterate through all of the PLAYER objects.
        foreach (GameObject racerOBJ in GameManager.gManager.players)
        {
            RacerDetails rDeets = racerOBJ.GetComponent<RacerDetails>();
            if (rDeets.crossedFinishLine == false)
            {
                m_allRacersCrosedLine = false;
            }

            if (rDeets.finishedRacing == false)
            {
                m_allRacersFinished = false;
                break;
            }
        }

        for (int i = 0; i < GameManager.gManager.players.Count; i++)
        {
            m_allRacersFinished = true;
            RacerDetails rDeets = GameManager.gManager.players[i].GetComponent<RacerDetails>();

            if (rDeets.crossedFinishLine == false)
            {
                m_allRacersCrosedLine = false;
            }

            if (rDeets.finishedRacing == false)
            {
                m_allRacersFinished = false;
                break;
            }
        }

        m_racersFinishedChecked = true;
        m_checkingRacersFinished = false;

        if (m_allRacersFinished == true && m_alreadyShowingPlacements == false && m_allRacersCrosedLine == true)
        {
            ShowFinalPlacements();
        }
        return;
    }
}

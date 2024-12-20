using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Net;
using UnityEngine.Assertions.Must;

public class FinishRace : MonoBehaviour
{
    public GameObject mainButton;
    public bool m_allRacersFinished = false;
    [SerializeField] private GameObject[] placementTexts;
    [SerializeField] private GameObject placementWindow;
    private TextMeshProUGUI[] tempSortingTextList;
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
        placementWindow.SetActive(false);
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
                RacerEntry rEntry = placementTexts[i].GetComponent<RacerEntry>();
                TextMeshProUGUI placementText = rEntry.placementObject; // Get a reference to the PLACEMENT text object.
                TextMeshProUGUI nameText = rEntry.racerNameObject; // Get a reference to the NAME text object.
                TextMeshProUGUI totalTimeText = rEntry.Time1Object; // Get a reference to the TOTAL RACE TIME text object.
                TextMeshProUGUI quickestTimeText = rEntry.Time2Object; // Get a reference to the FASTEST RACE TIME text object.

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


                                float totalMinutes = Mathf.CeilToInt(racerDeets.totalRaceTimeSeconds / 60);
                                float totalSeconds = Mathf.CeilToInt(racerDeets.totalRaceTimeSeconds - totalMinutes % 60);
                                float quickestTime = 0;

                                float secondsAdded = 0;
                                int minutesAdded = 0;

                                //for (int a = 0; a < totalSeconds; a++)
                                //{
                                //    secondsAdded += 1f;
                                //
                                //    if (secondsAdded >= 60)
                                //    {
                                //        minutesAdded += 1;
                                //        secondsAdded = 0;
                                //    }
                                //}
                                //
                                //foreach(float time in racerDeets.lapTimesSECONDS)
                                //{
                                //    if(quickestTime == 0)
                                //    {
                                //        quickestTime = time;
                                //    }
                                //
                                //    if(time < quickestTime)
                                //    {
                                //        quickestTime = time;
                                //    }
                                //}

                                //float quickestMiuntes = Mathf.CeilToInt(quickestTime / 60);
                                //float quickestSeconds = Mathf.CeilToInt(quickestTime - quickestMiuntes / 60);

                                float quickestSeconds = 0;
                                int quickestMinutes = 0;

                                for (int a = 0; a < quickestTime; a++)
                                {
                                    quickestSeconds += 1f;

                                    if (quickestSeconds >= 60)
                                    {
                                        quickestMinutes += 1;

                                        quickestSeconds = 0;
                                    }
                                }


                                placementText.text = "(" + (racerDeets.placement.ToString()) + ")";
                                nameText.text = racerDeets.RacerName;
                                totalTimeText.text = string.Format("{0:00}", racerDeets.totalRaceTimeMinutes) + ":" + string.Format("{0:00.00}", racerDeets.totalRaceTimeSeconds);
                                quickestTimeText.text = string.Format("{0:00}", racerDeets.quickestLapTimeMINUTES) + ":" + string.Format("{0:00.00}", racerDeets.quickestLapTimeSECONDS);
                            }
                        }
                        else
                        {
                            if (racerDeets.crossedFinishLine == false)
                            {
                                placementText.text = "(" + (racerDeets.placement.ToString()) + ")";
                                nameText.text = racerDeets.RacerName;
                                totalTimeText.text = "DNF";
                                quickestTimeText.text = string.Format("{0:00}", racerDeets.quickestLapTimeMINUTES) + ":" + string.Format("{0:00.00}", racerDeets.quickestLapTimeSECONDS);
                            }
                            else
                            {
                                float totalMinutes = Mathf.CeilToInt(racerDeets.totalRaceTimeSeconds / 60);
                                float totalSeconds = Mathf.CeilToInt(racerDeets.totalRaceTimeSeconds - totalMinutes % 60);
                                //float quickestTime = 0;

                                //int secondsAdded = 0;
                                //int minutesAdded = 0;
                                //
                                //for (int a = 0; a < totalSeconds; a++)
                                //{
                                //    secondsAdded += 1;
                                //
                                //    if (secondsAdded >= 60)
                                //    {
                                //        minutesAdded += 1;
                                //        secondsAdded = 0;
                                //    }
                                //}
                                //
                                //int quickestSeconds = 0;
                                //int quickestMinutes = 0;
                                //
                                //for (int a = 0; a < quickestTime; a++)
                                //{
                                //    quickestSeconds += 1;
                                //
                                //    if (quickestSeconds >= 60)
                                //    {
                                //        quickestMinutes += 1;
                                //
                                //        quickestSeconds = 0;
                                //    }
                                //}

                                //float quickestMiuntes = Mathf.CeilToInt(quickestTime / 60);
                                //float quickestSeconds = Mathf.CeilToInt(quickestTime - quickestMiuntes % 60);

                                placementText.text = "(" + (racerDeets.placement) + ")";
                                nameText.text = racerDeets.RacerName;
                                totalTimeText.text = string.Format("{0:00}", racerDeets.totalRaceTimeMinutes) + ":" + string.Format("{0:00.00}", racerDeets.totalRaceTimeSeconds);
                                quickestTimeText.text = string.Format("{0:00}", racerDeets.quickestLapTimeMINUTES) + ":" + string.Format("{0:00.00}", racerDeets.quickestLapTimeSECONDS);
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

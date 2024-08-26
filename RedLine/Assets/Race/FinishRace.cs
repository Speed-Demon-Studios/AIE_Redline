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
    private bool m_allRacersFinished = false;
    [SerializeField] private GameObject[] placementTexts;
    [SerializeField] private GameObject placementWindow;
    private bool readyToSetSelected = false;

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
                    placementText.text = "(" + (i + 1) + ") " + racerDeets.RacerName;
                }
            }

        }
    }


    public void CheckAllRacersFinished()
    {
        // Iterate through all of the racer objects.
        for (int i = 0; i < GameManager.gManager.racerObjects.Count; i++)
        {
            RacerDetails racerDeets = GameManager.gManager.racerObjects[i].GetComponent<RacerDetails>(); // Get a referemce to the racers RacerDetails script.

            if (racerDeets.finishedRacing == true && i == (GameManager.gManager.racerObjects.Count - 1))
            {
                m_allRacersFinished = true;
            }
            else if (racerDeets.finishedRacing == false)
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

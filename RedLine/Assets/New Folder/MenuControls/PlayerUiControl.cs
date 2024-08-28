using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.ComponentModel.Design;

public class PlayerUiControl : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI placementText;
    [SerializeField] private TextMeshProUGUI lapText;
    [SerializeField] private RacerDetails rDetails;

    private void Update()
    {
        if (GameManager.gManager.raceStarted == false)
        {
            placementText.text = " ";
            lapText.text = " ";
        }
        else if (GameManager.gManager.raceStarted == true)
        {
            if (rDetails.currentLap > 0)
            {
                lapText.text = rDetails.currentLap.ToString();
            }

            if (GameManager.gManager.indexListSorted == true)
            {
                for (int i = 0; i < GameManager.gManager.pHandler.racers.Count; i++)
                {
                    if (GameManager.gManager.pHandler.racers[i] == rDetails)
                    {
                        placementText.text = (i + 1).ToString();
                    }
                }
            }
        }
    }
}

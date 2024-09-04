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
    [SerializeField] private TextMeshProUGUI m_speed;
    [SerializeField] private RacerDetails rDetails;
    [SerializeField] private ShipsControls m_shipsControls;

    private void Update()
    {
        if (GameManager.gManager)
        {
            if (GameManager.gManager.raceStarted == false)
            {
                placementText.text = " ";
                lapText.text = " ";
            }
            else if (GameManager.gManager.raceStarted == true && GameManager.gManager.raceFinished == false)
            {
                if (rDetails.currentLap > 0)
                {
                    lapText.text = "laps: " + rDetails.currentLap.ToString() + " / " + GameManager.gManager.rManager.GetTotalLaps().ToString();
                }

                if (GameManager.gManager.indexListSorted == true)
                {
                    for (int i = 0; i < GameManager.gManager.pHandler.racers.Count; i++)
                    {
                        if (GameManager.gManager.pHandler.racers[i] == rDetails)
                        {
                            placementText.text = "Pos: " + (i + 1).ToString() + " / " + GameManager.gManager.racerObjects.Count.ToString();
                        }
                    }
                }

                m_speed.text = (((int)m_shipsControls.ReturnRB().velocity.magnitude) * 7f).ToString();
            }
        }
    }
}

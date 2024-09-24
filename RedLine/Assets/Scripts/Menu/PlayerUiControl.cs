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
    public List<Slider> sliders = new();
    private int m_sliderNumber;
    public TextMeshProUGUI test;

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

                switch (m_shipsControls.ReturnBoost())
                {
                    case > 3:
                        m_sliderNumber = 2;
                        break;
                    case > 2:
                        m_sliderNumber = 1;
                        break;
                    case > 1:
                        m_sliderNumber = 0;
                        break;
                }

                if (m_shipsControls.ReturnIsBoosting())
                {
                    for (int i = 0; i < sliders.Count; i++)
                    {
                        sliders[i].value = 0;
                    }
                }
                
                if (test != null)
                    test.text = m_shipsControls.ReturnRB().velocity.y.ToString();

                if(m_shipsControls.ReturnBoostLevel() < sliders.Count)
                    sliders[m_shipsControls.ReturnBoostLevel()].value = m_shipsControls.ReturnBoost() - m_shipsControls.ReturnBoostLevel();

                m_speed.text = (((int)m_shipsControls.ReturnRB().velocity.magnitude) * 7f).ToString();
            }
        }
    }

    public void ResetSliders()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.ComponentModel.Design;

public class PlayerUiControl : MonoBehaviour
{
    [SerializeField] private RacerDetails rDetails;
    [SerializeField] private ShipsControls m_shipsControls;

    public Animator finishAnim;
    public HUD hud;

    private void Update()
    {
        if (GameManager.gManager && !m_shipsControls.isTestShip)                                                                      
        {
            if (GameManager.gManager.raceStarted == true && GameManager.gManager.raceFinished == false)
            {
                float speed = ((int)m_shipsControls.ReturnRB().velocity.magnitude) * 7f;
                int pos = 0;
                if (GameManager.gManager.indexListSorted == true) // if the list of racers is sorted
                {
                    for (int i = 0; i < GameManager.gManager.pHandler.racers.Count; i++) // go through all racers and display the position 
                    {
                        if (GameManager.gManager.pHandler.racers[i] == rDetails) // if i is equal to this current object then display the position
                        {
                            pos = i + 1;
                        }
                    }
                }
                int currentLap = rDetails.currentLap;
                int totalLaps = GameManager.gManager.rManager.GetTotalLaps();
                bool isInRedline = m_shipsControls.ReturnIsInRedline();
                float energyfillValue = hud.map(m_shipsControls.ReturnBoost() * 0.32f, 0, 1, 0.025f, 0.194f);
                float speedFillValue = hud.map(((float)m_shipsControls.ReturnRB().velocity.magnitude * 0.0056f), 0, 1, 0.547f, 0.66f);
                hud.SetValues(speed, pos, currentLap, totalLaps, isInRedline, energyfillValue, speedFillValue, m_shipsControls.ReturnBoostLevel(),
                    rDetails.currentLapTimeMINUTES, rDetails.currentLapTimeSECONDS, rDetails.quickestLapTimeMINUTES, rDetails.quickestLapTimeSECONDS);
                hud.UpdateHUD();
            }
        }                                                                                                                                    
    }

    public void FinishPopUp()
    {
        finishAnim.gameObject.SetActive(true);
        StartCoroutine(WaitToHideFinish());
    }

    IEnumerator WaitToHideFinish()
    {
        yield return new WaitForSeconds(2f);
        finishAnim.gameObject.SetActive(false);
    }
}

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
    [SerializeField] private TextMeshProUGUI currentLapTime;
    [SerializeField] private TextMeshProUGUI bestLapTime;
    public List<Slider> sliders = new();
    private int m_sliderNumber;
    public List<Animator> anim;

    private void Update()
    {
        //---------------------------------------------------------------------------------------------------------------------------------|
        if (GameManager.gManager)                                                                                                        //|
        {                                                                                                                                //|
        //---------------------------------------------------------------------------------------------------------------------------------|
            if (GameManager.gManager.raceStarted == false) // if the race has not started than all the text is null                        |
            {                                                                                                                            //|
                placementText.text = " ";                                                                                                //|
                lapText.text = " ";                                                                                                      //|
                currentLapTime.text = " ";                                                                                               //|
                bestLapTime.text = " ";                                                                                                  //|
            }                                                                                                                            //|
        //---------------------------------------------------------------------------------------------------------------------------------|
        // if the race has started but not finished then start to display all the info                                                     |
            else if (GameManager.gManager.raceStarted == true && GameManager.gManager.raceFinished == false)                             //|
            {                                                                                                                            //|
        //---------------------------------------------------------------------------------------------------------------------------------|
                if (rDetails.currentLap > 0) // if the player has past the start line and started the race then display the laps           |
                {                                                                                                                        //|------|
                    lapText.text = "laps: " + rDetails.currentLap.ToString() + " / " + GameManager.gManager.rManager.GetTotalLaps().ToString(); //|
                }                                                                                                                               //|
        //----------------------------------------------------------------------------------------------------------------------------------------|
                if (GameManager.gManager.indexListSorted == true) // if the list of racers is sorted                                              |
                {                                                                                                                               //|
                    for (int i = 0; i < GameManager.gManager.pHandler.racers.Count; i++) // go through all racers and display the position        |
                    {                                                                                                                           //|
                        if (GameManager.gManager.pHandler.racers[i] == rDetails) // if i is equal to this current object then display the position|
                        {                                                                                                                       //|
                            placementText.text = "Pos: " + (i + 1).ToString() + " / " + GameManager.gManager.racerObjects.Count.ToString();     //|
                        }                                                                                                                       //|
                    }                                                                                                                           //|
                }                                                                                                                               //|
        //----------------------------------------------------------------------------------------------------------------------------------------|-------------------|
                // display the current lap time                                                                                                                       |
                currentLapTime.text = string.Format("{0:00}", rDetails.currentLapTimeMINUTES) + ":" + string.Format("{0:00.00}", rDetails.currentLapTimeSECONDS);   //|
                // if the quickest lap time is over a certain time than dont display it                                                                               |
                if(rDetails.quickestLapTimeMINUTES > 50)                                                                                                            //|
                {                                                                                                                                                   //|
                    bestLapTime.text = "--:--:--";                                                                                                                  //|
                }                                                                                                                                                   //|
                else // if the quickest lap time is good than display it on the screen                                                                                |
                {                                                                                                                                                   //|
                    bestLapTime.text = string.Format("{0:00}", rDetails.quickestLapTimeMINUTES) + ":" + string.Format("{0:00.00}", rDetails.quickestLapTimeSECONDS);//|
                }                                                                                                                                                   //|
        //---------------------------------------------------------------------------------------------------------------------------------|--------------------------|
                // this is for the bars and sets which bar is currently inscreasing depending on the level of boost                        |
                switch (m_shipsControls.ReturnBoost())                                                                                   //|
                {                                                                                                                        //|
                    case > 3:                                                                                                            //|
                        m_sliderNumber = 2;                                                                                              //|
                        break;                                                                                                           //|
                    case > 2:                                                                                                            //|
                        m_sliderNumber = 1;                                                                                              //|
                        break;                                                                                                           //|
                    case > 1:                                                                                                            //|
                        m_sliderNumber = 0;                                                                                              //|
                        break;                                                                                                           //|
                }                                                                                                                        //|
        //----------------------------------------------------------------------------------|----------------------------------------------|
                // if the player is boosting than set all the bars to zero                  |
                if (m_shipsControls.ReturnIsBoosting())                                   //|
                {                                                                         //|
                    for (int i = 0; i < sliders.Count; i++)                               //|
                    {                                                                     //|
                        sliders[i].value = 0;                                             //|
                    }                                                                     //|
                }                                                                         //|
        //----------------------------------------------------------------------------------|
                // flicker animation if the ship is in the redline                          |
                anim[0].SetBool("IsIn", m_shipsControls.ReturnIsInRedline());             //|
                anim[1].SetBool("IsIn", m_shipsControls.ReturnIsInRedline());             //|
                anim[2].SetBool("IsIn", m_shipsControls.ReturnIsInRedline());             //|
        //----------------------------------------------------------------------------------|---------------------------------------------------|
                // if the boost level is less than the slider count then set the sliders values to the boost                                    |
                if(m_shipsControls.ReturnBoostLevel() < sliders.Count)                                                                        //|
                    sliders[m_shipsControls.ReturnBoostLevel()].value = m_shipsControls.ReturnBoost() - m_shipsControls.ReturnBoostLevel();   //|
                                                                                                                                              //|
                // Set the speed text to the current speed times 7                                                                              |
                m_speed.text = (((int)m_shipsControls.ReturnRB().velocity.magnitude) * 7f).ToString();                                        //|
            }                                                                                                                                 //|
        }                                                                                                                                     //|
        //--------------------------------------------------------------------------------------------------------------------------------------|
    }
}

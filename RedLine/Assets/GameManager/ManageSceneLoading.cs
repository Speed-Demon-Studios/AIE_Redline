using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ManageSceneLoading : MonoBehaviour
{
    bool reloadingmenu = false;
    public void InitializeForMainMenu()
    {
        reloadingmenu = true;
        foreach (GameObject playerOBJ in GameManager.gManager.players)
        {
            InitializeBeforeRace IBR = playerOBJ.GetComponent<InitializeBeforeRace>();
            //IBR.playerCamera.gameObject.SetActive(false);
            RacerDetails racerDeets = playerOBJ.GetComponent<RacerDetails>();
            racerDeets.finishedRacing = false;
            racerDeets.currentLap = 0;
        }
        GameManager.gManager.pHandler.racerFinder = new List<RacerDetails>();
        GameManager.gManager.pHandler.racers = new List<RacerDetails>();
        GameManager.gManager.racerObjects = new List<GameObject>();
        GameManager.gManager.raceFinished = false;
        GameManager.gManager.raceStarted = false;
        GameManager.gManager.racersAdded = false;
        GameManager.gManager.pHandler.racersAdded = false;
        GameManager.gManager.countdownIndex = 5;
        
        SceneManager.UnloadSceneAsync(1);
        SceneManager.LoadScene(0);
    }

    public void SetPlayerUIInputMM()
    {
        if (reloadingmenu == true)
        {
            reloadingmenu = false;
            ActionMappingControl aMC = GameManager.gManager.players[0].GetComponent<ActionMappingControl>();

            aMC.mES.firstSelectedGameObject = GameManager.gManager.m_startButtons[0];
            aMC.mES.SetSelectedGameObject(GameManager.gManager.m_startButtons[0]);
        }
    }
}

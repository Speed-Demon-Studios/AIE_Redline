using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ManageSceneLoading : MonoBehaviour
{
    bool reloadingmenu = false;
    bool coroutineStarted = false;

    public void InitializeForMainMenu()
    {
        reloadingmenu = true;
        GameManager.gManager.rActivator.DeactivateRedline();
        foreach (GameObject playerOBJ in GameManager.gManager.players)
        {
            ResetShip(playerOBJ);
        }

        for (int i = GameManager.gManager.racerObjects.Count - 1; i >= 0; i--)
        {
            GameObject temp = GameManager.gManager.racerObjects[i];

            if (GameManager.gManager.allRacers.Contains(temp))
            {
                GameManager.gManager.allRacers.Remove(temp);
            }

            GameManager.gManager.racerObjects.Remove(temp);

            Destroy(temp);
        }

        ResetGameManager();

        reloadingmenu = true;
        if (coroutineStarted == false)
        {
            coroutineStarted = true;
            StartCoroutine(LoadMenuScene());
        }
    }

    public void ResetShip(GameObject playerOBJ)
    {
        InitializeBeforeRace IBR = playerOBJ.GetComponent<InitializeBeforeRace>();
        AIMoveInputs aiMove = playerOBJ.GetComponent<AIMoveInputs>();
        Destroy(aiMove, 0.5f);
        playerOBJ.GetComponent<ShipsControls>().enabled = false;
        playerOBJ.GetComponent<ShipBlendAnimations>().enabled = false;
        ShipsControls controls = playerOBJ.GetComponent<ShipsControls>();
        IsShipCollider shipCollider = controls.collisionParent.GetComponentInChildren<IsShipCollider>();
        controls.FireList().Clear();
        GameObject a = shipCollider.gameObject;
        GameObject b = controls.shipModel.transform.GetChild(0).gameObject;
        a.transform.parent = null;
        b.transform.parent = null;
        Destroy(a);
        Destroy(b);
        controls.VariantObject = null;
        playerOBJ.GetComponent<PlayerInputScript>().playerReadyInMenu = false;
        RacerDetails racerDeets = playerOBJ.GetComponent<RacerDetails>();
        racerDeets.finishedRacing = false;
        racerDeets.currentLap = 0;
        racerDeets.totalRaceTimeSeconds = 0;
        racerDeets.totalRaceTimeMinutes = 0;
        racerDeets.currentLapTimeSECONDS = 0;
        racerDeets.currentLapTimeMINUTES = 0;
        racerDeets.quickestLapTimeSECONDS = 99;
        racerDeets.quickestLapTimeMINUTES = 99;


        ShipToWallCollision stwc = playerOBJ.GetComponent<ShipToWallCollision>();
        racerDeets.rCS.ClearList();
    }

    public void ResetGameManager()
    {
        GameManager.gManager.pHandler.racerFinder = new List<RacerDetails>();
        GameManager.gManager.pHandler.racers = new List<RacerDetails>();
        GameManager.gManager.racerObjects = new List<GameObject>();
        GameManager.gManager.racersPlaced = false;
        GameManager.gManager.raceFinished = false;
        GameManager.gManager.raceStarted = false;
        GameManager.gManager.racersAdded = false;
        GameManager.gManager.pHandler.racersAdded = false;
        GameManager.gManager.countdownIndex = 5;
        GameManager.gManager.namesAssigned = false;
        GameManager.gManager.nRandomiser.usedNames = new List<string>();
        GameManager.gManager.redlineActivated = false;
    }

    IEnumerator LoadMenuScene()
    {
        yield return new WaitForEndOfFrame();

        foreach (GameObject collider in GameObject.FindGameObjectsWithTag("Redline"))
        {
            DestroyImmediate(collider.gameObject);
        }


        SceneManager.LoadSceneAsync(0);
        SceneManager.UnloadSceneAsync(1);



        coroutineStarted = false;
        StopCoroutine(LoadMenuScene());
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

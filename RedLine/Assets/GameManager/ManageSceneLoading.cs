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
        foreach (GameObject playerOBJ in GameManager.gManager.players)
        {
            InitializeBeforeRace IBR = playerOBJ.GetComponent<InitializeBeforeRace>();
            //IBR.playerCamera.gameObject.SetActive(false);
            RacerDetails racerDeets = playerOBJ.GetComponent<RacerDetails>();
            ShipsControls controls = playerOBJ.GetComponent<ShipsControls>();
            playerOBJ.GetComponent<ShipsControls>().enabled = false;
            playerOBJ.GetComponent<ShipBlendAnimations>().enabled = false;
            controls.variant = null;
            controls.VariantObject = null;
            IsShipCollider shipCollider = controls.collisionParent.GetComponentInChildren<IsShipCollider>();
            GameObject a = shipCollider.gameObject;
            GameObject b = controls.shipModel.transform.GetChild(3).gameObject;
            a.transform.parent = null;
            b.transform.parent = null;
            Destroy(a);
            Destroy(b);
            playerOBJ.GetComponent<PlayerInputScript>().playerReadyInMenu = false;
            racerDeets.finishedRacing = false;
            racerDeets.currentLap = 0;
        }

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
        reloadingmenu = true;
        if (coroutineStarted == false)
        {
            coroutineStarted = true;
            StartCoroutine(LoadMenuScene());
        }
    }

    IEnumerator LoadMenuScene()
    {
        yield return new WaitForEndOfFrame();

        foreach (ColliderTrigger collider in GameObject.FindObjectsOfType<ColliderTrigger>())
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

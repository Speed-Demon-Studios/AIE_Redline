using EAudioSystem;
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

        if (coroutineStarted == false)
        {
            coroutineStarted = true;
            StartCoroutine(LoadMenuScene());
            reloadingmenu = false;
        }
    }

    public void ResetShip(GameObject playerOBJ)
    {
        InitializeBeforeRace IBR = playerOBJ.GetComponent<InitializeBeforeRace>();
        AIMoveInputs aiMove = playerOBJ.GetComponent<AIMoveInputs>();
        ShipsControls controls = playerOBJ.GetComponent<ShipsControls>();
        IsShipCollider shipCollider = controls.collisionParent.GetComponentInChildren<IsShipCollider>();
        RacerDetails racerDeets = playerOBJ.GetComponent<RacerDetails>();
        PlayerInputScript playerInputScript = playerOBJ.GetComponent<PlayerInputScript>();
        ActionMappingControl acm = playerOBJ.GetComponent<ActionMappingControl>();

        GameObject shipCollisionObject = shipCollider.gameObject;
        GameObject shipModelObject = controls.shipModel.transform.GetChild(0).gameObject;
        Destroy(aiMove);

        controls.FireList().Clear();
        controls.VariantObject = null;

        shipCollisionObject.transform.parent = null;
        shipModelObject.transform.parent = null;

        Destroy(shipCollisionObject);
        Destroy(shipModelObject);

        playerInputScript.playerReadyInMenu = false;
        playerInputScript.DeActivateVirtualCam();

        racerDeets.finishedRacing = false;
        racerDeets.currentLap = 0;
        racerDeets.totalRaceTimeSeconds = 0;
        racerDeets.totalRaceTimeMinutes = 0;
        racerDeets.currentLapTimeSECONDS = 0;
        racerDeets.currentLapTimeMINUTES = 0;
        racerDeets.quickestLapTimeSECONDS = 99;
        racerDeets.quickestLapTimeMINUTES = 99;

        SparksParticlesController SPC = playerOBJ.GetComponentInChildren<SparksParticlesController>();
        if (SPC != null)
        {
            foreach (SparksTrigger sT in SPC.sparksList)
            {
                if (sT != null)
                {
                    sT.isColliding = false;

                    foreach (GameObject sparksOBJ in sT.sparks)
                    {
                        if (sparksOBJ != null)
                        {
                            SPC.DeactivateSparks(sparksOBJ, sT);
                        }
                    }
                }
            }
        }

        playerOBJ.GetComponent<ShipsControls>().enabled = false;
        playerOBJ.GetComponent<ShipBlendAnimations>().enabled = false;

        if(playerInputScript.GetPlayerNumber() != 1)
        {
            GameManager.gManager.numberOfPlayers -= 1;

            acm.GetPlayerInput().gameObject.SetActive(false);

        }

        controls.ChangeDoneDifficulty(false);
        controls.DeInitialize();

        controls.ResetRedline();
        racerDeets.rCS.ClearList();

        foreach (GameObject player in GameManager.gManager.players)
        {
            player.GetComponent<PlayerAudioController>().ResetPlayerAudio();
        }


        //playerOBJ.SetActive(false);
    }

    public void ResetGameManager()
    {
        PlayerPrefs.SetFloat("AfterRace", 1);
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

        //PlayerPrefs.SetInt("SceneID", 1);
        SceneManager.LoadScene(1);
        //SceneManager.UnloadSceneAsync(2);

        coroutineStarted = false;
        StopCoroutine(LoadMenuScene());
    }

    public void SetPlayerUIInputMM()
    {
        if (reloadingmenu == true)
        {
            reloadingmenu = false;
            ActionMappingControl aMC = GameManager.gManager.players[0].GetComponent<ActionMappingControl>();
        }
    }
}

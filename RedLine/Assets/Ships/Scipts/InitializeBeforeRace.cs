using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.InputSystem;

public class InitializeBeforeRace : MonoBehaviour
{
    public bool movementEnabled = false;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private PlayerInputScript pInput;
    [SerializeField] private ShipsControls sControls;
    [SerializeField] private AIMoveInputs aiInput;

    private void Awake()
    {
        this.gameObject.AddComponent<DontDestroy>();
        GameManager.gManager.playerObjects.Add(this.gameObject);
        if (pInput != null)
        {
            sControls.enabled = false;
        }

        if (playerCamera != null)
        {
            playerCamera.enabled = false;
        }
        
    }

    public void DisableShipControls()
    {
        sControls = this.GetComponent<ShipsControls>();
        if (sControls != null)
        {
            sControls.enabled = false;
        }
    }

    public void EnableRacerMovement()
    {
        movementEnabled = false;
        if (aiInput != null)
        {
            aiInput.enabled = true;
        }

        if (pInput != null)
        {
            pInput.enabled = true;
        }

        if (playerCamera != null)
        {
            playerCamera.enabled = true;
        }

        if (sControls.enabled == false)
        {
            sControls.enabled = true;
        }


        movementEnabled = true;
    }

    public void InitializeForRace()
    {
        if (playerCamera != null)
        {
            playerCamera.enabled = true;
        }
        sControls.enabled = true;
        foreach (GameObject gObj in GameManager.gManager.racerObjects)
        {

        }
        
        //pInput.enabled = true;
        if (GameManager.gManager.pHandler != null)
        {
            GameManager.gManager.pHandler.enabled = true;
        }
        //GameManager.gManager.pHandler.OnRaceLoaded();
    }
}

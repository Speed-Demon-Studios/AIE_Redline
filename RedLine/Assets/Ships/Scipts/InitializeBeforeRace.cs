using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class InitializeBeforeRace : MonoBehaviour
{
    public bool movementEnabled = false;
    public GameObject playerCamOBJECT;
    public ShipsControls sControls;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private PlayerInputScript pInput;
    [SerializeField] private AIMoveInputs aiInput;
    [SerializeField] private Rigidbody rb;

    private void Update()
    {
        if (GameManager.gManager.CurrentScene == "MainMenu" || (GameManager.gManager.CurrentScene == "Race" && GameManager.gManager.raceStarted == false))
        {
            rb.velocity = new Vector3(0, 0, 0);
            rb.angularVelocity = new Vector3(0, 0, 0);
            sControls.ResetAcceleration();
            rb.isKinematic = true;
        }
        else if (GameManager.gManager.CurrentScene == "Race" && GameManager.gManager.raceStarted == true)
        {
            rb.isKinematic = false;
        }
    }

    private void Awake()
    {
        this.gameObject.AddComponent<DontDestroy>();
        sControls = this.GetComponent<ShipsControls>();
        GameManager.gManager.playerObjects.Add(this.gameObject);

        if (playerCamOBJECT != null)
        {
            playerCamOBJECT.SetActive(false);
        }
    }

    public void DisableShipControls()
    {
        sControls.enabled = false;
    }

    public void EnableRacerMovement()
    {
        sControls.enabled = true;
    }

    public void InitializeForRace()
    {
        if (playerCamOBJECT != null)
        {
            playerCamOBJECT.SetActive(true);
        }

        foreach (GameObject racerOBJ in GameManager.gManager.players)
        {
            RacerDetails rDeets = racerOBJ.GetComponent<RacerDetails>();

            rDeets.finishedRacing = false;
            rDeets.crossedFinishLine = false;
        }
        //sControls.enabled = true;
    }

    public void AttachModels()
    {
        ShipVariant variant = sControls.variant;

        Instantiate(variant.model, sControls.shipModel.transform);
        Instantiate(variant.collision, sControls.collisionParent);
    }
}

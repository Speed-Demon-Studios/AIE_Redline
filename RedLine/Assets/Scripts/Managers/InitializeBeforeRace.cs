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

    //private void Update()
    //{
    //    if (GameManager.gManager.CurrentScene == "MainMenu" || (GameManager.gManager.CurrentScene == "Race" && GameManager.gManager.raceStarted == false))
    //    {
    //        rb.velocity = new Vector3(0, 0, 0);
    //        rb.angularVelocity = new Vector3(0, 0, 0);
    //        sControls.ResetAcceleration();
    //        rb.isKinematic = true;
    //    }
    //    else if (GameManager.gManager.CurrentScene == "Race" && GameManager.gManager.raceStarted == true)
    //    {
    //        rb.isKinematic = false;
    //    }
    //}

    private void Awake()
    {
        if (!GetComponent<ShipsControls>().isTestShip)
        {
            foreach (GameObject playerOBJ in GameManager.gManager.players)
            {
                if (this.gameObject == playerOBJ)
                {
                    DontDestroy ddol;

                    this.gameObject.TryGetComponent<DontDestroy>(out ddol);

                    if (ddol == null)
                    {
                        this.gameObject.AddComponent<DontDestroy>();
                    }
                    break;
                }
            }

            sControls = this.GetComponent<ShipsControls>();

            GameManager.gManager.allRacers.Add(this.gameObject);

            if (playerCamOBJECT != null)
            {
                playerCamOBJECT.SetActive(false);
            }
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
        GameManager.gManager.raceFinished = false;

        if (playerCamOBJECT != null)
        {
            playerCamOBJECT.SetActive(true);
        }


        GameManager.gManager.raceStarted = false;
        foreach (GameObject racerOBJ in GameManager.gManager.players)
        {
            RacerDetails rDeets = racerOBJ.GetComponent<RacerDetails>();
            //if (GetComponentInChildren<IsShipCollider>().gameObject != null && GetComponentInChildren<IsShipCollider>() != null)
            //{
            //    GetComponentInChildren<IsShipCollider>().shipControls = sControls;
            //}

            rDeets.finishedRacing = false;
            rDeets.crossedFinishLine = false;
            
            rb.velocity = new Vector3(0, 0, 0);
            rb.angularVelocity = new Vector3(0, 0, 0);
            sControls.ResetAcceleration();
            rb.isKinematic = true;
            DisableShipControls();
        }
    }
}

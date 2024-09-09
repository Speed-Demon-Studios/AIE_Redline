using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class PlayerInputScript : MonoBehaviour
{
    public PlayerInput player;
    public MultiplayerEventSystem eventSystem;
    private ShipsControls m_shipControls;
    [SerializeField] private Camera m_cam;
    private int m_playerNumber;
    private GameManager gMan;
    public bool playerReadyInMenu;

    private float m_currentPOV;
    private float m_desiredPOV;
    public float lerpTime;
    public float minPOV;
    public float maxPOV;

    // Start is called before the first frame update
    void Awake()
    {
        m_shipControls = GetComponentInParent<ShipsControls>();
        //m_cam = GetComponentInChildren<Camera>();
        gMan = GameManager.gManager;

        if(gMan != null)
            gMan.players.Add(gameObject);
        if (gMan != null)
            m_playerNumber = gMan.numberOfPlayers;
        if (gMan != null)
            eventSystem.firstSelectedGameObject = gMan.FindStartButton();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (GameManager.gManager.raceStarted == false && m_shipControls.enabled == true)
        //{
        //    m_shipControls.enabled = false;
        //}
        //
        //if (GameManager.gManager.raceStarted == true && m_shipControls.enabled == false)
        //{
        //    m_shipControls.enabled = true;
        //}
        //if(gMan.raceStarted)
            CalculatePOV();
    }

    private void CalculatePOV()
    {
        float speedPercentage = m_shipControls.ReturnRB().velocity.magnitude / m_shipControls.variant.DefaultMaxSpeed;
        if(speedPercentage > 0.001)
        {
            m_desiredPOV = ((maxPOV - minPOV) * speedPercentage) + minPOV;
        }
        else
        {
            m_desiredPOV = minPOV;
        }

        m_currentPOV = Mathf.Lerp(m_currentPOV, m_desiredPOV, lerpTime);
        m_cam.fieldOfView = m_currentPOV;

    }

    public void Brake(InputAction.CallbackContext context)
    {
        Debug.Log(context.ReadValue<float>());
        if (m_shipControls != null)
        {
            m_shipControls.SetBrakeMultiplier(context.ReadValue<float>());
        }
    }

    public void Accelerate(InputAction.CallbackContext context)
    {
        Debug.Log(context.ReadValue<float>());
        if(m_shipControls != null)
        {
            m_shipControls.SetSpeedMultiplier(context.ReadValue<float>());
        }
    }

    public void Turn(InputAction.CallbackContext context)
    {
        if (m_shipControls != null)
        {
            m_shipControls.SetTurnMultipliers(context.ReadValue<float>() * 0.6f);
        }
    }

    public void Strafe(InputAction.CallbackContext context)
    {
        if (m_shipControls != null)
        {
            m_shipControls.SetStrafeMultiplier(context.ReadValue<float>() * 0.45f);
        }
    }

    public void Boost(InputAction.CallbackContext context)
    {
        if (m_shipControls != null)
        {
            m_shipControls.IsBoosting();
        }
    }

    public void ChangeActionMap(string map)
    {
        player.SwitchCurrentActionMap(map);
    }
    //private void OnEnable()
    //{
    //    if (player != null)
    //    {
    //        player.FindAction("Move").started += Move;
    //        player.FindAction("Turn").started += Turn;
    //        player.Enable();
    //    }
    //}
    //
    //private void OnDisable()
    //{
    //    if (player != null)
    //    {
    //        player.FindAction("Move").started -= Move;
    //        player.FindAction("Turn").started -= Turn;
    //        player.Disable();
    //    }
    //}
}

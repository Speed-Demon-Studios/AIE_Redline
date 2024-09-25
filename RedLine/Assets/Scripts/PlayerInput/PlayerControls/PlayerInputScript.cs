using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class PlayerInputScript : MonoBehaviour
{
    public PlayerInput player;
    public MultiplayerEventSystem eventSystem;
    private ShipsControls m_shipControls;
    private Gamepad m_playerGamepad;

    [SerializeField] private Camera m_cam;
    private int m_playerNumber;
    public void SetPlayerNumber(int number) { m_playerNumber = number; }
    public int GetPlayerNumber() { return m_playerNumber; }
    private GameManager gMan;
    public bool playerReadyInMenu;
    private ShipSelection m_selection;
    public void SetSelection(ShipSelection selection) { m_selection = selection; }

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
        if (gMan != null && m_playerNumber != 1)
            gMan.uiCInput.ResetFirstButtonSelect(m_playerNumber - 1);


        if (player != null)
        {
            AssignController();
        }
        
        //GameManager.gManager.hapticsController.ConfigureRumble(thisGamepad);
    }

    public void PlayerDisconnect()
    {
        GameManager.gManager.uiCInput.DeleteSelection(m_selection.gameObject);
        Destroy(m_selection.gameObject);
        for(int i = m_playerNumber - 1; i < GameManager.gManager.players.Count; i++)
        {
            if (GameManager.gManager.players[i].GetComponent<PlayerInputScript>().GetPlayerNumber() != m_playerNumber)
            {
                GameManager.gManager.players[i].GetComponent<PlayerInputScript>().SetPlayerNumber(i);
            }
        }
        if (GameManager.gManager.players.Contains(this.gameObject))
        {
            GameManager.gManager.players.Remove(this.gameObject);
        }
        if (GameManager.gManager.playerObjects.Contains(this.gameObject))
        {
            GameManager.gManager.playerObjects.Remove(this.gameObject);
        }
        gMan.numberOfPlayers -= 1;
        gMan.uiCInput.SetNumberOfPlayers(gMan.uiCInput.GetNumberOfPlayers() - 1);
        Destroy(this.gameObject);
    }

    public Gamepad GetPlayerGamepad()
    {
        return m_playerGamepad;
    }

    private void AssignController()
    {
        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            if (Gamepad.all[i] != null && player.GetDevice<Gamepad>() != null)
            {
                if (Gamepad.all[i].deviceId == player.GetDevice<Gamepad>().deviceId)
                {
                    Debug.Log("FOUND CONTROLLER (Device ID: " + player.GetDevice<Gamepad>().deviceId + ")");
                    m_playerGamepad = Gamepad.all[i];
                    break;
                }
            }
        }
        return;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(gMan.raceStarted)
            CalculatePOV();
    }

    private void CalculatePOV()
    {
        float speedPercentage = m_shipControls.ReturnRB().velocity.magnitude / m_shipControls.variant.DefaultMaxSpeed;
        //if(speedPercentage > 0.001)
        //{
        //    
        //    //m_desiredPOV = ((maxPOV - minPOV) * speedPercentage) + minPOV;
        //}
        //else
        //{
        //    m_desiredPOV = minPOV;
        //}
        m_desiredPOV = Mathf.Lerp(minPOV, maxPOV, speedPercentage);

        m_currentPOV = Mathf.Lerp(m_currentPOV, m_desiredPOV, lerpTime);
        m_cam.fieldOfView = m_currentPOV;

    }

    public void OnRight(InputAction.CallbackContext context)
    {
        if(m_selection != null)
        {
            if(context.performed && !playerReadyInMenu)
                m_selection.OnNext();
        }
    }

    public void OnLeft(InputAction.CallbackContext context)
    {
        if (m_selection != null)
        {
            if (context.performed && !playerReadyInMenu)
                m_selection.OnPrev();
        }
    }

    public void Pause(InputAction.CallbackContext context)
    {
        if(GameManager.gManager != null)
        {
            GameManager.gManager.StopTime();
        }
    }

    public void Brake(InputAction.CallbackContext context)
    {
        if (m_shipControls != null)
        {
            m_shipControls.SetBrakeMultiplier(context.ReadValue<float>());
        }
    }

    public void Accelerate(InputAction.CallbackContext context)
    {
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

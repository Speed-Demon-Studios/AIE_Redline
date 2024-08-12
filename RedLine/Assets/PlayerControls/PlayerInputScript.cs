using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputScript : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputAsset;
    private InputActionMap player;
    private ShipsControls m_shipControls;
    private Camera m_cam;
    public bool test = true;

    private float m_currentPOV;
    private float m_desiredPOV;
    public float lerpTime;
    public float minPOV;
    public float maxPOV;

    // Start is called before the first frame update
    void Start()
    {
        m_shipControls = GetComponent<ShipsControls>();
        m_cam = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
       // if (Input.GetKeyDown(KeyCode.T))
       // {
       //     test = !test;
       // }
       //
       // if (test)
       // {
       // }
            CalculatePOV();
    }

    private void CalculatePOV()
    {
        float speedPercentage = m_shipControls.ReturnRB().velocity.magnitude / m_shipControls.Variant.MaxSpeed;
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

    public void Move(InputAction.CallbackContext context)
    {
        if (m_shipControls != null)
        {
            m_shipControls.SetSpeedMultiplier(context.ReadValue<float>());
        }
    }

    public void Turn(InputAction.CallbackContext context)
    {
        if (m_shipControls != null)
        {
            m_shipControls.SetTurnMultipliers(context.ReadValue<float>());
        }
    }


    private void OnEnable()
    {
        if (player != null)
        {
            player.FindAction("Move").started += Move;
            player.FindAction("Turn").started += Turn;
            player.Enable();
        }
    }
    
    private void OnDisable()
    {
        if (player != null)
        {
            player.FindAction("Move").started -= Move;
            player.FindAction("Turn").started -= Turn;
            player.Disable();
        }
    }
}

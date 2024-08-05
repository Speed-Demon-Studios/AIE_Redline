using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputScript : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputAsset;
    private InputActionMap player;
    private ShipsControls m_shipControls;

    public float minPOV;
    public float maxPOV;

    // Start is called before the first frame update
    void Start()
    {
        m_shipControls = GetComponent<ShipsControls>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShipPOV()
    {

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

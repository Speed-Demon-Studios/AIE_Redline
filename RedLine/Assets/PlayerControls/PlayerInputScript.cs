using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputScript : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputAsset;
    private InputActionMap player;
    private PlayerInput _input;
    private ShipsControls m_shipControls;

    // Start is called before the first frame update
    void Start()
    {
        m_shipControls = GetComponent<ShipsControls>();
    }

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        
    }

    // Update is called once per frame
    void Update()
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

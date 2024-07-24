using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    PlayerInput _input;
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
        m_shipControls.SetSpeedMultiplier(context.ReadValue<Vector2>().y);
    }

    public void Turn(InputAction.CallbackContext context)
    {
        m_shipControls.SetTurnMultiplier(context.ReadValue<float>());
        Debug.Log(context.ReadValue<float>());
    }
}

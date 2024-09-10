using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerHaptics : MonoBehaviour
{

    private void Awake()
    {
        GameManager.gManager.hapticsController = this;
    }

    public void ConfigureRumble(Gamepad controller)
    {
        controller.SetMotorSpeeds(0.25f, 0.75f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

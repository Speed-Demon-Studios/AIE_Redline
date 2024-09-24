using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerHaptics : MonoBehaviour
{
    private PlayerInputScript m_pInputScript;

    [SerializeField] private float m_defaultRumble = 0.0f;
    [SerializeField] private float m_boost1Rumble = 0.012f;
    [SerializeField] private float m_boost2Rumble = 0.15f;
    [SerializeField] private float m_boost3Rumble = 0.45f;

    public float rumbleDurationAtTarget = 0.0f;
    private float currentRumble = 0.0f;
    private float targetRumble = 0.0f;

    private bool rumbling = false;
    private bool m_rumbleReady = false;
    private bool m_stopRumble = false;
    private bool chargeUpRumble = false;


    private void Awake()
    {
        //GameManager.gManager.hapticsController = this;
        m_pInputScript = this.gameObject.GetComponent<PlayerInputScript>();
    }

    public void ConfigureRumble(Gamepad controller = null, float rumbleLevel = 0.0f)
    {
        if (controller != null)
        {
            controller.SetMotorSpeeds(rumbleLevel, rumbleLevel);
        }
        //else if (controller == null) // If a gamepad is NOT passed through into this function, this will configure the rumble for ALL connected gamepads.
        //{
        //    for (int i = 0; i < Gamepad.all.Count; i++)
        //    {
        //        Gamepad.all[i].SetMotorSpeeds(0.012f, m_defaultRumble);
        //    }
        //}
    }

    public void RumbleTiming(Gamepad controller = null, int rumbleType = 0, float duration = 0)
    {
        currentRumble = 0.0f;
        targetRumble = 0.0f;
        rumbleDurationAtTarget = duration;

        switch (rumbleType)
        {
            case 0:
                {
                    break;
                }
            case 1:
                {
                    targetRumble = m_boost1Rumble;
                    break;
                }
            case 2:
                {
                    targetRumble = m_boost2Rumble;
                    break;
                }
            case 3:
                {
                    targetRumble = m_boost3Rumble;
                    break;
                }
        }

        if (rumbleType > 0)
        {
            m_rumbleReady = true;
            chargeUpRumble = true;
        }
        return;
    }

    private IEnumerator WaitRumbleDuration()
    {
        Debug.Log("Waiting Rumble Duration");
        yield return new WaitForSecondsRealtime(rumbleDurationAtTarget);
        m_stopRumble = true;
        Debug.Log("Rumble Powering Down");
        StopCoroutine(WaitRumbleDuration());
    }

    // Update is called once per frame
    void Update()
    {
        if (m_pInputScript != null && m_pInputScript.GetPlayerGamepad() != null)
        {
            if (m_rumbleReady == true)
            {
                if (chargeUpRumble == true)
                {
                    ConfigureRumble(m_pInputScript.GetPlayerGamepad(), targetRumble);
                    //if (currentRumble < targetRumble)
                    //{
                    //    currentRumble = targetRumble;
                    //}
                    //if (currentRumble >= targetRumble)
                    //{
                        //currentRumble = targetRumble;
                        chargeUpRumble = false;
                        rumbling = true;
                        Debug.Log("Rumble Charged");
                        StartCoroutine(WaitRumbleDuration());
                    //}
                }
                else if (chargeUpRumble == false && m_stopRumble == true)
                {
                    targetRumble = 0.0f;
                    if (currentRumble > targetRumble)
                    {
                        currentRumble -= 1.1f * Time.deltaTime;
                        ConfigureRumble(m_pInputScript.GetPlayerGamepad(), currentRumble);
                        Debug.Log("Powering Down Rumble: " + currentRumble);
                    }
                    if (currentRumble <= targetRumble)
                    {
                        currentRumble = targetRumble;
                        rumbling = false;
                        m_stopRumble = false;
                        m_pInputScript.GetPlayerGamepad().PauseHaptics();
                        ConfigureRumble(m_pInputScript.GetPlayerGamepad(), currentRumble);
                    }
                }
            }
        }
        else
        {
            m_rumbleReady = false;
        }
    }
}

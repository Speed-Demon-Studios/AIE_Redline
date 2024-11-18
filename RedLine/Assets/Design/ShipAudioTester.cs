using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAudioTester : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 1f)]
    float m_shipSpeed;

    [SerializeField]
    AudioSource m_mainEngine, m_hum, m_wind;

    [SerializeField]
    AnimationCurve m_mainEnginePitch, m_humPitch, m_windPitch, m_mainEngineVolume, m_humVolume, m_windVolume;

    private void FixedUpdate()
    {
        m_mainEngine.pitch = m_mainEnginePitch.Evaluate(m_shipSpeed);
        m_hum.pitch = m_humPitch.Evaluate(m_shipSpeed);
        m_wind.pitch = m_windPitch.Evaluate(m_shipSpeed);

        m_mainEngine.volume = m_mainEngineVolume.Evaluate(m_shipSpeed);
        m_hum.volume = m_humVolume.Evaluate(m_shipSpeed);
        m_wind.volume = m_windVolume.Evaluate(m_shipSpeed);
    }
    //
}

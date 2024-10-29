using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using Unity.VisualScripting;

public class PlayerAudioController : MonoBehaviour
{
                                                                                                            // ------------------------------------------------------------------------|
    [Header("Engine Audio")]                                                                                //                                                                         |
    public List<FMODUnity.EventReference> m_engineAudioInfo = new List<FMODUnity.EventReference>();         // References to the FMOD audio "Events".                                  |
    public List<FMODUnity.StudioEventEmitter> m_engineEmitters = new List<FMODUnity.StudioEventEmitter>();  // References to the FMOD "StudioEventEmitter" components for the engines. |
    public List<float> m_engineEmitterPitches = new List<float>();                                          // Pitch/Frequency float variables.                                        |
    public List<float> m_engineEmitterVolumes = new List<float>();                                          // Volume float variables.                                                 |
                                                                                                            //                                                                         |
                                                                                                            // ------------------------------------------------------------------------|
    [Space]                   
    [Space]                   
                              
                                                                                                            // ------------------------------------------------------------------------|
    [Header("Redline Audio")]                                                                               //                                                                         |
    public List<FMODUnity.EventReference> m_redlineAudioInfo = new List<FMODUnity.EventReference>();        // References to the FMOD audio "Events".                                  |
    public List<FMODUnity.StudioEventEmitter> m_redlineEmitters = new List<FMODUnity.StudioEventEmitter>(); // References to the FMOD "StudioEventEmitter" components for the redline. |
    public List<float> m_redlineEmitterPitches = new List<float>();                                         // Pitch/Frequency float variables.                                        |
    public List<float> m_redlineEmitterVolumes = new List<float>();                                         // Volume float variables.                                                 |
                                                                                                            //                                                                         |
                                                                                                            // ------------------------------------------------------------------------|
    // Start is called before the first frame update
    void Start()
    {
                                                                                                            // ----------------------------------------------------|
        FMODUnity.StudioEventEmitter currentEmitter;                                                        // Reference to the StudioEventEmitter to be altered.  |
        for (int i = 0; i < m_engineEmitters.Count; i++)                                                    // Iterate through the "m_engineEmitters" list.        |
        {                                                                                                   //                                                     |
            currentEmitter = m_engineEmitters[i];                                                           // Set the reference for the 'CurrentEmitter'.         |
            currentEmitter.EventReference = m_engineAudioInfo[i];                                           // Set the audio FMOD 'Event' for the current emitter. |
        }                                                                                                   //                                                     |
                                                                                                            // ----------------------------------------------------|
                                                                                                            //                                                     |
        for (int i = 0; i < m_redlineEmitters.Count; i++)                                                   // Iterate through the "m_redlineEmitters" list.       |
        {                                                                                                   //                                                     |
            currentEmitter = m_redlineEmitters[i];                                                          // Set the reference for the 'CurrentEmitter'.         |
            currentEmitter.EventReference = m_redlineAudioInfo[i];                                          // Set the audio FMOD 'Event' for the current emitter. |
        }                                                                                                   //                                                     |
                                                                                                            // ----------------------------------------------------|
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < m_engineEmitters.Count; i++)
        {
            FMODUnity.StudioEventEmitter currentEngineEmitter = m_engineEmitters[i];
            currentEngineEmitter.EventInstance.setPitch(m_engineEmitterPitches[i]);
            currentEngineEmitter.EventInstance.setVolume(m_engineEmitterVolumes[i]);
        }

        for (int i = 0; i < m_redlineEmitters.Count; i++)
        {
            FMODUnity.StudioEventEmitter currentRedlineEmitter = m_redlineEmitters[i];
            currentRedlineEmitter.EventInstance.setPitch(m_redlineEmitterPitches[i]);
            currentRedlineEmitter.EventInstance.setVolume(m_redlineEmitterVolumes[i]);
        }
    }



    public void StartEngineHum()
    {
        foreach (StudioEventEmitter emitters in m_engineEmitters)
        {
            emitters.Play();
        }
    }

    public void StartRedlineSounds()
    {
        foreach (StudioEventEmitter emitters in m_redlineEmitters)
        {
            emitters.Play();
        }
    }
}

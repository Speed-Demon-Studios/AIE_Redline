using System.Collections.Generic;
using UnityEngine;


namespace EAudioSystem
{
    using FMODUnity;

    public class PlayerAudioController : MonoBehaviour
    {
        [Header("Engine Audio")]                                                                                                    // Engine Audio -----------------------------------------------------------|
        [SerializeField] private EventReference[] m_engineAudioInfo;                                                                // References to the FMOD audio "Events".                                  |
        [SerializeField] private StudioEventEmitter[] m_engineEmitters;                                                             // References to the FMOD "StudioEventEmitter" components for the engines. |
        [SerializeField] private float[] m_engineEmitterPitches;                                                                    // Pitch/Frequency float variables.                                        |
        [SerializeField] private float[] m_engineEmitterVolumes;                                                                    // Volume float variables.                                                 |
                                                                                                                                    // ------------------------------------------------------------------------|
        [Space]
        [Space]


        [Header("Redline Audio")]                                                                                                   // Redline Audio ----------------------------------------------------------|
        [SerializeField] private EventReference[] m_redlineAudioInfo;                                                               // References to the FMOD audio "Events".                                  |
        [SerializeField] private StudioEventEmitter[] m_redlineEmitters;                                                            // References to the FMOD "StudioEventEmitter" components for the redline. |
        [SerializeField] private float[] m_redlineEmitterPitches;                                                                   // Pitch/Frequency float variables.                                        |
        [SerializeField] private float[] m_redlineEmitterVolumes;                                                                   // Volume float variables.                                                 |
                                                                                                                                    // ------------------------------------------------------------------------|

        void Start()                                                                                                                // ----------------------------------------------------|
        {                                                                                                                           //                                                     |
            StudioEventEmitter currentEmitter;                                                                                      // Reference to the StudioEventEmitter to be altered.  |
                                                                                                                                    //                                                     |
            for (int i = 0; i < m_engineEmitters.Length; i++)                                                                       // Iterate through the "m_engineEmitters" list.        |
            {                                                                                                                       //                                                     |
                currentEmitter = m_engineEmitters[i];                                                                               // Set the reference for the 'CurrentEmitter'.         |
                currentEmitter.EventReference = m_engineAudioInfo[i];                                                               // Set the audio FMOD 'Event' for the current emitter. |
            }                                                                                                                       //                                                     |
                                                                                                                                    //                                                     |
            for (int i = 0; i < m_redlineEmitters.Length; i++)                                                                      // Iterate through the "m_redlineEmitters" list.       |
            {                                                                                                                       //                                                     |
                currentEmitter = m_redlineEmitters[i];                                                                              // Set the reference for the 'CurrentEmitter'.         |
                currentEmitter.EventReference = m_redlineAudioInfo[i];                                                              // Set the audio FMOD 'Event' for the current emitter. |
            }                                                                                                                       //                                                     |
        }                                                                                                                           // ----------------------------------------------------|

        void Update()
        {
            for (int i = 0; i < m_engineEmitters.Length; i++)
            {
                StudioEventEmitter currentEngineEmitter = m_engineEmitters[i];
                currentEngineEmitter.EventInstance.setPitch(m_engineEmitterPitches[i]);
                currentEngineEmitter.EventInstance.setVolume(m_engineEmitterVolumes[i]);
            }
    
            for (int i = 0; i < m_redlineEmitters.Length; i++)
            {
                StudioEventEmitter currentRedlineEmitter = m_redlineEmitters[i];
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
};

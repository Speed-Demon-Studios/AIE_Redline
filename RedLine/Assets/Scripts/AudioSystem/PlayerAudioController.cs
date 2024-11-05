using System.Collections.Generic;
using UnityEngine;


namespace EAudioSystem
{
    using FMODUnity;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using UnityEditor.ShaderGraph.Internal;

    public class PlayerAudioController : MonoBehaviour
    {
        [Header("Engine Audio")]                                                                                                    // Engine Audio -----------------------------------------------------------|
        [SerializeField] private List<EventReference> m_engineAudioInfo = new List<EventReference>();                               // References to the FMOD audio "Events".                                  |
        [SerializeField] private StudioEventEmitter[] m_engineEmitters;                                                             // References to the FMOD "StudioEventEmitter" components for the engines. |
        [SerializeField] private List<float> m_engineEmitterPitches = new List<float>();                                            // Pitch/Frequency float variables.                                        |
        [SerializeField] private List<float> m_engineEmitterVolumes = new List<float>();                                            // Volume float variables.                                                 |
        [SerializeField] private List<float> m_engineEmitterValueCaps = new List<float>();
        [SerializeField] private List<float> m_engineEmitterValueMultipliers = new List<float>();
                                                                                                                                    // ------------------------------------------------------------------------|
        [Space]
        [Space]


        [Header("Redline Audio")]                                                                                                   // Redline Audio ----------------------------------------------------------|
        [SerializeField] private List<EventReference> m_redlineAudioInfo = new List<EventReference>();                              // References to the FMOD audio "Events".                                  |
        [SerializeField] private StudioEventEmitter[] m_redlineEmitters;                                                            // References to the FMOD "StudioEventEmitter" components for the redline. |
        [SerializeField] private float[] m_redlineEmitterPitches;                                                                   // Pitch/Frequency float variables.                                        |
        [SerializeField] private float[] m_redlineEmitterVolumes;                                                                   // Volume float variables.                                                 |
                                                                                                                                    // ------------------------------------------------------------------------|
        public bool variantSet = false;

        public void SetEngineAudios(List<EventReference> newAudioEvents)
        {
            m_engineAudioInfo = new List<EventReference>();
            foreach (EventReference audioEvent in newAudioEvents)
            {
                m_engineAudioInfo.Add(audioEvent);
            }

            for (int i = 0; i < m_engineEmitters.Count(); i++)
            {
                m_engineEmitters[i].EventReference = m_engineAudioInfo[i];
            }
        }

        public void UpdatePitch(int index, float amount, float maxValue, bool addDT, bool add, bool subtract, float minimumValue = 999.0f)
        {
            if (add == true && subtract == true || add == false && subtract == false)
            {
                int playerListIndex = 0;
                for (int i = 0; i < GameManager.gManager.players.Count; i++)
                {
                    if (GameManager.gManager.players[i] = this.gameObject)
                    {
                        playerListIndex = i;
                        break;
                    }
                }
                if (add == true && subtract == true)
                {
                    Debug.LogError("Player [" + (playerListIndex + 1) + "] |PlayerAudioController.cs| UpdatePitch() Function Invoke Error :: Conflicting Parameters. BOOL 'add' & BOOL 'subtract' are both TRUE.");
                }
                else if (add == false && subtract == false)
                {
                    Debug.LogError("Player [" + (playerListIndex + 1) + "] |PlayerAudioController.cs| UpdatePitch() Function Invoke Error :: Conflicting Parameters. BOOL 'add' & BOOL 'subtract' are both FALSE.");
                }
                return;
            }
            else
            {
                if (subtract == true)
                {
                    add = false;
                }
                if (add == true)
                {
                    subtract = false;
                }
            }

            if (addDT == true)
            {
                if (add == true)
                {
                    if (m_engineEmitterPitches[index] < maxValue)
                    {
                        m_engineEmitterPitches[index] += amount * Time.deltaTime;
                    }
                }
                
                if (subtract == true)
                {
                    if (minimumValue == 999.0f)
                    {
                        if (m_engineEmitterPitches[index] <= 0.33f && m_engineEmitterPitches[index] > -0.7f)
                        {
                            m_engineEmitterPitches[index] = -0.7f;
                        }
                        m_engineEmitterPitches[index] -= amount * Time.deltaTime;
                    }
                    else
                    {
                        m_engineEmitterPitches[index] -= amount * Time.deltaTime;
                        if (m_engineEmitterPitches[index] < minimumValue)
                        {
                            m_engineEmitterPitches[index] = minimumValue;
                        }
                    }
                }
            }
            else if (addDT == false)
            {
                if (add == true)
                {
                    m_engineEmitterPitches[index] += amount;
                }

                if (subtract == true)
                {
                    m_engineEmitterPitches[index] -= amount;
                }
            }
        }

        public void UpdateVolume(int index, float amount, float maxValue, bool addDT, bool add, bool subtract, float minValue = 999.0f)
        {
            if (add == true && subtract == true || add == false && subtract == false)
            {
                int playerListIndex = 0;
                for (int i = 0; i < GameManager.gManager.players.Count; i++)
                {
                    if (GameManager.gManager.players[i] = this.gameObject)
                    {
                        playerListIndex = i;
                        break;
                    }
                }
                if (add == true && subtract == true)
                {
                    Debug.LogError("Player [" + (playerListIndex + 1) + "] |PlayerAudioController.cs| UpdateVolume() Function Invoke Error :: Conflicting Parameters. BOOL 'add' & BOOL 'subtract' are both TRUE.");
                }
                else if (add == false && subtract == false)
                {
                    Debug.LogError("Player [" + (playerListIndex + 1) + "] |PlayerAudioController.cs| UpdateVolume() Function Invoke Error :: Conflicting Parameters. BOOL 'add' & BOOL 'subtract' are both FALSE.");
                }
                return;
            }
            else
            {
                if (subtract == true)
                {
                    add = false;
                }
                if (add == true)
                {
                    subtract = false;
                }
            }

            if (addDT == true)
            {
                if (add == true)
                {
                    if (m_engineEmitterVolumes[index] < maxValue)
                    {
                        m_engineEmitterVolumes[index] += amount * Time.deltaTime;
                    }
                    if (m_engineEmitterVolumes[index] > maxValue)
                    {
                        m_engineEmitterVolumes[index] = maxValue;
                    }
                }

                if (subtract == true)
                {
                    if (minValue == 999.0f)
                    {
                        if (m_engineEmitterVolumes[index] > 0.0f)
                        {
                            m_engineEmitterVolumes[index] -= amount * Time.deltaTime;
                        }
                        if (m_engineEmitterVolumes[index] < 0.0f)
                        {
                            m_engineEmitterVolumes[index] = 0.0f;
                        }
                    }
                    else
                    {
                        if (m_engineEmitterVolumes[index] > minValue)
                        {
                            m_engineEmitterVolumes[index] -= amount * Time.deltaTime;
                        }
                        if (m_engineEmitterVolumes[index] < minValue)
                        {
                            m_engineEmitterVolumes[index] = minValue;
                        }
                    }
                }
            }

        }



        void Start()                                                                                                                // ----------------------------------------------------|
        {                                                                                                                           //                                                     |
            StudioEventEmitter currentEmitter;                                                                                      // Reference to the StudioEventEmitter to be altered.  |
                                                                                                                                    //                                                     |
            //for (int i = 0; i < m_engineEmitters.Length; i++)                                                                     // Iterate through the "m_engineEmitters" list.        |
            //{                                                                                                                     //                                                     |
            //    currentEmitter = m_engineEmitters[i];                                                                             // Set the reference for the 'CurrentEmitter'.         |
            //    currentEmitter.EventReference = m_engineAudioInfo[i];                                                             // Set the audio FMOD 'Event' for the current emitter. |
            //}                                                                                                                     //                                                     |
            //                                                                                                                      //                                                     |
            //for (int i = 0; i < m_redlineEmitters.Length; i++)                                                                    // Iterate through the "m_redlineEmitters" list.       |
            //{                                                                                                                     //                                                     |
            //    currentEmitter = m_redlineEmitters[i];                                                                            // Set the reference for the 'CurrentEmitter'.         |
            //    currentEmitter.EventReference = m_redlineAudioInfo[i];                                                            // Set the audio FMOD 'Event' for the current emitter. |
            //}                                                                                                                     //                                                     |
        }                                                                                                                           // ----------------------------------------------------|

        void Update()
        {
            if (variantSet == true)
            {
                for (int i = 0; i < m_engineEmitters.Length; i++)
                {
                    StudioEventEmitter currentEngineEmitter = m_engineEmitters[i];
                    currentEngineEmitter.EventInstance.setPitch(m_engineEmitterPitches[i]);
                    currentEngineEmitter.EventInstance.setVolume(m_engineEmitterVolumes[i]);
                }
    
                //for (int i = 0; i < m_redlineEmitters.Length; i++)
                //{
                //    StudioEventEmitter currentRedlineEmitter = m_redlineEmitters[i];
                //    currentRedlineEmitter.EventInstance.setPitch(m_redlineEmitterPitches[i]);
                //    currentRedlineEmitter.EventInstance.setVolume(m_redlineEmitterVolumes[i]);
                //}
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

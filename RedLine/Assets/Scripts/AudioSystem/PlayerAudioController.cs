using System.Collections.Generic;
using UnityEngine;


namespace EAudioSystem
{
    using FMODUnity;
    using System.Linq;

    public class PlayerAudioController : MonoBehaviour
    {
        public bool variantSet = false; // BOOLEAN value to tell the game whether or not the ship variant has been selected and set-up.

        // Engine Audio -------------------------------------------------------------------------------------------------------------------------------------
        [Header("Engine Audio")]
        [SerializeField] private StudioEventEmitter[] m_engineEmitters; // References to the FMOD "StudioEventEmitter" components for the engines.
        [SerializeField] private List<float> m_engineEmitterPitches = new(); // Pitch/Frequency float variables.
        [SerializeField] private List<float> m_engineEmitterVolumes = new(); // Audio Volume float variables.
        private List<EventReference> m_engineAudioInfo = new(); // References to the FMOD audio Events for the ship ENGINE.
        private List<float> m_maxEnginePitches = new(); // Default MAX pitch values for the individual engine sounds.
        private List<float> m_maxEngineVolumes = new(); // Default MAX volume values for the individual engine sounds.
        //---------------------------------------------------------------------------------------------------------------------------------------------------

        [Space]
        [Space]

        // Gameplay Audio -----------------------------------------------------------------------------------------------------------------------------------
        [Header("Gameplay Audio")]
        [SerializeField] private List<StudioEventEmitter> m_gameplaySoundEmitters = new(); // All references to the audio event emitters for gameplay sounds.
        [SerializeField] private List<EventReference> m_gameplayAudioInfo = new(); // All references to the FMOD audio events that are for gameplay (e.g. wall crashing sound, sparks sound, etc..)
        [SerializeField] private List<float> m_gameplayAudioPitches = new(); // List of frequency/pitch values for each individual gameplay sound.
        [SerializeField] private List<float> m_gameplayAudioVolumes = new(); // List of audio volume values for each individual gameplay sound.
        //---------------------------------------------------------------------------------------------------------------------------------------------------

        public void PlayGPSFX(int soundIndex)
        {
            switch (soundIndex)
            {
                // Wall Crash SFX.
                case 0:
                {
                    if (m_gameplayAudioInfo[soundIndex].IsNull == false && m_gameplaySoundEmitters[0] != null)
                    {
                            if (m_gameplaySoundEmitters[0].EventReference.IsNull == true)
                            {
                                m_gameplaySoundEmitters[0].EventReference = m_gameplayAudioInfo[0];
                            }

                            m_gameplaySoundEmitters[0].Play();
                    }
                    break;
                }
            }
        }

        // Set up the default modulation values for the engine sounds (Pitch, Volume).
        public void SetDefaultEngineModulations(float PitchValue, float VolumeValue)
        {
            m_maxEnginePitches.Add(PitchValue);
            m_maxEngineVolumes.Add(VolumeValue);
        }

        // Set up the variables, emitters and 'events' for engine audio, based off of the selected ship variation.
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

        // Update engine sound pitch. passing in values for the index of the sound you want to update, the amount you want to update it by, the maximum value, whether to add DeltaTime,
        // whether you are adding or subtracting, the minimum value - which has a default of '999.0' which will bypass the minimum, and wheather or not you want to use the default MAX value set in the editor.
        public void UpdateEnginePitch(int index, float amount, float maxValue, bool addDT, bool add, bool subtract, float minimumValue = 999.0f, bool useDefaultMax = false)
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

            if (float.IsNaN(m_engineEmitterPitches[index]) == true)
            {
                m_engineEmitterPitches[index] = 0.05f;
                m_engineEmitterPitches[index] = minimumValue;
            }

            if (addDT == true)
            {
                if (add == true)
                {
                    if (useDefaultMax == false)
                    {
                        if (m_engineEmitterPitches[index] < maxValue)
                        {
                            m_engineEmitterPitches[index] += amount * Time.deltaTime;
                        }
                        if (m_engineEmitterPitches[index] > maxValue)
                        {
                            m_engineEmitterPitches[index] = maxValue;
                        }
                    }
                    else
                    {
                        if (m_engineEmitterPitches[index] < m_maxEnginePitches[index])
                        {
                            m_engineEmitterPitches[index] += amount * Time.deltaTime;
                        }

                        if (m_engineEmitterPitches[index] > m_maxEnginePitches[index])
                        {
                            m_engineEmitterPitches[index] = m_maxEnginePitches[index];
                        }
                    }
                }
                
                if (subtract == true)
                {
                    if (minimumValue >= 800.0f)
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
        }

        // Update engine sound volume. passing in values for the index of the sound you want to update, the amount you want to update it by, the maximum value, whether to add DeltaTime,
        // whether you are adding or subtracting, the minimum value - which has a default of '999.0' which will bypass the minimum, and wheather or not you want to use the default MAX value set in the editor.
        public void UpdateEngineVolume(int index, float amount, float maxValue, bool addDT, bool add, bool subtract, float minValue = 999.0f, bool useDefaultMax = false)
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

            if (m_engineEmitters[index] == null || m_engineEmitters[index].EventReference.IsNull == true)
            {
                return;
            }

            Debug.Log("[" + index + "] Max Volume: " + maxValue);
            if (useDefaultMax == true)
            {
                maxValue = m_maxEngineVolumes[index];
            }
            Debug.Log("[" + index + "] Max Volume: " + maxValue);

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
                    if (minValue > 500.0f)
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
                    if (m_engineEmitters[i] != null)
                    {
                        StudioEventEmitter currentEngineEmitter = m_engineEmitters[i];
                        currentEngineEmitter.EventInstance.setPitch(m_engineEmitterPitches[i]);
                        currentEngineEmitter.EventInstance.setVolume(m_engineEmitterVolumes[i]);
                    }
                }
            }
        }
    
        public void StartEngineHum()
        {
            foreach (StudioEventEmitter emitters in m_engineEmitters)
            {
                emitters.Play();
            }
        }
    }
};

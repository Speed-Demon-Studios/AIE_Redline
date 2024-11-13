using System.Collections.Generic;
using UnityEngine;


namespace EAudioSystem
{
    using FMODUnity;
    using System.Collections;
    using System.Data;
    using System.Linq;
    using Unity.VisualScripting;

    public class PlayerAudioController : MonoBehaviour
    {
        // TODO:
        // * Finish gameplay audio functionality
        // * Update the funcionality for the engine sounds, allowing variation between the different difficulty modes, and so the engine sounds get quieter after a race has been completed.
        // * Update generalised functionality for the entire audio system so values get reset and deleted properly at the end of a race so the player can return to the main menu without getting errors.
        // * Add scaling to all engine sounds so they scale when boosting then go back to the normal range SMOOTHLY when a boost has finished.
        // * Remove the debugging custom error stuff from this script and the UIAudioController.cs script, and possibly add them into their own script to clean up the code in those scripts.
        // * Redline audio (e.g. gaining redline, reaching the different levels of redline, etc...).
        // * Update the funcitionality for the engine audio so that when a players speed lowers while they are turning or crashing into a wall, the sound modulation will reflect those changes.

        [HideInInspector] public bool variantSet = false; // BOOLEAN value to tell the game whether or not the ship variant has been selected and set-up.
        [HideInInspector] public bool resettingAudio = false;

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
        [SerializeField] private List<StudioEventEmitter> m_windEmitters = new(); // All references to the FMOD event emitters for wind sounds.
        [SerializeField] private List<EventReference> m_windAudioInfo = new();
        [SerializeField] private List<float> m_windAudioVolumes = new();

        [SerializeField] private List<StudioEventEmitter> m_gameplaySoundEmitters = new(); // All references to the audio event emitters for gameplay sounds.
        [SerializeField] private List<EventReference> m_gameplayAudioInfo = new(); // All references to the FMOD audio events that are for gameplay (e.g. wall crashing sound, sparks sound, etc..)
        [SerializeField] private List<float> m_gameplayAudioPitches = new(); // List of frequency/pitch values for each individual gameplay sound.
        [SerializeField] private List<float> m_gameplayAudioVolumes = new(); // List of audio volume values for each individual gameplay sound.
        //---------------------------------------------------------------------------------------------------------------------------------------------------

        // Resetting --
        // ------------
        public void ResetPlayerAudio()
        {
            resettingAudio = true;

            while (resettingAudio == true)
            {
                foreach (StudioEventEmitter emitter in m_engineEmitters)
                {
                    emitter.Stop();
                }

                m_engineAudioInfo = new();
                m_engineEmitterPitches = new();
                m_engineEmitterVolumes = new();

                foreach (StudioEventEmitter emitter in m_gameplaySoundEmitters)
                {
                    emitter.Stop();
                }

                m_gameplayAudioInfo = new();
                m_gameplayAudioPitches = new();
                m_gameplayAudioVolumes = new();

                foreach (StudioEventEmitter emitter in m_windEmitters)
                {
                    emitter.Stop();
                }

                m_windAudioInfo = new List<EventReference>();
                m_windAudioVolumes = new();

                variantSet = false;

                resettingAudio = false;
            }
            return;
        }



        public void UpdateEngineModulations(int shipSelected, int scenario, float breakingValue = 0.0f)
        {
            if (resettingAudio == true)
            {
                return;
            }
            // Scenario = what context the modulations are changing in. [0] = Accelerating, [1] = Slowing down VIA letting go of accelerator, [2] = Slowing down VIA breaking.

            switch (scenario)
            {
                case 0:
                    {
                        switch (shipSelected)
                        {
                            case 0:
                                {
                                    UpdateEnginePitch(0, ((3.5f) * GameManager.gManager.difficultyChange), ((8.30f) * GameManager.gManager.difficultyChange), true, true, false, default, true);           // Engine sound that is first  in the list  (Index [0]), Updating Pitch,  Multiplying the multiplier values, then  getting 64% of the returned value, before finally subtracting it all from the pitch value over time.
                                    UpdateEngineVolume(0, ((0.05f) * GameManager.gManager.difficultyChange), ((0.36f) * GameManager.gManager.difficultyChange), true, true, false, 0.24f, true);                                                                              // Engine sound that is first  in the list  (Index [0]), Updating Volume, Subtracting 0.03f from the volume  value over time, capping the minimum at 0.29f.

                                    UpdateEnginePitch(1, ((8.5f) * GameManager.gManager.difficultyChange), ((33.0f) * GameManager.gManager.difficultyChange), true, true, false, default, true);          // Engine sound that is second in the list  (Index [1]), Updating Pitch,  Multiplying the multiplier values, then  getting 64% of the returned value, before finally subtracting it all from the pitch value over time.
                                    UpdateEngineVolume(1, ((0.05f) * GameManager.gManager.difficultyChange), ((0.3f) * GameManager.gManager.difficultyChange), true, true, false, (float)0.1, true);                                                                              // Engine sound that is Second in the list  (Index [1]), Updating Volume, Subtracting 0.03f from the volume  value over time, capping the minimum at 0.29f.
                                    break;
                                }
                            case 1:
                                {
                                    // Audio Pitch & Volume Modulation
                                    UpdateEnginePitch(0, ((1.1f) * GameManager.gManager.difficultyChange), ((9.5f) * GameManager.gManager.difficultyChange), true, true, false, default, true);                                                                                 // Engine sound that is first  in the list  (Index [0]), Updating Pitch,  adding 0.7f  to the pitch  value over time.
                                    UpdateEngineVolume(0, ((0.015f) * GameManager.gManager.difficultyChange), ((0.36f) * GameManager.gManager.difficultyChange), true, true, false, default, true);                                                                              // Engine sound that is first  in the list  (Index [0]), Updating Volume, adding 0.015f to the volume value over time.

                                    UpdateEnginePitch(1, ((4.5f) * GameManager.gManager.difficultyChange), ((19.2f) * GameManager.gManager.difficultyChange), true, true, false, default, true);                                                                                     // Engine sound that is second in the list  (Index [1]), Updating Pitch,  adding 0.7f  to the pitch  value over time.
                                    UpdateEngineVolume(1, ((0.015f) * GameManager.gManager.difficultyChange), ((0.3f) * GameManager.gManager.difficultyChange), true, true, false, (float)0.1, true);                                                                              // Engine sound that is Second in the list  (Index [1]), Updating Volume, adding 0.015f to the volume value over time.

                                    UpdateEnginePitch(2, ((1.1f) * GameManager.gManager.difficultyChange), ((3.6f) * GameManager.gManager.difficultyChange), true, true, false, default, true);                                                                                     // Engine sound that is third  in the list  (Index [2]), Updating Pitch,  adding 0.7f  to the pitch  value over time.
                                    UpdateEngineVolume(2, ((0.015f) * GameManager.gManager.difficultyChange), ((0.41f) * GameManager.gManager.difficultyChange), true, true, false, default, true);
                                    break;
                                }
                            case 2:
                                {
                                    UpdateEnginePitch(0, ((1.3f) * GameManager.gManager.difficultyChange), ((5.8f) * GameManager.gManager.difficultyChange), true, true, false, default, true);                                                                                 // Engine sound that is first  in the list  (Index [0]), Updating Pitch,  adding 0.7f  to the pitch  value over time.
                                    UpdateEngineVolume(0, ((0.05f) * GameManager.gManager.difficultyChange), ((0.27f) * GameManager.gManager.difficultyChange), true, true, false, default, true);                                                                              // Engine sound that is first  in the list  (Index [0]), Updating Volume, adding 0.015f to the volume value over time.

                                    UpdateEnginePitch(1, ((3.5f) * GameManager.gManager.difficultyChange), ((10.0f) * GameManager.gManager.difficultyChange), true, true, false, default, true);                                                                                     // Engine sound that is second in the list  (Index [1]), Updating Pitch,  adding 0.7f  to the pitch  value over time.
                                    UpdateEngineVolume(1, ((0.2f) * GameManager.gManager.difficultyChange), ((0.49f) * GameManager.gManager.difficultyChange), true, true, false, default, true);                                                                              // Engine sound that is Second in the list  (Index [1]), Updating Volume, adding 0.015f to the volume value over time.

                                    UpdateEnginePitch(2, ((5.5f) * GameManager.gManager.difficultyChange), ((22.2f) * GameManager.gManager.difficultyChange), true, true, false, default, true);                                                                                     // Engine sound that is third  in the list  (Index [2]), Updating Pitch,  adding 0.7f  to the pitch  value over time.
                                    UpdateEngineVolume(2, ((0.01f) * GameManager.gManager.difficultyChange), ((0.03f) * GameManager.gManager.difficultyChange), true, true, false, default, true);

                                    UpdateEnginePitch(3, ((0.55f) * GameManager.gManager.difficultyChange), ((0.67f) * GameManager.gManager.difficultyChange), true, true, false, default, true);                                                                                     // Engine sound that is third  in the list  (Index [2]), Updating Pitch,  adding 0.7f  to the pitch  value over time.
                                    UpdateEngineVolume(3, ((0.55f) * GameManager.gManager.difficultyChange), ((0.77f) * GameManager.gManager.difficultyChange), true, true, false, default, true);
                                    break;
                                }
                        }

                        break;
                    }
                case 1:
                    {
                        switch (shipSelected)
                        {
                            case 0:
                                {
                                    UpdateEnginePitch(0, ((4.5f) * GameManager.gManager.difficultyChange), ((8.30f) * GameManager.gManager.difficultyChange), true, false, true, 0.33f, true);                                                                                    // Engine sound that is first  in the list  (Index [0]), Updating Pitch,  subtracting 0.6f  from the pitch value over  time.
                                    UpdateEngineVolume(0, ((0.05f) * GameManager.gManager.difficultyChange), ((0.36f) * GameManager.gManager.difficultyChange), true, false, true, 0.24f, true);                                                                          // Engine sound that is first  in the list  (Index [0]), Updating Volume, subtracting 0.01f from the volume value over time, capping the minimum at 0.29f.

                                    UpdateEnginePitch(1, ((10.5f) * GameManager.gManager.difficultyChange), ((33.0f) * GameManager.gManager.difficultyChange), true, false, true, 0.33f, true);                                                                                   // Engine sound that is second in the list  (Index [1]), Updating Pitch,  subtracting 0.6f  from the pitch value over  time.
                                    UpdateEngineVolume(1, ((0.05f) * GameManager.gManager.difficultyChange), ((0.3f) * GameManager.gManager.difficultyChange), true, false, true, (float)0.1, true);                                                                          // Engine sound that is Second in the list  (Index [1]), Updating Volume, subtracting 0.01f from the volume value over time, capping the minimum at 0.29f.

                                    break;
                                }
                            case 1:
                                {
                                    // Engine sound that is third  in the list  (Index [2]), Updating Volume, subtracting 0.01f from the volume value over time, capping the minimum at 0.29f.
                                    UpdateEnginePitch(0, ((1.0f) * GameManager.gManager.difficultyChange), ((9.5f) * GameManager.gManager.difficultyChange), true, false, true, 0.33f, true);                                                                                    // Engine sound that is first  in the list  (Index [0]), Updating Pitch,  subtracting 0.6f  from the pitch value over  time.
                                    UpdateEngineVolume(0, ((0.05f) * GameManager.gManager.difficultyChange), ((0.36f) * GameManager.gManager.difficultyChange), true, false, true, 0.24f, true);                                                                          // Engine sound that is first  in the list  (Index [0]), Updating Volume, subtracting 0.01f from the volume value over time, capping the minimum at 0.29f.

                                    UpdateEnginePitch(1, ((2.5f) * GameManager.gManager.difficultyChange), ((19.2f) * GameManager.gManager.difficultyChange), true, false, true, 0.33f, true);                                                                                   // Engine sound that is second in the list  (Index [1]), Updating Pitch,  subtracting 0.6f  from the pitch value over  time.
                                    UpdateEngineVolume(1, ((0.05f) * GameManager.gManager.difficultyChange), ((0.3f) * GameManager.gManager.difficultyChange), true, false, true, (float)0.1, true);                                                                          // Engine sound that is Second in the list  (Index [1]), Updating Volume, subtracting 0.01f from the volume value over time, capping the minimum at 0.29f.

                                    UpdateEnginePitch(2, ((0.25f) * GameManager.gManager.difficultyChange), ((3.6f) * GameManager.gManager.difficultyChange), true, false, true, 0.33f, true);                                                                                    // Engine sound that is third  in the list  (Index [2]), Updating Pitch,  subtracting 0.6f  from the pitch value over  time.
                                    UpdateEngineVolume(2, ((0.05f) * GameManager.gManager.difficultyChange), ((0.41f) * GameManager.gManager.difficultyChange), true, false, true, 0.37f, true);                                                                          // Engine sound that is third  in the list  (Index [2]), Updating Volume, subtracting 0.01f from the volume value over time, capping the minimum at 0.29f.
                                    break;
                                }
                            case 2:
                                {
                                    UpdateEnginePitch(0, ((0.55f) * GameManager.gManager.difficultyChange), ((5.8f) * GameManager.gManager.difficultyChange), true, false, true, default, true);                                                                                 // Engine sound that is first  in the list  (Index [0]), Updating Pitch,  adding 0.7f  to the pitch  value over time.
                                    UpdateEngineVolume(0, ((0.015f) * GameManager.gManager.difficultyChange), ((0.27f) * GameManager.gManager.difficultyChange), true, false, true, 0.24f, true);                                                                              // Engine sound that is first  in the list  (Index [0]), Updating Volume, adding 0.015f to the volume value over time.

                                    UpdateEnginePitch(1, ((2.5f) * GameManager.gManager.difficultyChange), ((10.0f) * GameManager.gManager.difficultyChange), true, false, true, default, true);                                                                                     // Engine sound that is second in the list  (Index [1]), Updating Pitch,  adding 0.7f  to the pitch  value over time.
                                    UpdateEngineVolume(1, ((0.015f) * GameManager.gManager.difficultyChange), ((0.49f) * GameManager.gManager.difficultyChange), true, false, true, default, true);                                                                              // Engine sound that is Second in the list  (Index [1]), Updating Volume, adding 0.015f to the volume value over time.

                                    UpdateEnginePitch(2, ((0.55f) * GameManager.gManager.difficultyChange), ((22.2f) * GameManager.gManager.difficultyChange), true, false, true, default, true);                                                                                     // Engine sound that is third  in the list  (Index [2]), Updating Pitch,  adding 0.7f  to the pitch  value over time.
                                    UpdateEngineVolume(2, ((0.015f) * GameManager.gManager.difficultyChange), ((0.03f) * GameManager.gManager.difficultyChange), true, false, true, 0.03f, true);

                                    UpdateEnginePitch(3, ((0.55f) * GameManager.gManager.difficultyChange), ((0.67f) * GameManager.gManager.difficultyChange), true, false, true, default, true);                                                                                     // Engine sound that is third  in the list  (Index [2]), Updating Pitch,  adding 0.7f  to the pitch  value over time.
                                    UpdateEngineVolume(3, ((0.058f) * GameManager.gManager.difficultyChange), ((0.77f) * GameManager.gManager.difficultyChange), true, false, true, 0.1f, true);
                                    break;
                                }
                        }
                        break;
                    }
                case 2:
                    {
                        switch (shipSelected)
                        {
                            case 0:
                                {
                                    UpdateEnginePitch(0, (breakingValue * GameManager.gManager.difficultyChange), ((8.30f) * GameManager.gManager.difficultyChange), true, false, true, 0.33f, true);                                                                                    // Engine sound that is first  in the list  (Index [0]), Updating Pitch,  subtracting 0.6f  from the pitch value over  time.
                                    UpdateEngineVolume(0, ((0.05f) * GameManager.gManager.difficultyChange), ((0.36f) * GameManager.gManager.difficultyChange), true, false, true, 0.24f, true);                                                                          // Engine sound that is first  in the list  (Index [0]), Updating Volume, subtracting 0.01f from the volume value over time, capping the minimum at 0.29f.

                                    UpdateEnginePitch(1, ((breakingValue * 3.5f) * GameManager.gManager.difficultyChange), ((33.0f) * GameManager.gManager.difficultyChange), true, false, true, 0.33f, true);                                                                                   // Engine sound that is second in the list  (Index [1]), Updating Pitch,  subtracting 0.6f  from the pitch value over  time.
                                    UpdateEngineVolume(1, ((0.05f) * GameManager.gManager.difficultyChange), ((0.3f) * GameManager.gManager.difficultyChange), true, false, true, (float)0.1, true);                                                                          // Engine sound that is Second in the list  (Index [1]), Updating Volume, subtracting 0.01f from the volume value over time, capping the minimum at 0.29f.

                                    break;
                                }
                            case 1:
                                {
                                    UpdateEnginePitch(0, (breakingValue * GameManager.gManager.difficultyChange), ((9.5f) * GameManager.gManager.difficultyChange), true, false, true, 0.33f, true);                                                                                    // Engine sound that is first  in the list  (Index [0]), Updating Pitch,  subtracting 0.6f  from the pitch value over  time.
                                    UpdateEngineVolume(0, ((0.05f) * GameManager.gManager.difficultyChange), ((0.36f) * GameManager.gManager.difficultyChange), true, false, true, 0.24f, true);                                                                          // Engine sound that is first  in the list  (Index [0]), Updating Volume, subtracting 0.01f from the volume value over time, capping the minimum at 0.29f.

                                    UpdateEnginePitch(1, (breakingValue * GameManager.gManager.difficultyChange), ((19.2f) * GameManager.gManager.difficultyChange), true, false, true, 0.33f, true);                                                                                   // Engine sound that is second in the list  (Index [1]), Updating Pitch,  subtracting 0.6f  from the pitch value over  time.
                                    UpdateEngineVolume(1, ((0.05f) * GameManager.gManager.difficultyChange), ((0.3f) * GameManager.gManager.difficultyChange), true, false, true, (float)0.1, true);                                                                          // Engine sound that is Second in the list  (Index [1]), Updating Volume, subtracting 0.01f from the volume value over time, capping the minimum at 0.29f.

                                    UpdateEnginePitch(2, (breakingValue * GameManager.gManager.difficultyChange), ((3.6f) * GameManager.gManager.difficultyChange), true, false, true, 0.33f, true);                                                                                    // Engine sound that is third  in the list  (Index [2]), Updating Pitch,  subtracting 0.6f  from the pitch value over  time.
                                    UpdateEngineVolume(2, ((0.05f) * GameManager.gManager.difficultyChange), ((0.41f) * GameManager.gManager.difficultyChange), true, false, true, 0.37f, true);                                                                          // Engine sound that is third  in the list  (Index [2]), Updating Volume, subtracting 0.01f from the volume value over time, capping the minimum at 0.29f.
                                    break;
                                }
                            case 2:
                                {
                                    UpdateEnginePitch(0, (breakingValue * GameManager.gManager.difficultyChange), ((5.8f) * GameManager.gManager.difficultyChange), true, false, true, default, true);                                                                                    // Engine sound that is first  in the list  (Index [0]), Updating Pitch,  subtracting 0.6f  from the pitch value over  time.
                                    UpdateEngineVolume(0, ((0.05f) * GameManager.gManager.difficultyChange), ((0.27f) * GameManager.gManager.difficultyChange), true, false, true, 0.24f, true);                                                                          // Engine sound that is first  in the list  (Index [0]), Updating Volume, subtracting 0.01f from the volume value over time, capping the minimum at 0.29f.

                                    UpdateEnginePitch(1, (breakingValue * GameManager.gManager.difficultyChange), ((10.0f) * GameManager.gManager.difficultyChange), true, false, true, 0.33f, true);                                                                                   // Engine sound that is second in the list  (Index [1]), Updating Pitch,  subtracting 0.6f  from the pitch value over  time.
                                    UpdateEngineVolume(1, ((0.05f) * GameManager.gManager.difficultyChange), ((0.49f) * GameManager.gManager.difficultyChange), true, false, true, default, true);                                                                          // Engine sound that is Second in the list  (Index [1]), Updating Volume, subtracting 0.01f from the volume value over time, capping the minimum at 0.29f.

                                    UpdateEnginePitch(2, (breakingValue * GameManager.gManager.difficultyChange), ((22.2f) * GameManager.gManager.difficultyChange), true, false, true, 0.33f, true);                                                                                    // Engine sound that is third  in the list  (Index [2]), Updating Pitch,  subtracting 0.6f  from the pitch value over  time.
                                    UpdateEngineVolume(2, ((0.015f) * GameManager.gManager.difficultyChange), ((0.03f) * GameManager.gManager.difficultyChange), true, false, true, 0.03f, true);

                                    UpdateEnginePitch(3, (breakingValue * GameManager.gManager.difficultyChange), ((0.67f) * GameManager.gManager.difficultyChange), true, false, true, 0.33f, true);                                                                                    // Engine sound that is third  in the list  (Index [2]), Updating Pitch,  subtracting 0.6f  from the pitch value over  time.
                                    UpdateEngineVolume(3, ((0.05f) * GameManager.gManager.difficultyChange), ((0.77f) * GameManager.gManager.difficultyChange), true, false, true, 0.3f, true);
                                    break;
                                }
                        }
                        break;
                    }

            }

            if (resettingAudio == true)
            {
                return;
            }

            return;
        }

        public bool IsEmitterPlaying(int listNum, int index)
        {
            StudioEventEmitter EmitterToCheck = new();
            switch (listNum)
            {
                case 0:
                    {
                        if (m_engineEmitters[index] != null)
                        {
                            EmitterToCheck = m_engineEmitters[index];
                        }
                        break;
                    }
                case 1:
                    {
                        if (m_gameplaySoundEmitters[index] != null)
                        {
                            EmitterToCheck = m_gameplaySoundEmitters[index];
                        }
                        break;
                    }
                case 2:
                    {
                        if (m_windEmitters[index] != null)
                        {
                            EmitterToCheck = m_windEmitters[index];
                        }
                        break;
                    }
            }

            if (EmitterToCheck != null)
            {
                if (EmitterToCheck.IsPlaying() == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        public void PlayGPSFX(int soundIndex, int windIndex = 99)
        {
            if (resettingAudio == true)
            {
                return;
            }

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
                case 1:
                    {
                        if (windIndex < 50)
                        {
                            if (windIndex >= 50)
                            {
                                windIndex = 0;
                            }

                            if (m_windAudioInfo[windIndex].IsNull == false && m_windEmitters[windIndex] != null)
                            {

                            }
                        }
                        break;
                    }
            }

            if (resettingAudio == true)
            {
                return;
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

            for (int i = 0; i < m_engineAudioInfo.Count(); i++)
            {
                m_engineEmitters[i].EventReference = m_engineAudioInfo[i];
            }

            m_windEmitters[0].EventReference = m_windAudioInfo[0];
        }

        // Update engine sound pitch. passing in values for the index of the sound you want to update, the amount you want to update it by, the maximum value, whether to add DeltaTime,
        // whether you are adding or subtracting, the minimum value - which has a default of '999.0' which will bypass the minimum, and wheather or not you want to use the default MAX value set in the editor.
        public void UpdateEnginePitch(int index, float amount, float maxValue, bool addDT, bool add, bool subtract, float minimumValue = 999.0f, bool useDefaultMax = false)
        {
            if (resettingAudio == true)
            {
                return;
            }

            if (index > m_engineEmitterPitches.Capacity)
            {
                return;
            }
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
            if (resettingAudio == true)
            {
                return;
            }

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

        public void UpdateWindVolume(int index, float amount, float maxValue, bool add, bool subtract, float minValue = 0.0f)
        {
            if (resettingAudio == true)
            {
                return;
            }

            if (float.IsNaN(m_windAudioVolumes[index]) == true)
            {
                m_windAudioVolumes[index] = 0.03f;
            }
            if (add == true)
            {
                if (m_windAudioVolumes[index] < maxValue)
                {
                    m_windAudioVolumes[index] += amount * Time.deltaTime;
                }
                if (m_windAudioVolumes[index] > maxValue)
                {
                    m_windAudioVolumes[index] = maxValue;
                }
            }
            else if (subtract == true)
            {
                if (minValue < 0.0f)
                {
                    minValue = 0.0f;
                }

                if (m_windAudioVolumes[index] > minValue)
                {
                    m_windAudioVolumes[index] -= amount * Time.deltaTime;
                }
                if (m_windAudioVolumes[index] < minValue)
                {
                    m_windAudioVolumes[index] = minValue;
                }
            }
        }

        void Update()
        {
            if (variantSet == true) // If the variant has been selected
            {
                if (GetComponentInChildren<SparksParticlesController>() != null && GetComponentInChildren<SparksParticlesController>().PAC != this)
                {
                    GetComponentInChildren<SparksParticlesController>().PAC = this;
                }

                if (resettingAudio == false)
                {
                    for (int i = 0; i < m_engineEmitters.Length; i++) // Iterate through the array of engine sound emmitters
                    {
                        if (m_engineEmitters[i] != null) // If the emitter at the current index is not NULL
                        {
                            StudioEventEmitter currentEngineEmitter = m_engineEmitters[i]; // Set the current emmitter to the emitter in the list at the current index.
                            currentEngineEmitter.EventInstance.setPitch(m_engineEmitterPitches[i]); // Update the PITCH of the audio assigned to the current emitter.
                            currentEngineEmitter.EventInstance.setVolume(m_engineEmitterVolumes[i]); // Update the VOLUME of the audio assigned to the current emitter.
                        }
                    }

                    for (int i = 0; i < m_gameplaySoundEmitters.Count; i++) // Iterate through the array of gameplay sound emmitters
                    {
                        if (m_gameplaySoundEmitters[i] != null) // If the emitter at the current index is not NULL
                        {
                            StudioEventEmitter currentGameplayEmitter = m_gameplaySoundEmitters[i]; // Set the current emmitter to the emitter in the list at the current index.
                            currentGameplayEmitter.EventInstance.setVolume(m_gameplayAudioVolumes[i]); // Update the VOLUME of the audio assigned to the current emitter.
                        }
                    }

                    for (int i = 0; i < m_windEmitters.Count; i++) // Iterate through the array of wind sound emmitters
                    {
                        if (m_windEmitters[i] != null) // If the emitter at the current index is not NULL
                        {
                            StudioEventEmitter currentWindEmitter = m_windEmitters[i]; // Set the current emmitter to the emitter in the list at the current index.
                            currentWindEmitter.EventInstance.setVolume(m_windAudioVolumes[i]); // Update the VOLUME of the audio assigned to the current emitter.
                        }
                    }
                }
            }
        }

        // Start playing the engine audio from all of the engine audio emitters.
        public void StartEngineHum()
        {
            if (resettingAudio == true)
            {
                return;
            }

            foreach (StudioEventEmitter emitters in m_engineEmitters)
            {
                emitters.Play();
            }
            foreach (StudioEventEmitter emitters in m_windEmitters)
            {
                emitters.Play();
            }
        }
    }
};

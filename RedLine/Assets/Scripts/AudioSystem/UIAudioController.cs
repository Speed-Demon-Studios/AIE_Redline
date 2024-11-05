using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using System;

namespace EAudioSystem
{
    public class UIAudioController : MonoBehaviour
    {
        [SerializeField] private StudioEventEmitter[] fmodEmitters;
        [SerializeField] private EventReference[] menuAudio;
        [SerializeField] private float[] pitchVariations;
        private float selectedVariation = 0.0f;
        private float pitchSelected = 0.0f;

        // Error Checking
        private List<string> errorMessages = new List<string>();

        private bool[] errorBools = new bool[6];
        private bool checkingArrays = false;
        private bool detectedERROR = false;

        public void GamePauseSound()
        {
            fmodEmitters[0].Play();
            StudioEventEmitter testEmitter = new StudioEventEmitter();
        }
    
        public void PlayerJoinSound()
        {
            fmodEmitters[1].Play();
        }
    
        public void MenuConfirmSound()
        {
            pitchSelected = UnityEngine.Random.Range(0.0f, 25.0f);
    
            switch (pitchSelected)
            {
                case <= 5:
                    {
                        selectedVariation = pitchVariations[0];
                        break;
                    }
                case float v when (v > 5 && v <= 10):
                    {
                        selectedVariation = pitchVariations[1];
                        break;
                    }
                case float v when (v > 10 && v <= 15):
                    {
                        selectedVariation = pitchVariations[2];
                        break;
                    }
                case float v when (v > 15 && v <= 20):
                    {
                        selectedVariation = pitchVariations[3];
                        break;
                    }
                case float v when (v > 20 && v <= 25):
                    {
                        selectedVariation = pitchVariations[4];
                        break;
                    }
            }
            fmodEmitters[2].EventInstance.setPitch(selectedVariation);
            fmodEmitters[2].Play();
        }

        // ERROR CHECKING
        private void CheckArrays()
        {
            bool checkingArrays = false;
            bool fmeERROR = false;
            bool maERROR = false;
            bool pvsERROR = false;
            bool FmeMaSizeMismatchERROR = false;
            errorBools[0] = checkingArrays;
            errorBools[1] = fmeERROR;
            errorBools[2] = maERROR;
            errorBools[3] = pvsERROR;
            errorBools[4] = FmeMaSizeMismatchERROR;

            // fmodEmitters array
            for (int i = 0; i < fmodEmitters.Length; i++)
            {
                if (fmodEmitters[i] == null)
                {
                    Debug.LogException(new Exception("|UIAudioController.cs|: Item at index [" + i + "] in array 'fmodEmitters' is NULL!"));
                    fmeERROR = true;
                }
                else
                    Debug.Log("|UIAudioController.cs|: Item at index [" + i + "] in array 'fmodEmitters' ::: OK!");
            }

            // menuAudio array
            for (int i = 0; i < menuAudio.Length; i++)
            {
                if (menuAudio[i].IsNull == true)
                {
                    Debug.LogException(new Exception("|UIAudioController.cs|: Item at index [" + i + "] in array 'menuAudio' is NULL!"));
                    maERROR = true;
                }
                else
                    Debug.Log("|UIAudioController.cs|: Item at index [" + i + "] in array 'menuAudio' ::: OK!");
            }

            for (int i = 0; i < pitchVariations.Length; i++)
            {
                if (pitchVariations[i] == 0)
                {
                    Debug.LogException(new Exception("|UIAudioController.cs|: Item at index [" + i + "] in array 'pitchVariations' is ZERO!"));
                    pvsERROR = true;
                }
                else
                    Debug.Log("|UIAudioController.cs|: Item at index [" + i + "] in array 'pitchVariations' ::: OK!");
            }

            if (menuAudio.Length != fmodEmitters.Length)
            {
                Debug.LogException(new Exception("|UIAudioController.cs|: Size Mismatch(Array 'menuAudio' & Array 'fmodEmitters')"));
                FmeMaSizeMismatchERROR = true;
            }
            else
                Debug.Log("|UIAudioController.cs|: Array 'menuAudio' & Array 'fmodEmitters' Size Check Status ::: OK!");

            foreach (bool errorCheck in errorBools)
            {
                if (errorCheck == true)
                {
                    detectedERROR = true;
                }
            }
        }

        // ------------------


        private void Start()
        {
            CheckArrays();
            if (detectedERROR == false)
            {
                for (int i = 0; i < fmodEmitters.Length; i++)
                {
                    fmodEmitters[i].EventReference = menuAudio[i];
                }
            }
        }
    
        private void Update()
        {
            if (detectedERROR == false)
            {
                fmodEmitters[2].EventInstance.setPitch(selectedVariation);
            }
        }
    }
}

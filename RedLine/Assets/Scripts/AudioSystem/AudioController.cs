using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EAudioSystem
{
    public class AudioController : MonoBehaviour
    {
        public float paramValue = 0.0f;

        public StudioEventEmitter a;
        public StudioGlobalParameterTrigger sgpt;
        public GameObject musicParentGO;

        public void SetParamValue(string paramName, float value)
        {
            sgpt.Value = 0.0f;
            float currentParamValue = 0.0f;
            a.EventInstance.getParameterByName(paramName, out currentParamValue);

            if (currentParamValue == value)
            {
                return;
            }

            float oldParamValue = currentParamValue;

            Debug.Log("Audio Parameter '" + paramName + "' Current Value: " + currentParamValue);

            a.EventInstance.setParameterByName(paramName, value);
            a.EventInstance.getParameterByName(paramName, out currentParamValue);

            if (currentParamValue == value)
            {
                Debug.Log("Audio Parameter '" + paramName + "' Value successfully changed from (" + oldParamValue + ") to (" + currentParamValue + ")");
            }
            else
            {
                Debug.Log("Audio Parameter '" + paramName + "' value failed to update");
            }
        }

        private void Update()
        {
            if (GameManager.gManager.musicEmitter != null && GameManager.gManager.musicEmitter.IsPlaying() == false)
            {
                GameManager.gManager.musicEmitter.Play();
            }

            if (GameManager.gManager.m_musicVolume > 5.0f)
            {
                GameManager.gManager.m_musicVolume = 5.0f;
            }

            if (GameManager.gManager.m_musicVolume < 0.0f)
            {
                GameManager.gManager.m_musicVolume = 0.0f;
            }

            if (GameManager.gManager.aC.sgpt.Value != GameManager.gManager.m_musicVolume)
            {
                musicParentGO.SetActive(true);
                GameManager.gManager.aC.sgpt.Value = GameManager.gManager.m_musicVolume;
                musicParentGO.SetActive(false);
                musicParentGO.SetActive(true);
            }
        }
    }
}
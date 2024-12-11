using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace EAudioSystem
{
    public class AudioController : MonoBehaviour
    {
        public float paramValue = 0.0f;

        public StudioEventEmitter a;
        public StudioGlobalParameterTrigger sgpt;
        public StudioGlobalParameterTrigger musicTrigger;
        public StudioGlobalParameterTrigger musicRaceFinishedTrigger;
        public StudioGlobalParameterTrigger musicLowpassValue;
        public GameObject musicParentGO;
        public GameObject musicTriggerGO;
        public GameObject musicRaceFinishedTriggerGO;
        public GameObject musicLowpassValueGO;
        public bool newValueChanged = false;
        public float newValue = 0.0f;

        public void SetParamValue(string paramName, float value)
        {
            GameManager.gManager.aC.musicTrigger.Value = value;
            GameManager.gManager.aC.musicTriggerGO.SetActive(false);
            GameManager.gManager.aC.musicTriggerGO.SetActive(true);
            GameManager.gManager.aC.musicTriggerGO.SetActive(false);
        }

        private void Update()
        {
            if (GameManager.gManager.musicEmitter != null && GameManager.gManager.musicEmitter.IsPlaying() == false)
            {
                GameManager.gManager.musicEmitter.Play();
            }

            //if (GameManager.gManager.m_musicVolume > 5.0f)
            //{
            //    GameManager.gManager.m_musicVolume = 5.0f;
            //}
            //
            //if (GameManager.gManager.m_musicVolume < 0.0f)
            //{
            //    GameManager.gManager.m_musicVolume = 0.0f;
            //}
            //
            //if (GameManager.gManager.aC.newValueChanged == true)
            //{
            //
            //}
        }
    }
}
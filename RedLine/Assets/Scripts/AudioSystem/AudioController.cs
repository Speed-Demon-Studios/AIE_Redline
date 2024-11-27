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
        public FMOD.Studio.Bank[] banksTest;
        public StudioBankLoader bankLoaderTest;
        bool displayedBankInfo = false;
        EventDescription[] testDesc;

        private void Update()
        {
            if (displayedBankInfo == false)
            {
                displayedBankInfo = true;
                Debug.Log(bankLoaderTest.Banks[0]);
                RuntimeManager.StudioSystem.getBankList(out banksTest);
                
                for (int i = 0; i < banksTest.Count(); i++)
                {
                    string path;
                    banksTest[i].getPath(out path);
                    if (RuntimeManager.HasBankLoaded(bankLoaderTest.Banks[0]))
                    {
                        Debug.Log(path + " LOADED SUCCESSFULLY");
                        int a;
                        banksTest[i].getEventCount(out a);

                        banksTest[i].getEventList(out testDesc);

                        string eventPath;
                        Debug.Log("Event '" + a + "' in BANK '" + path);
                    }
                    else
                    {
                        Debug.Log(path + " HAS NOT LOADED");
                    }

                }

                foreach (EventDescription desc in testDesc)
                {
                    string ePath = "";
                    desc.getPath(out ePath);
                    Debug.Log(ePath);
                }
            }
        }

        public void LoadEngineBank(int engineBank)
        {
            switch (engineBank)
            {
                case 1:
                    {
                        Bank[] banks;
                        RuntimeManager.StudioSystem.getBankList(out banks);

                        RuntimeManager.UnloadBank("Master");
                        RuntimeManager.LoadBank("FulcrumEngine");
                        break;
                    }
            }
        }

    }
}


// OLD UNUSED -- DELETE LATER
//[SerializeField] private AudioMixer redlineMixer;
//[SerializeField] private AudioMixerGroup SFXGROUP;
//[SerializeField] private AudioMixerGroup BGMGROUP;
//
//[SerializeField] private List<AudioClip> SFXClips = new List<AudioClip>();
//[SerializeField] private List<AudioClip> BGMClips = new List<AudioClip>();
//
//public IEnumerator SetAndPlayNewClip(AudioSource audioSource, int audioIndex = 0, int audioType = 0)
//{
//    audioSource.Stop();
//    AudioClip newClip = null;
//
//    if (audioType > 0)
//    {
//        if (audioType == 1)
//        {
//            audioSource.outputAudioMixerGroup = SFXGROUP;
//            newClip = SFXClips[audioIndex];
//        }
//        if (audioType == 2)
//        {
//            audioSource.outputAudioMixerGroup = BGMGROUP;
//            newClip = BGMClips[audioIndex];
//        }
//
//        newClip.LoadAudioData();
//        audioSource.clip = newClip;
//        audioSource.Play();
//        yield return new WaitForEndOfFrame();
//    }
//    StopCoroutine(SetAndPlayNewClip(audioSource, 0, 0));
//}

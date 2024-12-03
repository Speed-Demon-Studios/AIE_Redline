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

        public double m_masterVolume = 1.0f;
        public double m_gameplayVolume = 1.0f;
        public double m_musicVolume = 1.0f;

        private void Update()
        {
            //if (displayedBankInfo == false)
            //{
            //    displayedBankInfo = true;
            //    Debug.Log(bankLoaderTest.Banks[0]);
            //    RuntimeManager.StudioSystem.getBankList(out banksTest);
            //    
            //    for (int i = 0; i < banksTest.Count(); i++)
            //    {
            //        string path;
            //        banksTest[i].getPath(out path);
            //        if (RuntimeManager.HasBankLoaded(bankLoaderTest.Banks[0]))
            //        {
            //            Debug.Log(path + " LOADED SUCCESSFULLY");
            //            int a;
            //            banksTest[i].getEventCount(out a);
            //
            //            banksTest[i].getEventList(out testDesc);
            //
            //            string eventPath;
            //            Debug.Log("Event '" + a + "' in BANK '" + path);
            //        }
            //        else
            //        {
            //            Debug.Log(path + " HAS NOT LOADED");
            //        }
            //
            //    }
            //
            //    foreach (EventDescription desc in testDesc)
            //    {
            //        string ePath = "";
            //        desc.getPath(out ePath);
            //        Debug.Log(ePath);
            //    }
            //}
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
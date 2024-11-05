using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using EAudioSystem;

public class VariantAudioContainer : MonoBehaviour
{
    public List<EventReference> FulcrumEngineAudio = new List<EventReference>();
    public List<EventReference> SplitwingEngineAudio = new List<EventReference>();
    public List<EventReference> CutlassEngineAudio = new List<EventReference>();

    public void CheckVariant(int variantINDEX)
    {
        PlayerAudioController PAC = this.gameObject.GetComponent<PlayerAudioController>();

        switch (variantINDEX)
        {
            case 0:
                {
                    PAC.SetEngineAudios(SplitwingEngineAudio);
                    break;
                }
            case 1:
                {
                    PAC.SetEngineAudios(FulcrumEngineAudio);
                    break;
                }
            case 2:
                {
                    PAC.SetEngineAudios(CutlassEngineAudio);
                    break;
                }
        }

        PAC.variantSet = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

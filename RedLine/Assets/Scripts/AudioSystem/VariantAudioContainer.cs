using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using EAudioSystem;
using UnityEditor.ShaderGraph.Internal;

public class VariantAudioContainer : MonoBehaviour
{
    public List<EventReference> FulcrumEngineAudio = new List<EventReference>();
    public List<EventReference> SplitwingEngineAudio = new List<EventReference>();
    public List<EventReference> CutlassEngineAudio = new List<EventReference>();
    public bool isTestFulcrum = false;
    public bool isTestCutlass = false;
    public bool isTestSplitwing = false;

    [SerializeField] private float[] m_FulcrumMaxEnginePitchValues;
    [SerializeField] private float[] m_FulcrumMaxEngineVolumeValues;

    [SerializeField] private float[] m_SplitwingMaxEnginePitchValues;
    [SerializeField] private float[] m_SplitwingMaxEngineVolumeValues;

    [SerializeField] private float[] m_CutlassMaxEnginePitchValues;
    [SerializeField] private float[] m_CutlassMaxEngineVolumeValues;

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
                    PAC.SetDefaultModulations(m_FulcrumMaxEnginePitchValues, m_FulcrumMaxEngineVolumeValues);
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

    public void SetTestFulcrum()
    {
        PlayerAudioController PAC = this.gameObject.GetComponent<PlayerAudioController>();

        PAC.SetEngineAudios(FulcrumEngineAudio);
        PAC.SetDefaultModulations(m_FulcrumMaxEnginePitchValues, m_FulcrumMaxEngineVolumeValues);
        PAC.variantSet = true;
    }

    private void Start()
    {
        if (isTestFulcrum == true)
        {
            SetTestFulcrum();
        }
    }
}

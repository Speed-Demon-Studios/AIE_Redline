using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using EAudioSystem;
using UnityEditor.ShaderGraph.Internal;
using System.Linq;

public class VariantAudioContainer : MonoBehaviour
{
    public List<EventReference> FulcrumEngineAudio = new List<EventReference>();
    public List<EventReference> SplitwingEngineAudio = new List<EventReference>();
    public List<EventReference> CutlassEngineAudio = new List<EventReference>();

    public bool isTestFulcrum = false;
    public bool isTestCutlass = false;
    public bool isTestSplitwing = false;

    [SerializeField] private List<float> m_FulcrumMaxEnginePitchValues = new List<float>();
    [SerializeField] private List<float> m_FulcrumMaxEngineVolumeValues = new List<float>();

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
                    for (int i = 0; i < m_SplitwingMaxEnginePitchValues.Count(); i++)
                    {
                        PAC.SetDefaultEngineModulations(m_SplitwingMaxEnginePitchValues[i], m_SplitwingMaxEngineVolumeValues[i]);
                    }
                    break;
                }
            case 1:
                {
                    PAC.SetEngineAudios(FulcrumEngineAudio);
                    for (int i = 0; i < m_FulcrumMaxEnginePitchValues.Count(); i++)
                    {
                        PAC.SetDefaultEngineModulations(m_FulcrumMaxEnginePitchValues[i], m_FulcrumMaxEngineVolumeValues[i]);
                    }
                    break;
                }
            case 2:
                {
                    PAC.SetEngineAudios(CutlassEngineAudio);
                    for (int i = 0; i < m_CutlassMaxEnginePitchValues.Count(); i++)
                    {
                        PAC.SetDefaultEngineModulations(m_CutlassMaxEnginePitchValues[i], m_CutlassMaxEngineVolumeValues[i]);
                    }
                    break;
                }
        }

        PAC.variantSet = true;
    }

    public void SetTestFulcrum()
    {
        PlayerAudioController PAC = this.gameObject.GetComponent<PlayerAudioController>();
    
        PAC.SetEngineAudios(FulcrumEngineAudio);
        PAC.SetDefaultEngineModulations(m_FulcrumMaxEnginePitchValues[0], m_FulcrumMaxEngineVolumeValues[0]);
        PAC.SetDefaultEngineModulations(m_FulcrumMaxEnginePitchValues[1], m_FulcrumMaxEngineVolumeValues[1]);
        PAC.SetDefaultEngineModulations(m_FulcrumMaxEnginePitchValues[2], m_FulcrumMaxEngineVolumeValues[2]);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsVolumeControl : MonoBehaviour
{
    [SerializeField] private Slider m_masterVolumeSlider;


    // Start is called before the first frame update
    void Start()
    {
        m_masterVolumeSlider.onValueChanged.AddListener(delegate { GameManager.gManager.aC.SetMasterVolume(m_masterVolumeSlider.value); });
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RacerEntry : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_positionDisplay;
    public string position { get => m_positionDisplay.text; set => m_positionDisplay.text = value; }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

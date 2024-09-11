using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBlendAnimations : MonoBehaviour
{
    private ShipsControls m_controls;
    private Animator m_controller;

    // Start is called before the first frame update
    void OnEnable()
    {
        m_controls = GetComponent<ShipsControls>();
        FindEveryChild(m_controls.shipModel.transform);
    }

    public void FindEveryChild(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Animator outPut;
            if (child.TryGetComponent<Animator>(out outPut))
            {
                m_controller = outPut;
                return;
            }
            else if(child.childCount > 0)
                FindEveryChild(child);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.enabled)
        {
            m_controller.SetFloat("EngineBlend", m_controls.ReturnRB().velocity.magnitude / m_controls.GetMaxSpeed());

            m_controller.SetFloat("WingBlend", m_controls.GetTurnMultiplier());
        }
    }
}

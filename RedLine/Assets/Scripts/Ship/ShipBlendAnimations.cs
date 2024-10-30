using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public enum ShipType
{
    Splitwing,
    Cutlass,
    Fulcrum
}
public class ShipBlendAnimations : MonoBehaviour
{
    private ShipsControls m_controls;
    private Animator m_controller;
    private ShipType shipType;

    // Start is called before the first frame update
    void OnEnable()
    {
        m_controls = GetComponent<ShipsControls>();
        FindEveryChild(m_controls.shipModel.transform);
        shipType = GetComponent<ShipsControls>().VariantObject.shipType;
    }

    /// <summary>
    /// Finds every child in the parent and if the parent has children then go through all of them
    /// </summary>
    /// <param name="parent"></param>
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
        //---------------------------------------------------------------------------------------------------------------------------------------|
        if (this.enabled && m_controller != null) // check for null refreneces so there are no errors                                            |
        {                                                                                                                                      //|
            m_controller.SetFloat("Engine", m_controls.ReturnRB().velocity.magnitude / m_controls.GetCurrentMaxSpeed()); // blend engine to speed|
                                                                                                                                               //|
            m_controller.SetFloat("Flaps", m_controls.GetTurnMultiplier()); // blend wing to the direction of turning                            |

            if (shipType == ShipType.Cutlass)
            {
                if (m_controls.ReturnRB().velocity.magnitude > 0.5f && m_controls.GetBrakeMultiplier() == 0)
                {
                    m_controller.SetFloat("WingRight", m_controls.GetAccelerationMultiplier());
                    m_controller.SetFloat("WingLeft", m_controls.GetAccelerationMultiplier());
                }
                else if (m_controls.GetBrakeMultiplier() != 0) // if the ship is braking                     
                {
                    m_controller.SetFloat("WingRight", -m_controls.GetBrakeMultiplier());
                    m_controller.SetFloat("WingLeft", -m_controls.GetBrakeMultiplier());
                }
                else // if the ship is at a stop and not doing anything                                                                          |
                {                                                                                                                              //|
                    m_controller.SetFloat("WingRight", 0);                                                                                     //|
                    m_controller.SetFloat("WingLeft", 0);                                                                                      //|
                }
            }
            else if(shipType == ShipType.Splitwing)
            {
                m_controller.SetFloat("WingRight", m_controls.GetTurnMultiplier());
            }
            else if(shipType == ShipType.Fulcrum)
            {

            }
                                                                                                                                               //|
        }                                                                                                                                      //|
        //---------------------------------------------------------------------------------------------------------------------------------------|
    }
}

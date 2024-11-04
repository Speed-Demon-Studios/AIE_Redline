using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedlineChecker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ShipsControls tryTest = this.GetComponentInParent<ShipsControls>();
        if (other.CompareTag("Redline") && other.gameObject != this.gameObject)
        {
            tryTest.SwitchRedlineBool();
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        ShipsControls tryTest = this.GetComponentInParent<ShipsControls>();
        if (other.CompareTag("Redline") && other.gameObject != this.gameObject)
        {
            tryTest.SwitchRedlineBool();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ShipsControls tryTest = this.GetComponentInParent<ShipsControls>(); ;
        if (other.CompareTag("Redline") && other.gameObject != this.gameObject)
        {
            tryTest.DelayRedlineFalse();
        }
    }
}

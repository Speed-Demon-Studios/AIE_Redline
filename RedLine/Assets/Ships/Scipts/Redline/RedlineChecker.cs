using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedlineChecker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ShipsControls tryTest = this.GetComponentInParent<ShipsControls>();
        if (other.CompareTag("Redline"))
        {
            tryTest.SwitchRedlineBool(true);
            tryTest.AddToBoost();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        ShipsControls tryTest = this.GetComponentInParent<ShipsControls>(); ;
        if (other.CompareTag("Redline"))
        {
            tryTest.AddToBoost();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ShipsControls tryTest = this.GetComponentInParent<ShipsControls>(); ;
        if (other.CompareTag("Redline"))
        {
            tryTest.SwitchRedlineBool(false);
        }
    }
}

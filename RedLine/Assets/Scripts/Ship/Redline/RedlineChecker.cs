using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedlineChecker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ShipsControls tryTest = this.GetComponentInParent<ShipsControls>();
        if (other.CompareTag("Redline") && !transform.parent.GetComponent<RacerDetails>().rCS.ReturnList().Contains(other.gameObject))
        {
            tryTest.SwitchRedlineBool(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ShipsControls tryTest = this.GetComponentInParent<ShipsControls>(); ;
        if (other.CompareTag("Redline") && !transform.parent.GetComponent<RacerDetails>().rCS.ReturnList().Contains(other.gameObject))
        {
            tryTest.SwitchRedlineBool(false);
        }
    }
}

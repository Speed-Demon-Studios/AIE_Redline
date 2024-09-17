using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetNormal : MonoBehaviour
{
    public Transform point;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Racer"))
            other.transform.parent.gameObject.GetComponentInParent<ShipsControls>().SetRotationToTrack(point);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Racer"))
            other.transform.parent.gameObject.GetComponentInParent<ShipsControls>().SetRotationToTrack(point);
    }
}

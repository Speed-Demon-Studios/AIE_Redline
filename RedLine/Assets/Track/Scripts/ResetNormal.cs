using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetNormal : MonoBehaviour
{
    public Transform point;
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponentInParent<ShipsControls>().SetRotation(point);
    }

    private void OnTriggerStay(Collider other)
    {
        other.GetComponentInParent<ShipsControls>().SetRotation(point);
    }
}

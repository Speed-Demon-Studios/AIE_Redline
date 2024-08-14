using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTrigger : MonoBehaviour
{
    public bool shipInTrigger;

    public List<GameObject> shipsInTrigger = new();

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.GetComponent<ShipsControls>())
        {
            if (!shipsInTrigger.Contains(other.transform.parent.gameObject))
                shipsInTrigger.Add(other.transform.parent.gameObject);

            shipInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.parent.GetComponent<ShipsControls>())
        {
            shipsInTrigger.Remove(other.transform.parent.gameObject);
            shipInTrigger = false;
        }
    }
}

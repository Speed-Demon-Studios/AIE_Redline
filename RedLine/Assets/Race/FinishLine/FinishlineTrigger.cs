using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishlineTrigger : MonoBehaviour
{
    [SerializeField] private RaceManager rM;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.ToLower() == "racer")
        {
            RacerDetails rDeets = other.gameObject.GetComponent<RacerDetails>();
            rM.LapComplete(rDeets);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

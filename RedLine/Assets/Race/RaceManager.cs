using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    [SerializeField] private int TotalLaps;
    
    public void LapComplete(RacerDetails racer)
    {
        if (racer.currentLap < TotalLaps)
        {
            racer.currentLap += 1;
        }
        

        if (racer.currentLap > 0)
        {
            Debug.Log("Lap " + (racer.currentLap - 1) + " Completed!");
        }
        return;
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

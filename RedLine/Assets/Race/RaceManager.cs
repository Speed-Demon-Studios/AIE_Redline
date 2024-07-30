using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    [SerializeField] private int TotalLaps;
    
    public void LapComplete(RacerDetails racer)
    {
        int _currentLap = racer.GetCurrentLap();


        if (_currentLap < TotalLaps)
        {
            _currentLap += 1;
        }
        

        if (_currentLap > 0)
        {
            Debug.Log("Lap " + (_currentLap - 1) + " Completed!");
        }

        racer.SetCurrentLap(_currentLap);
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

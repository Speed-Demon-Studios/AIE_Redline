using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacerDetails : MonoBehaviour
{
    private int currentLap = 0;
    

    public int GetCurrentLap()
    {
        return currentLap;
    }

    public void SetCurrentLap(int value)
    {
        currentLap = value;
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

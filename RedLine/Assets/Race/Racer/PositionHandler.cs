using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionHandler : MonoBehaviour
{
    public List<RacerDetails> racers = new List<RacerDetails>();
    public bool racersAdded = false;
    private IList<RacerDetails> racerFinder = new List<RacerDetails>();


    private void Awake()
    {
        // Find all the racers in the scene
        racerFinder = FindObjectsOfType<RacerDetails>();

        foreach (RacerDetails rD in racerFinder)
        {
            Debug.Log("FOUND RACER IN FINDER");
            racers.Add(rD);
        }

        foreach (RacerDetails rD in racers)
        {
            Debug.Log("FOUND RACERS");
        }
        if (racers.Count == racerFinder.Count)
        {
            racersAdded = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(racersAdded);
        if (racersAdded == true)
        {
            racers.Sort((r1, r2) =>
            {
                if (r1.currentCheckpoint != r2.currentCheckpoint)
                {
                    return r1.currentLap.CompareTo(r2.currentLap);
                }

                if (r1.currentCheckpoint != r2.currentCheckpoint)
                {
                    return r1.currentCheckpoint.CompareTo(r2.currentCheckpoint);
                }

                return r1.NextCheckpointDistance().CompareTo(r2.NextCheckpointDistance());
            });

            foreach (RacerDetails rD in racers)
            {
                rD.placement = racers.IndexOf(rD);
            }

            for (int i = 0; i < racers.Count; i++)
            {
                racers[i].placement = i + 1;
            }
        }
    }
}

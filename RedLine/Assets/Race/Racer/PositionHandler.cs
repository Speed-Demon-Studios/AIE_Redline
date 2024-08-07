using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionHandler : MonoBehaviour
{
    public bool racersAdded = false;
    public List<RacerDetails> racers = new List<RacerDetails>();
    private IList<RacerDetails> racerFinder = new List<RacerDetails>();
    private bool racersSorted = false;

    public void OnRaceLoaded()
    {
        // Find all the racers in the scene
        racersAdded = false;
        racerFinder = FindObjectsOfType<RacerDetails>();
        //racerFinder = new List<RacerDetails>();
        //racerFinder.Clear();
        //racers.Clear();

        foreach (RacerDetails rD in racerFinder)
        {
            Debug.Log("FOUND RACER IN FINDER");
            racers.Add(rD);
        }


        foreach (RacerDetails rD in racers)
        {
            Debug.Log("FOUND RACERS");
        }

        foreach (RacerDetails rD in racers)
        {
            GameManager.gManager.racerObjects.Add(rD.gameObject);
        }

        if (racers.Count == racerFinder.Count)
        {
            racersAdded = true;
        }
    }

    public IEnumerator SortRacers()
    {
        foreach (RacerDetails rD in racers)
        {
            rD.placement = racers.IndexOf(rD) + 1;
        }
        racersSorted = false;
        StopCoroutine(SortRacers());
        yield return new WaitForEndOfFrame();
    }

    // Update is called once per frame
    void Update()
    {
        if (racersAdded == false)
        {
            OnRaceLoaded();
        }
        else if (racersAdded == true)
        {
             racers.Sort((r1, r2) =>
             {
                 racersSorted = false;
                 //if (r1.currentLap != r2.currentCheckpoint)
                 //{
                 //    return r1.currentCheckpoint.CompareTo(r2.currentLap);
                 //}
                 //if (r1.currentCheckpoint != r2.currentCheckpoint)
                 //{
                 //    return r1.currentLap.CompareTo(r2.currentCheckpoint);
                 //}
                 //
                 //if (r1.currentCheckpoint != r2.currentCheckpoint)
                 //{
                 //    return r1.currentLap.CompareTo(r2.currentLap);
                 //}
                 //
                 //if (r1.currentLap != r2.currentLap)
                 //{
                 //    return r1.currentCheckpoint.CompareTo(r2.currentCheckpoint);
                 //}
                 //
                 //if (r1.currentLap != r2.currentLap)
                 //{
                 //    return r1.currentCheckpoint.CompareTo(r2.currentLap);
                 //}
                 if (r1.currentCheckpoint != r2.currentCheckpoint)
                 {
                     return r1.currentCheckpoint;
                     //return r1.currentCheckpoint.CompareTo(r2.currentLap);
                 }

                 if (r1.currentLap != r2.currentLap)
                 {
                     return r1.currentCheckpoint;
                 }

                 racersSorted = true;
                 return r1.NextCheckpointDistance().CompareTo(r2.NextCheckpointDistance());
             });
             
             StartCoroutine(SortRacers());
        }
    }
}

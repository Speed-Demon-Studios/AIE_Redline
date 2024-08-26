using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionHandler : MonoBehaviour
{
    public bool racersAdded = false;
    public List<RacerDetails> racers = new List<RacerDetails>();
    public IList<RacerDetails> racerFinder = new List<RacerDetails>();
    private bool racersSorted = false;

    public void OnRaceLoaded()
    {
        // Find all the racers in the scene
        racersAdded = false;
        racerFinder = FindObjectsOfType<RacerDetails>();
        //racerFinder = new List<RacerDetails>();
        //racerFinder = new List<RacerDetails>();
        //racers = new List<RacerDetails>();

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
        if (GameManager.gManager.racerObjects.Count == racers.Count)
        {
            GameManager.gManager.racersAdded = true;
        }
    }

    public IEnumerator SortRacers()
    {
        //racersSorted = false;
        foreach (RacerDetails rD in racers)
        {
            rD.placement = racers.IndexOf(rD) + 1;
        }
        //racersSorted = true;
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
             GameManager.gManager.indexListSorted = true;
             racers.Sort((r1, r2) =>
             {
                 racersSorted = false;
                 if (r1.currentCheckpoint != r2.currentCheckpoint)
                 {
                     return r1.currentCheckpoint;
                 }

                 if (r1.currentLap != r2.currentLap)
                 {
                     return r1.currentCheckpoint;
                 }

                 if (r1.finishedRacing)
                 {
                     return r1.currentCheckpoint;
                 }

                 if (r2.finishedRacing)
                 {
                     return r2.currentCheckpoint;
                 }

                 racersSorted = true;
                 return r1.NextCheckpointDistance().CompareTo(r2.NextCheckpointDistance());
             });
             GameManager.gManager.indexListSorted = true;
             
             StartCoroutine(SortRacers());
        }
    }
}

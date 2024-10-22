using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PositionHandler : MonoBehaviour
{
    public bool racersAdded = false;
    public List<RacerDetails> racers = new List<RacerDetails>();
    public IList<RacerDetails> racerFinder = new List<RacerDetails>();
    private bool racersSorted = false;

    public List<GameObject> aiRacePrefabs = new();
    public Nodes startNode;

    private void Awake()
    {
        GameManager.gManager.pHandler = this;
        racerFinder = new List<RacerDetails>();
        racers = new List<RacerDetails>();
        racersAdded = false;
        racersSorted = false;
    }

    public void OnRaceLoaded()
    {
        //for (int i = 0; i < 9; i++)
        //{
        //    int index = Random.Range(0, aiRacePrefabs.Count - 1);
        //
        //    GameObject a = Instantiate(aiRacePrefabs[index]);
        //
        //    a.GetComponent<AIMoveInputs>().desiredNode = startNode;
        //
        //    GameManager.gManager.racerObjects.Add(a);
        //}

        //foreach (RacerDetails rD in racers)
        //{
        //    GameManager.gManager.racerObjects.Add(rD.gameObject);
        //}

        //if (GameManager.gManager.racerObjects.Count == GameManager.gManager.playerObjects.Count + 9)
        //{
        //    Debug.Log("Racers Added");
        //    racersAdded = true;
        //    GameManager.gManager.racersAdded = true;
        //}

        racersAdded = true;
        GameManager.gManager.racersAdded = true;
    }

    public IEnumerator SortRacers()
    {
        foreach (RacerDetails rD in racers)
        {
            rD.placement = racers.IndexOf(rD) + 1;
        }
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
            GameManager.gManager.indexListSorted = false;
            if(GameManager.gManager.raceFinished == false)
            {
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
}

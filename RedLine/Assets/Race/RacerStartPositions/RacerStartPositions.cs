using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RacerStartPositions : MonoBehaviour
{
    [SerializeField] private GameObject[] startPositions;
    private bool racersPlaced = false;
    private int placementIndexer = 0;

    // Update is called once per frame
    void Update()
    {
        // If the race has started and the racers have been added to the list of racers
        if (GameManager.gManager.raceStarted == false && GameManager.gManager.racersAdded == true)
        {
            if (racersPlaced == false)
            {
                // Iterate through the list of racer objects, and the list of start positions, and assign racers to their respective starting positions.
                for (int i = 0; i < GameManager.gManager.racerObjects.Count; i++)
                {
                    for (int a = 0; a < startPositions.Count(); a++)
                    {
                        StartingPositionDetails thisPosition = startPositions[a].GetComponent<StartingPositionDetails>();
                        if (thisPosition.SpotFilled == false)
                        {
                            thisPosition.HeldRacer = GameManager.gManager.racerObjects[i];
                            GameManager.gManager.racerObjects[i].transform.position = startPositions[a].transform.position;
                            thisPosition.SpotFilled = true;
                            a = startPositions.Count();
                            break;
                        }
                    }
                }
            }
        }
    }
}

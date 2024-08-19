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
        if (GameManager.gManager.raceStarted == false && GameManager.gManager.racersAdded == true)
        {
            if (racersPlaced == false)
            {
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

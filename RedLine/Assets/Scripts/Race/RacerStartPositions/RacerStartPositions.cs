using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using EAudioSystem;

public class RacerStartPositions : MonoBehaviour
{
    [SerializeField] private GameObject[] startPositions;
    private bool placingRacers = false;
    private int placementIndexer = 0;


    private void Awake()
    {
        GameManager.gManager.racersPlaced = false;
        GameManager.gManager.readyForCountdown = false;
        placingRacers = false;
    }

    public void PlaceRacersInSpots()
    {
        // If the race has started and the racers have been added to the list of racers
        if (GameManager.gManager.racersAdded == true)
        {
            if (GameManager.gManager.racersPlaced == false)
            {
                // Iterate through the list of racer objects, and the list of start positions, and assign racers to their respective starting positions.
                for (int i = 0; i < GameManager.gManager.racerObjects.Count; i++)
                {
                    bool isPlayer = false;
                    foreach (GameObject playerOBJ in GameManager.gManager.players)
                    {
                        if (GameManager.gManager.allRacers[i] == playerOBJ)
                        {
                            isPlayer = true;
                        }
                    }   

                    if (isPlayer == false)
                    {
                        for (int a = 0; a < startPositions.Count(); a++)
                        {
                            StartingPositionDetails thisPosition = startPositions[a].GetComponent<StartingPositionDetails>();
                            if (thisPosition.SpotFilled == false)
                            {
                                thisPosition.HeldRacer = GameManager.gManager.racerObjects[i];
                                GameManager.gManager.racerObjects[i].GetComponent<ShipsControls>().enabled = true;
                                GameManager.gManager.racerObjects[i].GetComponent<ShipsControls>().SetRotationToTrack(GameManager.gManager.racerObjects[i].transform);
                                GameManager.gManager.racerObjects[i].GetComponent<ShipsControls>().ResetAngles(0.0f, 0.0f, 0.0f);
                                GameManager.gManager.racerObjects[i].GetComponent<ShipsControls>().ResetPositions(new Vector3(0.0f, 0.0f, 0.0f));
                                GameManager.gManager.racerObjects[i].transform.position = startPositions[a].transform.position;
                                GameManager.gManager.racerObjects[i].transform.rotation = startPositions[a].transform.rotation;
                                GameManager.gManager.racerObjects[i].GetComponent<ShipsControls>().enabled = false;
                                thisPosition.SpotFilled = true;
                                a = startPositions.Count();
                                break;
                            }
                        }
                    }
                }

                for (int i = 0; i < GameManager.gManager.players.Count; i++)
                {
                    for (int a = 0; a < startPositions.Count(); a++)
                    {
                        StartingPositionDetails thisPosition = startPositions[a].GetComponent<StartingPositionDetails>();
                        if (thisPosition.SpotFilled == false)
                        {
                            thisPosition.HeldRacer = GameManager.gManager.allRacers[i];
                            GameManager.gManager.allRacers[i].GetComponent<ShipsControls>().enabled = true;
                            GameManager.gManager.allRacers[i].GetComponent<ShipsControls>().SetRotationToTrack(GameManager.gManager.allRacers[i].transform);
                            GameManager.gManager.allRacers[i].GetComponent<ShipsControls>().ResetAngles(0.0f, 0.0f, 0.0f);
                            GameManager.gManager.allRacers[i].GetComponent<ShipsControls>().ResetPositions(new Vector3(0.0f, 0.0f, 0.0f));
                            GameManager.gManager.allRacers[i].transform.position = startPositions[a].transform.position;
                            GameManager.gManager.allRacers[i].transform.rotation = startPositions[a].transform.rotation;
                            GameManager.gManager.allRacers[i].GetComponent<ShipsControls>().enabled = false;
                            thisPosition.SpotFilled = true;
                            a = startPositions.Count();
                            break;
                        }

                    }


                    if (GameManager.gManager.players[i].GetComponent<PlayerAudioController>() != null)
                    {
                        PlayerAudioController pAC = GameManager.gManager.players[i].GetComponent<PlayerAudioController>();
                        pAC.StartEngineHum();
                    }
                }

                GameManager.gManager.racersPlaced = true;
                GameManager.gManager.readyForCountdown = true;
            }
        }
        placingRacers = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gManager.CurrentScene == "Race" && GameManager.gManager.racersPlaced == false && GameManager.gManager.raceStarted == false && placingRacers == false)
        {
            placingRacers = true;
            PlaceRacersInSpots();
        }
    }
}

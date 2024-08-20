using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UIControllerInput uiCInput;
    public CheckpointHandler checkpointParent;
    public GameObject[] StartingPoints;
    public PositionHandler pHandler;
    public RaceManager rManager;
    public IList<GameObject> playerObjects = new List<GameObject>();
    public IList<GameObject> racerObjects = new List<GameObject>();
    public bool racersAdded = false;
    public bool raceStarted = false;
    public bool indexListSorted = true;
    public int countdownIndex = 5;
    public int neededLaps = 0;

    public static GameManager gManager { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Awake()
    {
        if (gManager != null && gManager != this)
        {
            Destroy(this);
        }
        else
        {
            gManager = this;
        }
    }
}

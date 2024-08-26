using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UIControllerInput uiCInput;
    public CheckpointHandler checkpointParent;
    public PositionHandler pHandler;
    public ManageSceneLoading mSL;
    public RaceManager rManager;
    public FinishRace raceFinisher;
    public GameObject[] m_startButtons;
    public GameObject[] StartingPoints;
    public List<GameObject> players;
    public IList<GameObject> playerObjects = new List<GameObject>();
    public IList<GameObject> racerObjects = new List<GameObject>();
    public bool racersAdded = false;
    public bool raceStarted = false;
    public bool raceFinished = false;
    public bool indexListSorted = true;
    public int countdownIndex = 5;
    public int neededLaps = 0;
    public int numberOfPlayers = 0;

    public static GameManager gManager { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void AddToNumberOfPlayers() { numberOfPlayers += 1; }

    public GameObject FindStartButton()
    {
        return m_startButtons[numberOfPlayers - 1];
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

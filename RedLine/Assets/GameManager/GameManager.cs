using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public string CurrentScene;

    public RedlineActivator rActivator;
    public NameRandomiser nRandomiser;
    public RaceCountdown raceCountdown;
    public UIControllerInput uiCInput;
    public CheckpointHandler checkpointParent;
    public PositionHandler pHandler;
    public ManageSceneLoading mSL;
    public RaceManager rManager;
    public FinishRace raceFinisher;
    public ControllerHaptics hapticsController;

    public GameObject[] m_startButtons;
    public GameObject[] StartingPoints;

    public List<GameObject> players;
    public IList<GameObject> playerObjects = new List<GameObject>();
    public IList<GameObject> racerObjects = new List<GameObject>();

    public bool resetRacerVariables = false;
    public bool namesAssigned = false;

    public bool readyForCountdown = false;

    public bool raceStarted = false;
    public bool raceFinished = false;

    public bool racersAdded = false;
    public bool racersPlaced = false;

    public bool disablePlayerCams = false;
    public bool enablePlayerCams = false;

    public bool disableRacerMovement = false;
    public bool enableRacerMovement = false;

    public bool redlineActivated = false;
    
    public bool indexListSorted = true;

    public bool timingsListUpdated = false;

    public int countdownIndex = 2;
    public int neededLaps = 0;
    public int numberOfPlayers = 0;

    public static GameManager gManager { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (CurrentScene == "MainMenu" && enableRacerMovement == true)
        {
            enableRacerMovement = false;
        }

        if (resetRacerVariables == true)
        {
            resetRacerVariables = false;
            foreach (GameObject playerOBJ in players)
            {
                RacerDetails rDeets = playerOBJ.GetComponent<RacerDetails>();

                rDeets.ResetRacerVariables();
            }
        }

        if (disablePlayerCams == true)
        {
            disablePlayerCams = false;

            if (players.Count > 0)
            {
                foreach (GameObject playerOBJ in players)
                {
                    InitializeBeforeRace rDeets = playerOBJ.GetComponent<InitializeBeforeRace>();

                    if (rDeets.playerCamOBJECT != null)
                    {
                        rDeets.playerCamOBJECT.SetActive(false);
                    }
                }
            }
        }

        if (enablePlayerCams == true)
        {
            enablePlayerCams = false;

            if (players.Count > 0)
            {
                foreach (GameObject playerOBJ in players)
                {
                    InitializeBeforeRace rDeets = playerOBJ.GetComponent<InitializeBeforeRace>();

                    if (rDeets.playerCamOBJECT != null)
                    {
                        rDeets.playerCamOBJECT.SetActive(true);
                    }
                }
            }
        }

        if (CurrentScene == "Race" && enableRacerMovement == true)
        {
            enableRacerMovement = false;
            EnableRMovement();
        }

        if (CurrentScene == "Race" && raceStarted == false)
        {
            foreach (GameObject racerOBJ in racerObjects)
            {
                InitializeBeforeRace rDeets = racerOBJ.GetComponent<InitializeBeforeRace>();

                if (rDeets.sControls.enabled == true)
                {
                    rDeets.DisableShipControls();
                }
            }
        }
    }

    public void EnableRMovement()
    {
        foreach (GameObject racerOBJ in racerObjects)
        {
            InitializeBeforeRace rDeets = racerOBJ.GetComponent<InitializeBeforeRace>();
            rDeets.EnableRacerMovement();
        }
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

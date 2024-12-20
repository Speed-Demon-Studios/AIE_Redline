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
    public PauseMenu pMenu;

    public List<GameObject> m_startButtons = new();
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

    public bool timeStopped = false;

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
    }

    public void StopTime()
    {
        pMenu.transform.GetChild(0).gameObject.SetActive(true);
        players[0].GetComponent<ActionMappingControl>().UpdateActionMapForUI();
        pMenu.SwitchPlayerOneButton();
        Time.timeScale = 0;
    }

    public void StartTime(bool switchs)
    {
        if(switchs)
            players[0].GetComponent<ActionMappingControl>().UpdateActionMapForRace();
        Time.timeScale = 1;
    }

    public void EnableRMovement()
    {
        foreach (GameObject racerOBJ in racerObjects)
        {
            InitializeBeforeRace rDeets = racerOBJ.GetComponent<InitializeBeforeRace>();
            Rigidbody rB = racerOBJ.GetComponent<Rigidbody>();
            rDeets.EnableRacerMovement();
            rB.isKinematic = false;
        }
    }

    public void DisableRMovement(GameObject racer = null)
    {
        if (racer != null)
        {
            InitializeBeforeRace rDeets = racer.GetComponent<InitializeBeforeRace>();
            Rigidbody rB = racer.GetComponent<Rigidbody>();
            ShipsControls sControls = racer.GetComponent<ShipsControls>();

            rB.velocity = new Vector3(0, 0, 0);
            rB.angularVelocity = new Vector3(0, 0, 0);

            sControls.ResetAcceleration();
            rB.isKinematic = true;

            rDeets.DisableShipControls();
        }
        else if (racer == null)
        {
            foreach (GameObject racerOBJ in racerObjects)
            {
                InitializeBeforeRace rDeets = racerOBJ.GetComponent<InitializeBeforeRace>();
                Rigidbody rB = racerOBJ.GetComponent<Rigidbody>();
                ShipsControls sControls = racerOBJ.GetComponent<ShipsControls>();

                rB.velocity = new Vector3(0, 0, 0);
                rB.angularVelocity = new Vector3(0, 0, 0);

                sControls.ResetAcceleration();
                rB.isKinematic = true;

                rDeets.DisableShipControls();
            }
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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIControllerInput : MonoBehaviour
{
    [SerializeField] private GameObject InputControllerParentPrefab;
    private PlayerInputManager PIM;
    [SerializeField] private int currentMenuObjectIndex = 0;
    public TextMeshProUGUI playerCountText;
    private int m_numberOfPalyers;
    public void SetNumberOfPlayers(int number) { m_numberOfPalyers = number; }
    public int GetNumberOfPlayers() { return m_numberOfPalyers; }
    [SerializeField] private GameObject firstButton;
    public Transform selectionMenuGrid;
    public GameObject SelectionMenu;
    private List<GameObject> m_selectionMenuButtons = new();
    public List<RenderTexture> textures = new();
    public bool OnShipSelection = false;

    // Testing
    private bool HasInitialized = true;

    /// <summary>
    /// Adds another selectionMenu in for when a player joins
    /// </summary>
    public void AddToPlayers()
    {
        m_numberOfPalyers += 1;
        if(playerCountText != null)
            playerCountText.text = "Player Count: " + m_numberOfPalyers;

        //
        //---------------------------------------------------------------------------------------------------------------------------------|
        GameObject a = Instantiate(SelectionMenu, selectionMenuGrid);// Instantiate the selectionMenu                                      |
                                                                     //                                                                    | 
        m_selectionMenuButtons.Add(a); // Adds that new selectionMenu to personal list                                                     |
                                                                                                                                         //|
        GameManager.gManager.m_startButtons.Add(a.GetComponentInChildren<Button>().gameObject); // Adds the button to the GameManager list |
                                                                                                                                         //|
        a.GetComponent<ShipSelection>().texture = textures[m_numberOfPalyers - 1]; // Assigns the texture which displays the ship          |
                                                                                                                                         //|
        a.GetComponent<ShipSelection>().playerNum = m_numberOfPalyers - 1;  // Assigns the players number to the selectionMenu             |
                                                                                                                                         //|
        //---------------------------------------------------------------------------------------------------------------------------------|
        //

    }
    private void Awake()
    {
        //---------------------------------------------------------------------------------------------------------------------------------|
        GameManager.gManager.CurrentScene = "MainMenu";// Sets a string to MainMenu to know when we are in main menu                       |
        GameManager.gManager.disablePlayerCams = true; // turns a bool on that will disaple all cameras when in the main menu              |                                                                                  
        GameManager.gManager.resetRacerVariables = true; // turns a bool on that will let the game know the players variables are ready    |                                                                                
        GameManager.gManager.m_startButtons[0] = firstButton; // Sets the firstButton to the start buttons 1 list item                     |                                                                            
        GameManager.gManager.mSL.SetPlayerUIInputMM(); // Reseting player 1 main menu button                                               |                                                                                   
        //---------------------------------------------------------------------------------------------------------------------------------|                                                                                                                                         //|
    }

    private void Update()
    {
        if (playerCountText != null)
            playerCountText.text = "Player Count: " + m_numberOfPalyers;
    }

    /// <summary>
    /// When a player disconnects we need to delete the player selection screen which this does
    /// </summary>
    /// <param name="selection"> what selection to delete </param>
    public void DeleteSelection(GameObject selection)
    {
        //---------------------------------------------------------------------------------------------------------------------------------|
        if (m_selectionMenuButtons.Contains(selection)) // if the list contains the deleting menu then remove it from the list             |
        {                                                                                                                                //|
            m_selectionMenuButtons.Remove(selection); // Remove it from the list                                                           |
        }                                                                                                                                //|
        else                                                                                                                             //|
        {                                                                                                                                //|
            Debug.Log("Does not contain " + selection + " in the list"); // If not then post an error                                      |
        }                                                                                                                                //|
        //---------------------------------------------------------------------------------------------------------------------------------|
    }

    //public void InitializePlayerConnections()
    //{
    //    //---------------------------------------------------------------------------------------------------------------------------------|
    //    HasInitialized = false;                                                                                                            |
    //    GameObject inputControllerParent = Instantiate(InputControllerParentPrefab);                                                       |
    //    PIM = inputControllerParent.GetComponent<PlayerInputManager>();                                                                    |
    //    PIM.EnableJoining();                                                                                                               |
    //    HasInitialized = true;                                                                                                             |
    //    Debug.Log("Players can now join.");                                                                                                |
    //    //---------------------------------------------------------------------------------------------------------------------------------|
    //}


    public void GoToRace()
    {
        //---------------------------------------------------------------------------------------------------------------------------------|
        if (HasInitialized == true)
        {
            foreach(GameObject player in GameManager.gManager.players)
            {
                player.GetComponent<PlayerInputScript>().player.SwitchCurrentActionMap("Player");
                RedlineColliderSpawner redline = null;
                foreach (Transform child in player.transform)
                {
                    if (child.GetComponent<RedlineColliderSpawner>())
                        redline = child.GetComponent<RedlineColliderSpawner>();
                }

                    AttachModels(player.GetComponent<ShipsControls>());

                foreach (Transform child in player.transform)
                {
                    FindEveryChild(child, redline);
                }

                ActionMappingControl aMC = player.GetComponent<ActionMappingControl>();
                aMC.mES.firstSelectedGameObject = null;
                aMC.mES.SetSelectedGameObject(null);
            }

            Debug.Log("Loading Race Scene");
            Debug.Log("Ready To Start Race");
            GameManager.gManager.racerObjects = new List<GameObject>();

            SceneManager.LoadSceneAsync(1);
        }
        //---------------------------------------------------------------------------------------------------------------------------------|
    }

    public void AttachModels(ShipsControls ship)
    {
        Instantiate(ship.VariantObject.model, ship.shipModel.transform);
        Instantiate(ship.VariantObject.collision, ship.collisionParent);
    }

    public void FindEveryChild(Transform parent, RedlineColliderSpawner redline)
    {
        foreach (Transform child in parent)
        {
            if (child.CompareTag("TrailSpawn"))
                redline.spawnPoint = child;

            if (child.transform.childCount > 0)
                FindEveryChild(child, redline);
        }
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void ReadyPlayer(int playerNumber)
    {
        GameManager.gManager.players[playerNumber].GetComponent<PlayerInputScript>().playerReadyInMenu = true;
        int playersReady = 0;
        foreach(GameObject player in GameManager.gManager.players)
        {
            if (player.GetComponent<PlayerInputScript>().playerReadyInMenu)
                playersReady += 1;
        }

        if(playersReady >= GameManager.gManager.players.Count)
        {
            GoToRace();
        }
    }

    public void CallResetButtons()
    {
        int index = 0;
        foreach (GameObject player in GameManager.gManager.players)
        {
            ActionMappingControl aMC = player.GetComponent<ActionMappingControl>();

            aMC.UpdateActionMapForUI();

            if (index != 0)
            {
                ResetFirstButtonSelect(index);
            }

            index++;
        }
    }

    public void ResetFirstButtonSelect(int playerNumber)
    {
        OnShipSelection = true;
        GameManager.gManager.players[playerNumber].GetComponent<ActionMappingControl>().mES.SetSelectedGameObject(m_selectionMenuButtons[playerNumber].GetComponentInChildren<Button>().gameObject);
        GameManager.gManager.players[playerNumber].GetComponent<PlayerInputScript>().SetSelection(m_selectionMenuButtons[playerNumber].GetComponent<ShipSelection>());
        m_selectionMenuButtons[playerNumber].GetComponent<ShipSelection>().SetShip(GameManager.gManager.playerObjects[playerNumber]);

    }

    public void ResetFirstButtonSelectForPlayerOne()
    {
        OnShipSelection = true;
        int index = 0;
        GameManager.gManager.players[index].GetComponent<ActionMappingControl>().mES.SetSelectedGameObject(m_selectionMenuButtons[index].GetComponentInChildren<Button>().gameObject);
        GameManager.gManager.players[index].GetComponent<PlayerInputScript>().SetSelection(m_selectionMenuButtons[index].GetComponent<ShipSelection>());
        m_selectionMenuButtons[index].GetComponent<ShipSelection>().SetShip(GameManager.gManager.playerObjects[index]);

    }

    public void SetUpSelectionScreen()
    {
        foreach (GameObject playerOBJ in GameManager.gManager.players)
        {
            playerOBJ.GetComponent<PlayerInputScript>().ReturnShipSelection().SetUp();
        }
    }
}

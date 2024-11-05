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

namespace MenuManagement
{
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

        private MenuManager m_mManager;
        public SetMenu shipSelectionMenu;
        // Testing
        private bool HasInitialized = true;

        public MenuManager GetMenuManager() { return m_mManager; }
        /// <summary>
        /// Adds another selectionMenu in for when a player joins
        /// </summary>
        public void AddToPlayers()
        {
            m_numberOfPalyers += 1;
            if (playerCountText != null)
                playerCountText.text = "Player Count: " + m_numberOfPalyers;

            //---------------------------------------------------------------------------------------------------------------------------------|
            GameObject a = Instantiate(SelectionMenu, selectionMenuGrid);// Instantiate the selectionMenu                                      |
                                                                                                                                             //| 
            m_selectionMenuButtons.Add(a); // Adds that new selectionMenu to personal list                                                     |
                                                                                                                                             //|
            shipSelectionMenu.menuStartButtons.Add(a.GetComponentInChildren<Button>()); // Adds the button to the GameManager list             |
                                                                                                                                             //|
            a.GetComponent<ShipSelection>().texture = textures[m_numberOfPalyers - 1]; // Assigns the texture which displays the ship          |
                                                                                                                                             //|
            a.GetComponent<ShipSelection>().playerNum = m_numberOfPalyers - 1;  // Assigns the players number to the selectionMenu             |
                                                                                //|
            if (m_mManager != null && m_mManager.GetCurrentType() == MenuType.ShipSelectionUnready)
                m_mManager.SetButtons(m_mManager.GetCurrentMenu());
        //-------------------------------------------------------------------------------------------------------------------------------------|
        }
        private void Awake()
        {
            //---------------------------------------------------------------------------------------------------------------------------------|
            GameManager.gManager.uiCInput = this; // Sets the gameManagers reference to this script                                            |
            GameManager.gManager.CurrentScene = "MainMenu";// Sets a string to MainMenu to know when we are in main menu                       |
            GameManager.gManager.disablePlayerCams = true; // turns a bool on that will disaple all cameras when in the main menu              |                                                                                  
            GameManager.gManager.resetRacerVariables = true; // turns a bool on that will let the game know the players variables are ready    |                                                                                                                                                           
            GameManager.gManager.mSL.SetPlayerUIInputMM(); // Reseting player 1 main menu button                                               |
            m_mManager = GetComponent<MenuManager>();
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
            if (HasInitialized == true) //                                                                                                     |
            { //                                                                                                                               |
                foreach (GameObject player in GameManager.gManager.players) // For each player in the player list                               |
                { //                                                                                                                           |
                    player.GetComponent<PlayerInputScript>().player.SwitchCurrentActionMap("Player"); // Switch the action map to player       |
                    RedlineColliderSpawner redline = null; // Makes a new reference to the redline collider spawner script                     |
                                                           //---------------------------------------------------------------------------------------------------------------------------------|
                    foreach (Transform child in player.transform) // for each child object in the player object                                |
                    { //                                                                                                                       |
                        if (child.GetComponent<RedlineColliderSpawner>()) // If the child object has a redline collider spawner script         |
                            redline = child.GetComponent<RedlineColliderSpawner>(); // then assign it to the redline reference                 |
                    } //                                                                                                                       |
                      //---------------------------------------------------------------------------------------------------------------------------------|
                    AttachModels(player.GetComponent<ShipsControls>()); // Attach the models of the ship the player selected               |
                                                                        //---------------------------------------------------------------------------------------------------------------------------------|
                    foreach (Transform child in player.transform) // do another check on the redline collider spawner reference                |
                    { //                                                                                                                       |
                        FindEveryChild(child, redline); //                                                                                     |
                    } //                                                                                                                       |
                      //---------------------------------------------------------------------------------------------------------------------------------|
                    ActionMappingControl aMC = player.GetComponent<ActionMappingControl>(); //          Reseting the first selected buttons    |
                    aMC.mES.firstSelectedGameObject = null; //                                                                                 |
                    aMC.mES.SetSelectedGameObject(null); //                                                                                    |
                } //                                                                                                                           |
                  //---------------------------------------------------------------------------------------------------------------------------------|
                Debug.Log("Loading Race Scene"); //                                                                                            |
                Debug.Log("Ready To Start Race"); //                                                                                           |
                GameManager.gManager.racerObjects = new List<GameObject>(); // Empty the racerObject List                                      |
                for (int i = GameManager.gManager.allRacers.Count - 1; i >= 0; i--)                                                          //|
                {                                                                                                                            //|
                    GameObject temp = GameManager.gManager.allRacers[i];                                                                     //|
                                                                                                                                             //|
                    if (temp == null)                                                                                                        //|
                    {                                                                                                                        //|
                        GameManager.gManager.allRacers.Remove(temp);                                                                         //|
                    }                                                                                                                        //|
                }                                                                                                                            //|
                                                                                                                                             //                                                                                                                             |
                SceneManager.LoadSceneAsync(2); // Load the new race scene                                                                     |
            } //                                                                                                                               |
              //---------------------------------------------------------------------------------------------------------------------------------|
        }

        /// <summary>
        /// Spawn the models onto the ship for the ship that the player chose
        /// </summary>
        /// <param name="ship"> Which player ship controls is it </param>
        public void AttachModels(ShipsControls ship)
        {
            Instantiate(ship.VariantObject.model, ship.shipModel.transform);
            Instantiate(ship.VariantObject.collision, ship.collisionParent);
        }

        /// <summary>
        /// go through every child object in each object untill you find the trailSpawner
        /// </summary>
        /// <param name="parent"> the parent </param>
        /// <param name="redline"> reference to the redline collider spawner script </param>
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

        /// <summary>
        /// Quit the game
        /// </summary>
        public void QuitButton()
        {
            Application.Quit();
        }

        /// <summary>
        /// Check if each player is ready
        /// </summary>
        /// <param name="playerNumber"> which player just ready up </param>
        public void ReadyPlayer(int playerNumber)
        {
            //---------------------------------------------------------------------------------------------------------------------------------|
            // go to that players inputScript and change the bool to say they are ready                                                        |
            GameManager.gManager.players[playerNumber].GetComponent<PlayerInputScript>().playerReadyInMenu = true; //                          |
                                                                                                                   // the number of players ready                                                                                                     |
            int playersReady = 0; //                                                                                                           |
                                  //---------------------------------------------------------------------------------------------------------------------------------|
                                  // for each player in the players list                                                                                             |
            foreach (GameObject player in GameManager.gManager.players) //                                                                     |
            { //                                                                                                                               |
                if (player.GetComponent<PlayerInputScript>().playerReadyInMenu) // if the player is ready                                      |
                    playersReady += 1; // add 1 to the number of players ready                                                                 |
            } //                                                                                                                               |
              //---------------------------------------------------------------------------------------------------------------------------------|
            if (playersReady >= GameManager.gManager.players.Count) // if the number of player ready is equal to the number of players         |
            { //                                                                                                                               |
                GoToRace(); // then go to race                                                                                                 |
            } //                                                                                                                               |
              //---------------------------------------------------------------------------------------------------------------------------------|
        }

        /// <summary>
        /// Reseting the action maps and the new first selected buttons
        /// </summary>
        public void CallResetButtons()
        {
            //---------------------------------------------------------------------------------------------------------------------------------|
            int index = 0; // index for the foreach loop                                                                                       |
                           //                                                                                                                                 |
            foreach (GameObject player in GameManager.gManager.players) // foreach player in players list                                      |
            { //                                                                                                                               |
                ActionMappingControl aMC = player.GetComponent<ActionMappingControl>(); // reference to the actionMappingControl               |
                                                                                        //                                                                                                                               |
                aMC.UpdateActionMapForUI(); // Update Action map                                                                               |
                                            //                                                                                                                               |
                if (index != 0) //                                                                                                             |
                { //                                                                                                                           |
                    ResetFirstButtonSelect(index); // reset the first button selection                                                         |
                } //                                                                                                                           |
                  //                                                                                                                               |
                index++; //                                                                                                                    |
            } //                                                                                                                               |
              //---------------------------------------------------------------------------------------------------------------------------------|
        }

        /// <summary>
        /// Reseting the first selected button for the player so they can use the menu screen
        /// </summary>
        /// <param name="playerNumber"> which player is it </param>
        public void ResetFirstButtonSelect(int playerNumber)
        {
            OnShipSelection = true;
            GameManager.gManager.players[playerNumber].GetComponent<ActionMappingControl>().mES.SetSelectedGameObject(m_selectionMenuButtons[playerNumber].GetComponentInChildren<Button>().gameObject);
            GameManager.gManager.players[playerNumber].GetComponent<PlayerInputScript>().SetSelection(m_selectionMenuButtons[playerNumber].GetComponent<ShipSelection>());
            m_selectionMenuButtons[playerNumber].GetComponent<ShipSelection>().SetShip(GameManager.gManager.allRacers[playerNumber]);

        }

        public void ResetFirstButton(int playerNumber, Button button)
        {
            GameManager.gManager.players[playerNumber].GetComponent<ActionMappingControl>().mES.SetSelectedGameObject(button.gameObject);
            GameManager.gManager.players[playerNumber].GetComponent<ActionMappingControl>().mES.firstSelectedGameObject = button.gameObject;
        }

        /// <summary>
        /// Reseting the first selected button for player 1 so they can use the menu screen
        /// </summary>
        public void ResetFirstButtonSelectForPlayerOne()
        {
            OnShipSelection = true;
            int index = 0;
            GameManager.gManager.players[index].GetComponent<ActionMappingControl>().mES.SetSelectedGameObject(m_selectionMenuButtons[index].GetComponentInChildren<Button>().gameObject);
            GameManager.gManager.players[index].GetComponent<PlayerInputScript>().SetSelection(m_selectionMenuButtons[index].GetComponent<ShipSelection>());
            m_selectionMenuButtons[index].GetComponent<ShipSelection>().SetShip(GameManager.gManager.allRacers[index]);

        }

        /// <summary>
        /// Reseting the first selected button for player 1 for the difficultyMenu
        /// </summary>
        public void ResetFirstButtonForPlayerOne(GameObject button)
        {
            OnShipSelection = true;
            int index = 0;
            GameManager.gManager.players[index].GetComponent<ActionMappingControl>().mES.SetSelectedGameObject(button);

        }

        /// <summary>
        /// Sets up the selection screen for each player
        /// </summary>
        public void SetUpSelectionScreen()
        {
            foreach (GameObject playerOBJ in GameManager.gManager.players)
            {
                playerOBJ.GetComponent<PlayerInputScript>().ReturnShipSelection().SetUp();
            }
        }
    }
}

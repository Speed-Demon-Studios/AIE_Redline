using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using TMPro;
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

    public void AddToPlayers()
    {
        m_numberOfPalyers += 1;
        if(playerCountText != null)
            playerCountText.text = "Player Count: " + m_numberOfPalyers;
        GameObject a = Instantiate(SelectionMenu, selectionMenuGrid);
        m_selectionMenuButtons.Add(a);
        GameManager.gManager.m_startButtons.Add(a.GetComponentInChildren<Button>().gameObject);
        a.GetComponent<ShipSelection>().texture = textures[m_numberOfPalyers - 1];
        a.GetComponent<ShipSelection>().playerNum = m_numberOfPalyers - 1;
    }
    private void Awake()
    {
        GameManager.gManager.CurrentScene = "MainMenu";
        GameManager.gManager.disablePlayerCams = true;
        GameManager.gManager.resetRacerVariables = true;
        GameManager.gManager.m_startButtons[0] = firstButton;
        GameManager.gManager.mSL.SetPlayerUIInputMM();

    }

    private void Update()
    {
        if (playerCountText != null)
            playerCountText.text = "Player Count: " + m_numberOfPalyers;
    }

    public void DeleteSelection(GameObject selection)
    {
        if (m_selectionMenuButtons.Contains(selection))
        {
            m_selectionMenuButtons.Remove(selection);
        }
        else
        {
            Debug.Log("Does not contain " + selection + " in the list");
        }
    }

    public void InitializePlayerConnections()
    {
        HasInitialized = false;
        GameObject inputControllerParent = Instantiate(InputControllerParentPrefab);
        PIM = inputControllerParent.GetComponent<PlayerInputManager>();
        PIM.EnableJoining();
        HasInitialized = true;
        Debug.Log("Players can now join.");
    }


    public void GoToRace()
    {
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
}

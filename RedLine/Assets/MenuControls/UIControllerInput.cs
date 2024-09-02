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
    [SerializeField] private GameObject firstButton;

    // Testing
    private bool HasInitialized = true;

    public void AddToPlayers()
    {
        m_numberOfPalyers += 1;
        if(playerCountText != null)
            playerCountText.text = "Player Count: " + m_numberOfPalyers;
    }
    private void Awake()
    {
        GameManager.gManager.CurrentScene = "MainMenu";
        GameManager.gManager.disablePlayerCams = true;
        GameManager.gManager.resetRacerVariables = true;
        GameManager.gManager.m_startButtons[0] = firstButton;
        GameManager.gManager.mSL.SetPlayerUIInputMM();

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

                ActionMappingControl aMC = player.GetComponent<ActionMappingControl>();
                aMC.mES.firstSelectedGameObject = null;
                aMC.mES.SetSelectedGameObject(null);

            }
            Debug.Log("Loading Race Scene");
            Debug.Log("Ready To Start Race");
            GameManager.gManager.racerObjects = new List<GameObject>();
            SceneManager.LoadScene(1);
        }
    }
}

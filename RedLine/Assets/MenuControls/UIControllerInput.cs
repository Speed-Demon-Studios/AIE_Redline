using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIControllerInput : MonoBehaviour
{
    [SerializeField] private GameObject[] menuButtonsHOLDER;
    [SerializeField] private Button[] vMenuButtons;
    [SerializeField] private GameObject[] hMenuButtons;
    [SerializeField] private int[] hMenuIndexes;
    [SerializeField] private GameObject InputControllerParentPrefab;
    [SerializeField] private Color buttonHighlightedColor;
    [SerializeField] private Color buttonPressedColor;
    [SerializeField] private Color buttonDefaultColor;
    private bool menuOpen = false;
    private PlayerInputManager PIM;
    private int currentMenuObjectIndex = 0;
    public TextMeshProUGUI playerCountText;

    // Testing
    private bool HasInitialized = false;

    public void MenuUP()
    {
        if ((currentMenuObjectIndex - 1) >= 0)
        {
            currentMenuObjectIndex -= 1;
        }
    }


    private void Start()
    {
        InitializePlayerConnections();
    }

    public void InitializePlayerConnections()
    {
        HasInitialized = false;
        GameObject inputControllerParent = Instantiate(InputControllerParentPrefab);
        PIM = inputControllerParent.GetComponent<PlayerInputManager>();
        PIM.EnableJoining();
        foreach (Button menuItem in vMenuButtons)
        {
            menuItem.enabled = true;
        }
        HasInitialized = true;
        Debug.Log("Players can now join.");
    }


    public void GoToRace()
    {
        if (HasInitialized == true)
        {
            //GameManager.gManager.raceStarted = true;
            Debug.Log("Loading Race Scene");
            Debug.Log("Ready To Start Race");
            SceneManager.LoadScene(1);
        }
    }


    private void Update()
    {
        if (PIM != null)
        {
            if (PIM.playerCount == 4)
            {
                PIM.DisableJoining();
            }
        }

        playerCountText.text = "Number of Players: " + PIM.playerCount;
    }
}

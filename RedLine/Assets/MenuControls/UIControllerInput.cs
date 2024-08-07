using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIControllerInput : MonoBehaviour
{
    [SerializeField] private GameObject[] menuButtonsHOLDER;
    [SerializeField] private GameObject[] vMenuButtons;
    [SerializeField] private GameObject[] hMenuButtons;
    [SerializeField] private int[] hMenuIndexes;
    [SerializeField] private GameObject InputControllerParentPrefab;
    private PlayerInputManager PIM;
    private int playerCount = 0;
    public TextMeshProUGUI playerCountText;

    // Testing
    private bool HasInitialized = false;

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
        HasInitialized = true;
        Debug.Log("Players can now join.");
    }

    private void Update()
    {
        if (PIM != null)
        {
            playerCount = PIM.playerCount;
            if (PIM.playerCount == 4)
            {
                PIM.DisableJoining();
            }
        }

        playerCountText.text = "Number of Players: " + playerCount;

        if (HasInitialized == true)
        {
            if (Input.GetKeyUp(KeyCode.KeypadEnter))
            {
                //GameManager.gManager.raceStarted = true;
                Debug.Log("Loading Race Scene");
                Debug.Log("Ready To Start Race");
                SceneManager.LoadScene(1);
            }
        }
    }
}

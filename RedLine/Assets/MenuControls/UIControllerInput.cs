using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.InputSystem;
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
        if (PIM.playerCount == 4)
        {
            PIM.DisableJoining();
        }

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

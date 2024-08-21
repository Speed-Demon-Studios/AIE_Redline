using System.Collections.Generic;
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
    //[SerializeField] private GameObject[] menuButtonsHOLDER;
    //[SerializeField] private Button[] vMenuButtons;
    //[SerializeField] private GameObject[] hMenuButtons;
    //[SerializeField] private int[] hMenuIndexes;
    [SerializeField] private GameObject InputControllerParentPrefab;
    //[SerializeField] private Color buttonHighlightedColor;
    //[SerializeField] private Color buttonPressedColor;
    //[SerializeField] private Color buttonDefaultColor;
    //private bool menuOpen = true;
    private PlayerInputManager PIM;
    [SerializeField] private int currentMenuObjectIndex = 0;
    public TextMeshProUGUI playerCountText;

    // Testing
    private bool HasInitialized = true;

    public void MenuUP()
    {
        //if (vMenuButtons.Count() > 1)
        //{
        //    if ((currentMenuObjectIndex - 1) >= 0)
        //    {
        //        currentMenuObjectIndex -= 1;
        //    }
        //    if (currentMenuObjectIndex - 1 < 0)
        //    {
        //        currentMenuObjectIndex = 0;
        //    }
        //}
        //else if (vMenuButtons.Count() == 1)
        //{
        //    currentMenuObjectIndex = 0;
        //}
        //
        //ButtonHighlights();
    }

    public void MenuDown()
    {
        //if (vMenuButtons.Count() > 1)
        //{
        //    if ((currentMenuObjectIndex + 1) < vMenuButtons.Count())
        //    {
        //        currentMenuObjectIndex += 1;
        //    }
        //}
        //else if (vMenuButtons.Count() == 1)
        //{
        //    currentMenuObjectIndex = 0;
        //}
        //ButtonHighlights();
    }

    public void MenuConfirm()
    {
        //MenuButtonPressed();
        //vMenuButtons[currentMenuObjectIndex].onClick.Invoke();
    }

    public void MenuButtonPressed()
    {
        //if (vMenuButtons.Count() > 1)
        //{
        //    foreach (Button menuButton in vMenuButtons)
        //    {
        //        menuButton.GetComponentInChildren<Image>().color = buttonDefaultColor;
        //    }
        //
        //    vMenuButtons[currentMenuObjectIndex].GetComponentInChildren<Image>().color = buttonPressedColor;
        //}
        //else if (vMenuButtons.Count() == 1)
        //{
        //    vMenuButtons[0].GetComponentInChildren<Image>().color = buttonPressedColor;
        //}
        //
        //return;
    }

    public void ButtonHighlights()
    {
        //if (vMenuButtons.Count() > 1)
        //{
        //    foreach (Button menuButton in vMenuButtons)
        //    {
        //        menuButton.GetComponentInChildren<Image>().color = buttonDefaultColor;
        //    }
        //    vMenuButtons[currentMenuObjectIndex].GetComponentInChildren<Image>().color = buttonHighlightedColor;
        //}
        //else if (vMenuButtons.Count() == 1)
        //{
        //    vMenuButtons[0].GetComponentInChildren<Image>().color = buttonHighlightedColor;
        //}
        //return;
    }


    private void Start()
    {
        //InitializePlayerConnections();
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
            }
            Debug.Log("Loading Race Scene");
            Debug.Log("Ready To Start Race");
            SceneManager.LoadScene(1);
        }
    }


    private void Update()
    {
        //if (PIM != null)
        //{
        //    if (PIM.playerCount == 4)
        //    {
        //        PIM.DisableJoining();
        //    }
        //}
        //playerCountText.text = "Number of Players: " + PIM.playerCount;
    }
}

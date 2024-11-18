using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using MenuManagement;
using System;

namespace MenuManagement
{
    public enum MenuType
    {
        PlayerOneJoin,
        PlayersJoined,
        Main,
        Option,
        Credits,
        Difficulty,
        ShipSelectionUnready,
        ShipSelectionReady
    }
    public class MenuManager : MonoBehaviour
    {
        private bool m_gameLoadedAndStarted;
        private MenuType m_currentMenuType;

        public SetMenu start;
        public SetMenu mainMenu;
        private SetMenu m_currentMenu;

        public MenuType GetCurrentType() { return m_currentMenuType; }
        public SetMenu GetCurrentMenu() { return m_currentMenu; }

        // Start is called before the first frame update
        void Start()
        {
            if (PlayerPrefs.GetFloat("AfterRace") == 1)
            {
                foreach(GameObject player in GameManager.gManager.players)
                {
                    player.GetComponent<SelectionScreenSpawn>().Initialize();
                    if(player.GetComponent<PlayerInputScript>().GetPlayerNumber() != 1)
                        player.GetComponent<ActionMappingControl>().GetPlayerInput().gameObject.SetActive(true);
                }
                PressStart();
                m_gameLoadedAndStarted = false;
            }
            else
                m_gameLoadedAndStarted = false;


        }

        // Update is called once per frame
        void Update()
        {

        }

        public void PressStart()
        {
            if (!m_gameLoadedAndStarted)
            {
                start.gameObject.SetActive(false);
                m_currentMenu = mainMenu;
                m_currentMenu.gameObject.SetActive(true);
                GameManager.gManager.uiCInput.ResetFirstButton(0, mainMenu.menuStartButtons[0]);
                m_gameLoadedAndStarted = true;
            }
        }

        public void BackOutMenu(int playerNumber)
        {
            switch (m_currentMenuType)
            {
                case MenuType.Difficulty:
                    Back();
                    break;
                case MenuType.Option:
                    Back();
                    break;
                case MenuType.Credits:
                    Back();
                    break;
                case MenuType.ShipSelectionReady:
                    GameManager.gManager.uiCInput.UnReadyPlayer(playerNumber);
                    break;

            }
        }

        public void SwitchMenu(SetMenu switchingTo)
        {
            GameObject switchingToGameObject = switchingTo.gameObject;

            m_currentMenu.gameObject.SetActive(false);
            switchingToGameObject.SetActive(true);

            m_currentMenu = switchingTo;

            m_currentMenuType = switchingTo.typeOfMenu;

            SetButtons(switchingTo);
        }

        public void SetButtons(SetMenu menu)
        {
            int index = 0;
            foreach (Button startButton in menu.menuStartButtons)
            {
                GameManager.gManager.uiCInput.ResetFirstButton(index, startButton);
                index++;
            }
        }

        public void Back()
        {
            GameObject prevObject = m_currentMenu.prevMenu.gameObject;
            GameObject currentObject = m_currentMenu.gameObject;

            currentObject.SetActive(false);
            prevObject.SetActive(true);

            m_currentMenu = m_currentMenu.prevMenu;
            SetButtons(m_currentMenu);
        }
    }
}

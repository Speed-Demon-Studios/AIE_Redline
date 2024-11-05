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

        public void BackOutMenu()
        {
            switch (m_currentMenuType)
            {
                case MenuType.Difficulty:
                    BackToMenu();
                    break;
                case MenuType.Option:
                    BackToMenu();
                    break;
                case MenuType.Credits:
                    BackToMenu();
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

        private void BackToMenu()
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

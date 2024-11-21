using MenuManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundPanelsController : MonoBehaviour
{
    [SerializeField] List<GameObject> m_pressAToStart, m_title, m_selectClass, m_selectShip, m_options, m_credits;

    public void ChangeBGPanels()
    {
        MenuType currentMenu = GameManager.gManager.uiCInput.GetMenuManager().GetCurrentType();
        List<GameObject> currentBGPanels = new();

        switch (currentMenu)
        {
            case MenuType.PlayerOneJoin:
                currentBGPanels = m_pressAToStart;
                break;

            case MenuType.Main:
                currentBGPanels = m_title;
                break;

            case MenuType.Difficulty:
                currentBGPanels = m_selectClass;
                break;

            case MenuType.ShipSelectionReady:
                currentBGPanels = m_selectShip;
                break;

            case MenuType.Option:
                currentBGPanels = m_options;
                break;

            case MenuType.Credits:
                currentBGPanels = m_credits;
                break;
        }

        foreach (Transform child in gameObject.transform)
        {
            child.gameObject.SetActive(false);
        }

        foreach (GameObject obj in currentBGPanels)
        {
            obj.SetActive(true);
        }
    }

}

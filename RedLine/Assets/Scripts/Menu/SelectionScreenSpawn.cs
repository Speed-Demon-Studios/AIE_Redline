using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionScreenSpawn : MonoBehaviour
{
    public GameObject SelectionMenu;
    private int m_playerNumber;

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        SpawnShipSelection();
    }

    private void SpawnShipSelection()
    {
        m_playerNumber = GetComponent<PlayerInputScript>().GetPlayerNumber();// set player number

        GameObject selectionMenu = Instantiate(SelectionMenu, GameManager.gManager.uiCInput.selectionMenuGrid);// Instantiate the selectionMenu

        StartCoroutine(SetShipSelection(selectionMenu));

        selectionMenu.GetComponent<ShipSelection>().SetShipSelectionNumbers(m_playerNumber - 1);

        GameManager.gManager.uiCInput.shipSelectionMenu.menuStartButtons.Add(selectionMenu.GetComponent<ShipSelection>().readyButton); // Adds the button to the GameManager list
    }

    IEnumerator SetShipSelection(GameObject selection)
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        GameObject player = GameManager.gManager.players[m_playerNumber - 1];
        player.GetComponent<PlayerInputScript>().SetSelection(selection.GetComponent<ShipSelection>());

        selection.GetComponent<ShipSelection>().SetShip(player);
    }

}

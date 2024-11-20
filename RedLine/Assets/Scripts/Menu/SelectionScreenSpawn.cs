using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionScreenSpawn : MonoBehaviour
{
    private int m_playerNumber;

    public void SpawnShipSelection(GameObject prefabToSpawn)
    {
        m_playerNumber = GetComponent<PlayerInputScript>().GetPlayerNumber();// set player number

        GameObject selectionMenu = Instantiate(prefabToSpawn, GameManager.gManager.uiCInput.selectionMenuGrid);// Instantiate the selectionMenu

        selectionMenu.GetComponent<ShipSelection>().SetShipSelectionNumbers(m_playerNumber - 1);

        GameManager.gManager.uiCInput.shipSelectionMenu.menuStartButtons.Add(selectionMenu.GetComponent<ShipSelection>().readyButton); // Adds the button to the GameManager list

        GameObject player = GameManager.gManager.players[m_playerNumber - 1];
        player.GetComponent<PlayerInputScript>().SetSelection(selectionMenu.GetComponent<ShipSelection>());

        selectionMenu.GetComponent<ShipSelection>().SetShip(player);
    }
}

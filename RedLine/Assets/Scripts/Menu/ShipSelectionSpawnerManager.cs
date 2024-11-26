using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MenuManagement;

public class ShipSelectionSpawnerManager : MonoBehaviour
{
    public GameObject onePrefab;
    public GameObject twoPrefab;
    public GameObject threeAndFourPrefab;

    private void SpawnSelectionScreens(int number, GameObject prefab)
    {
        GameManager.gManager.players[number].GetComponent<SelectionScreenSpawn>().SpawnShipSelection(prefab, number);
    }

    public void ReOrderShipSelection()
    {
        GameManager.gManager.uiCInput.shipSelectionMenu.menuStartButtons.Clear();   
        int index = 0;
        foreach(GameObject playerOBJ in GameManager.gManager.players)
        {
            if (playerOBJ.GetComponent<PlayerInputScript>().GetShipSelection() != null)
            {
                Destroy(playerOBJ.GetComponent<PlayerInputScript>().GetShipSelection().gameObject);

                playerOBJ.GetComponent<PlayerInputScript>().SetSelection(null);
            }

            switch (GameManager.gManager.players.Count)
            {
                case 1:
                    SpawnSelectionScreens(index, onePrefab);
                    break;
                case 2:
                    SpawnSelectionScreens(index, twoPrefab);
                    break;
                case 3:
                    SpawnSelectionScreens(index, threeAndFourPrefab);
                    break;
                case 4:
                    SpawnSelectionScreens(index, threeAndFourPrefab);
                    break;
            }

            index++;
        }
    }
}

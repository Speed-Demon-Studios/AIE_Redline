using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseButtonFirst;

    // Start is called before the first frame update
    public void Inistialize()
    {
        GameManager.gManager.pMenu = this;
    }

    public void SwitchPlayerOneButton()
    {
        GameManager.gManager.players[0].GetComponent<ActionMappingControl>().mES.SetSelectedGameObject(pauseButtonFirst);
    }

    public void StartTimeAgain(bool switchs)
    {
        GameManager.gManager.StartTime(switchs);
    }

    public void QuitToMenu()
    {
        GameManager.gManager.mSL.InitializeForMainMenu();
    }
}

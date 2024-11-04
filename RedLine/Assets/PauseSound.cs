using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSound : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.gManager.rAC.GamePauseSound();
    }

    public void PlayConfirmSound()
    {
        GameManager.gManager.rAC.MenuConfirmSound();
    }
}

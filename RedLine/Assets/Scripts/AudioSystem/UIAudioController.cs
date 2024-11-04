using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAudioController : MonoBehaviour
{
    public FMODUnity.StudioEventEmitter joinEmitter;
    public FMODUnity.StudioEventEmitter confirmEmitter;
    public FMODUnity.StudioEventEmitter pauseEmitter;
    public FMODUnity.StudioEventEmitter cancelEmitter;
    public List<FMODUnity.EventReference> menuAudio = new List<FMODUnity.EventReference>();

    public void GamePauseSound()
    {
        pauseEmitter.EventReference = menuAudio[0];
        pauseEmitter.Play();
    }

    public void PlayerJoinSound()
    {
        joinEmitter.EventReference = menuAudio[1];
        joinEmitter.Play();
    }

    public void MenuConfirmSound()
    {
        confirmEmitter.EventReference = menuAudio[2];
        confirmEmitter.Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIAudioController : MonoBehaviour
{
    public FMODUnity.StudioEventEmitter joinEmitter;
    public FMODUnity.StudioEventEmitter confirmEmitter;
    public FMODUnity.StudioEventEmitter pauseEmitter;
    public FMODUnity.StudioEventEmitter cancelEmitter;
    public List<FMODUnity.EventReference> menuAudio = new List<FMODUnity.EventReference>();
    public float[] pitchVariations;
    public float selectedVariation;
    public int selectedVariationIndex;
    public float pitchOUT;

    private void Start()
    {
        pauseEmitter.EventReference = menuAudio[0];
        joinEmitter.EventReference = menuAudio[1];
        confirmEmitter.EventReference = menuAudio[2];
    }

    public void GamePauseSound()
    {
        pauseEmitter.Play();
    }

    public void PlayerJoinSound()
    {
        joinEmitter.Play();
    }

    public void MenuConfirmSound()
    {
        confirmEmitter.EventInstance.getPitch(out pitchOUT);
        float pitchSelected = 0.0f;
        pitchSelected = Random.Range(0.0f, 25.0f);

        if (pitchSelected <= 5)
        {
            selectedVariation = pitchVariations[0];
        }
        else if (pitchSelected > 5 && pitchSelected <= 10)
        {
            selectedVariation = pitchVariations[1];
        }
        else if (pitchSelected > 10 && pitchSelected <= 15)
        {
            selectedVariation = pitchVariations[2];
        }
        else if (pitchSelected > 15 && pitchSelected <= 20)
        {
            selectedVariation = pitchVariations[3];
        }
        else if (pitchSelected > 20 && pitchSelected <= 25)
        {
            selectedVariation = pitchVariations[4];
        }
        confirmEmitter.EventInstance.getPitch(out pitchOUT);
        confirmEmitter.EventInstance.setPitch(selectedVariation);
        confirmEmitter.Play();
    }

    private void Update()
    {
        confirmEmitter.EventInstance.setPitch(selectedVariation);
    }
}

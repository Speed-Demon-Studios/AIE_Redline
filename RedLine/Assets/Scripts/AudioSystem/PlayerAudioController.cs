using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    public List<FMODUnity.StudioEventEmitter> eventEmitterRefs = new List<FMODUnity.StudioEventEmitter>();
    public float pitch = 1.0f;
    public float volume = 0.045f;

    private void OnEnable()
    {
    }

    public void StartEngineHum()
    {
        FMODUnity.StudioEventEmitter currentEmitter = eventEmitterRefs[0];

        currentEmitter.Play();
    }


    // Start is called before the first frame update
    void Start()
    {
        FMODUnity.StudioEventEmitter currentEmitter = eventEmitterRefs[0];

        Debug.Log("Event Path: " + currentEmitter.EventReference.Path);
        currentEmitter.EventReference = FMODUnity.EventReference.Find("event:/ShipEngineHum/EngineHum");
        Debug.Log("Event Path: " + currentEmitter.EventReference.Path);


    }

    // Update is called once per frame
    void Update()
    {
        FMODUnity.StudioEventEmitter engineEmitter = eventEmitterRefs[0];
        engineEmitter.EventInstance.setPitch(pitch);
        engineEmitter.EventInstance.setVolume(volume);
    }
}

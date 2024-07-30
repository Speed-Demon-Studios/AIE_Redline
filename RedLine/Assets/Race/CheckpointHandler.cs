using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class CheckpointHandler : MonoBehaviour
{

    public Transform GetCheckpoint(int index)
    {
        return transform.GetChild(index);
    }

    public int GetNextIndex(int current)
    {
        int nextIndex = current + 1;
        if (nextIndex >= transform.childCount)
        {
            return 0;
        }
        return nextIndex;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

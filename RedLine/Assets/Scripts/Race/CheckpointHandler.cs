using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointHandler : MonoBehaviour
{
    /// <summary>
    /// Return the position of the checkpoint at the given index.
    /// </summary>
    public Transform GetCheckpoint(int index)
    {
        return transform.GetChild(index);
    }

    private void Awake()
    {
        GameManager.gManager.checkpointParent = this;
    }


    /// <summary>
    /// Returns the next index.
    /// </summary>
    public int GetNextIndex(int current)
    {
        int nextIndex = current + 1;
        if (nextIndex >= transform.childCount)
        {
            return 0;
        }
        return nextIndex;
    }
}

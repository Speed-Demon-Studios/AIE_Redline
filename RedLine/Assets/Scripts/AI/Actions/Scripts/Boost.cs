using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Decisions;
using Unity.VisualScripting;

[CreateAssetMenu(fileName = "Boost", menuName = "Actions/Boost")]
public class Boost : Action
{

    public override void ExecuteAction(NPCController npc, ShipsControls controls)
    {
        controls.IsBoosting();
        Debug.Log("AI boosted");
        isFinished = true;
    }
}

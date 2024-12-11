using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.WSA;

public class TransitionManager : MonoBehaviour
{
    public Animator anim;

    public void Transition(string whichTransition)
    {
        anim.SetTrigger(whichTransition);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DifficultyButtonSwitch;
using UnityEngine.Events;

public class ButtonOnSelected : MonoBehaviour, ISelectHandler
{
    public UnityEvent onSelectEvent;


    public void OnSelect(BaseEventData eventData)
    {
        onSelectEvent.Invoke();
    }

}

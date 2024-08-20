using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RacerDetails : MonoBehaviour
{
    public int currentLap = 0;
    public int placement = 0;
    public int currentCheckpoint = 0;
    private int playerRacingActionMapIndex;
    public float distanceToCheckpoint;
    bool controlMapChanged = false;

    private CheckpointHandler m_cHandler;

    public UIControllerInput uiCInput;
    [SerializeField] private PlayerInput _playerInputActions;

    public void UiControllerUP()
    {
        if (GameManager.gManager.uiCInput != null)
        {
            GameManager.gManager.uiCInput.MenuUP();
        }
    }

    public void UiControllerDown()
    {
        if (GameManager.gManager.uiCInput != null)
        {
            GameManager.gManager.uiCInput.MenuDown();
        }
    }

    public void UiControllerConfirm()
    {
        if (GameManager.gManager.uiCInput != null)
        {
            GameManager.gManager.uiCInput.MenuConfirm();
        }
    }

    /// <summary>
    /// Calculates the distance to the next checkpoint
    /// </summary>
    public float NextCheckpointDistance()
    {
        m_cHandler = GameManager.gManager.checkpointParent;
        var nextCheckpoint = m_cHandler.GetCheckpoint(currentCheckpoint);
        distanceToCheckpoint = Vector3.Distance(transform.position, nextCheckpoint.transform.position);
        return distanceToCheckpoint;
    }
    public void SwitchActionMapToPlayer()
    {
        for (int i = 0; i < _playerInputActions.actions.actionMaps.Count; i++)
        {
            Debug.Log("Action Map: " + _playerInputActions.actions.actionMaps[i].name);
            if (_playerInputActions.actions.actionMaps[i].name.ToLower() == "player")
            {
                playerRacingActionMapIndex = i;
            }

        }

        //for (int i = 0; i < _playerInputActions.actions.actionMaps.Count; i++)
        //{
        //    _playerInputActions.actions.actionMaps[i].Disable();
        //}

        //_playerInputActions.actions.actionMaps[playerRacingActionMapIndex].Enable();
        //_playerInputActions.currentActionMap = _playerInputActions.actions.actionMaps[playerRacingActionMapIndex];
        _playerInputActions.actions.FindActionMap("Player").Enable();
        _playerInputActions.actions.FindActionMap("Menus").Disable();
        _playerInputActions.currentActionMap = _playerInputActions.actions.FindActionMap("Player");
        //_playerInputActions.SwitchCurrentActionMap("Player");

        Debug.Log("Current Action Map: " + _playerInputActions.currentActionMap.name);

    }

    private void Update()
    {
        if (GameManager.gManager.raceStarted == true)
        {
            PlayerInputScript playerInput = this.gameObject.GetComponent<PlayerInputScript>();
            if (controlMapChanged == false)
            {
                controlMapChanged = true;
                SwitchActionMapToPlayer();
            }
            if (playerInput.enabled == false)
            {
                playerInput.enabled = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.ToLower() == "checkpoint")
        {
            CheckpointHandler cHandler = GameManager.gManager.checkpointParent;
            if (other.transform == cHandler.GetCheckpoint(currentCheckpoint))
            {
                currentCheckpoint = cHandler.GetNextIndex(currentCheckpoint);

                if (other.TryGetComponent(out CheckpointTrigger trigger))
                {
                    if (trigger.finalCheckpoint == true)
                    {
                        GameManager.gManager.rManager.LapComplete(this);
                    }
                    else
                    {
                        return;
                    }
                }
            }
            else
            {
                return;
            }
        }
    }

}

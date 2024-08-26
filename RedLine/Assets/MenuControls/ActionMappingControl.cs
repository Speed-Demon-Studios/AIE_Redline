using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class ActionMappingControl : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInputActions;
    public MultiplayerEventSystem mES;
    private ShipsControls sControls;
    private RacerDetails racerInfo;
    private bool controlMapChanged = false;
    private int playerRacingActionMapIndex;

    public void SwitchActionMapToPlayer()
    {
        for (int i = 0; i < _playerInputActions.actions.actionMaps.Count; i++)
        {
            if (_playerInputActions.actions.actionMaps[i].name.ToLower() == "player")
            {
                playerRacingActionMapIndex = i;
            }
        }
        _playerInputActions.actions.FindActionMap("Player").Enable();
        _playerInputActions.currentActionMap = _playerInputActions.actions.FindActionMap("Player");
    }

    public void SwitchActionMapToUI()
    {
        for (int i = 0; i < _playerInputActions.actions.actionMaps.Count; i++)
        {
            if (_playerInputActions.actions.actionMaps[i].name.ToLower() == "UI")
            {
                playerRacingActionMapIndex = i;
            }
        }
        _playerInputActions.actions.FindActionMap("UI").Enable();
        _playerInputActions.currentActionMap = _playerInputActions.actions.FindActionMap("UI");

        Debug.Log("Current ActionMap: " + _playerInputActions.currentActionMap.name);
    }

    // Start is called before the first frame update
    void Start()
    {
        racerInfo = this.GetComponent<RacerDetails>();
        sControls = this.gameObject.GetComponent<ShipsControls>();
    }

    // Update is called once per frame
    void Update()
    {
        if (racerInfo.finishedRacing == true)
        {
            //if (sControls != null)
            //{
            //_playerInputActions.DeactivateInput();
            sControls.enabled = false;
            Debug.Log("Disabling ShipControls Script.");
            if ( _playerInputActions != null && _playerInputActions.currentActionMap.name != "UI")
            {
                SwitchActionMapToUI();
            }
            //}
        }
        if (GameManager.gManager.raceStarted == true && racerInfo.finishedRacing == false)
        {

            PlayerInputScript playerInput = this.gameObject.GetComponent<PlayerInputScript>();
            AIMoveInputs aiInput = this.gameObject.GetComponent<AIMoveInputs>();
            if (racerInfo.finishedRacing == false)
            {
                if (playerInput != null)
                {
                    if (controlMapChanged == false && _playerInputActions != null)
                    {
                        controlMapChanged = true;
                        SwitchActionMapToPlayer();
                    }
                    if (playerInput.enabled == false)
                    {
                        playerInput.enabled = true;
                    }
                }
                if (aiInput != null)
                {
                    if (aiInput.enabled == false)
                    {
                        aiInput.enabled = true;
                    }
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActionMappingControl : MonoBehaviour
{
    private RacerDetails racerInfo;
    private bool controlMapChanged = false;
    private int playerRacingActionMapIndex;
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
        _playerInputActions.actions.FindActionMap("Menus").Disable();
        _playerInputActions.currentActionMap = _playerInputActions.actions.FindActionMap("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        racerInfo = this.GetComponent<RacerDetails>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gManager.raceStarted == true)
        {
            if (racerInfo.finishedRacing == true)
            {
                _playerInputActions.DeactivateInput();
            }

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

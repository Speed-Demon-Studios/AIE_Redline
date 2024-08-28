using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ToggleOnPlayerJoin : MonoBehaviour
{
    private PlayerInputManager m_playerInputManager;

    private void Awake()
    {
        m_playerInputManager = FindObjectOfType<PlayerInputManager>();
    }

    private void OnEnable()
    {
        m_playerInputManager.onPlayerJoined += ToggleThis;
    }

    private void OnDisable()
    {
        m_playerInputManager.onPlayerJoined -= ToggleThis;
    }

    public void ToggleThis(PlayerInput player)
    {
        this.gameObject.SetActive(false);
    }
}

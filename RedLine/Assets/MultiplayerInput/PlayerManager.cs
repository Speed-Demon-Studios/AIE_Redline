using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    private PlayerInputManager m_playerInputManager;
    private List<PlayerInput> m_players = new List<PlayerInput>();
    [SerializeField] private List<Transform> m_startingPoints = new List<Transform>();
    [SerializeField] private List<LayerMask> m_playerLayers = new List<LayerMask>();

    private void Awake()
    {
        m_playerInputManager = FindObjectOfType<PlayerInputManager>();
    }

    private void OnEnable()
    {
        m_playerInputManager.onPlayerJoined += AddPlayer;
    }

    private void OnDisable()
    {
        m_playerInputManager.onPlayerJoined -= AddPlayer;
    }

    public void AddPlayer(PlayerInput player)
    {
        m_players.Add(player);

        Transform playerParent = player.transform.parent;
        if (m_startingPoints[m_players.Count - 1] != null)
        {
            playerParent.transform.position = m_startingPoints[0].transform.position;
        }

        int layerToAdd = (int)Mathf.Log(m_playerLayers[m_players.Count - 1].value, 2);

        playerParent.GetComponentInChildren<Camera>().cullingMask |= 1 << layerToAdd;
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

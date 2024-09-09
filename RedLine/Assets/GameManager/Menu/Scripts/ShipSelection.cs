using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipSelection : MonoBehaviour
{
    public List<GameObject> ships = new();
    public List<ShipVariant> variants = new();
    private GameObject m_currentShips;
    private int m_shipIndex;
    public GameObject cam;
    private float m_y;
    public RenderTexture texture;
    public RawImage image;
    private GameObject m_ship;
    public int playerNum;
    public void SetShip(GameObject ship) { m_ship = ship; }

    private void Start()
    {
        m_currentShips = ships[0];
        cam.GetComponentInChildren<Camera>().targetTexture = texture;
        image.texture = texture;
    }

    private void Update()
    {
        m_y += 5f * Time.deltaTime;
        cam.transform.rotation = Quaternion.Euler(0, m_y, 0);
    }

    public void OnNext()
    {
        m_currentShips.SetActive(false);
        m_shipIndex += 1;
        if(m_shipIndex > ships.Count - 1)
        {
            m_shipIndex = 0;
        }
        m_currentShips = ships[m_shipIndex];
        m_currentShips.SetActive(true);
    }

    public void OnPrev()
    {
        m_currentShips.SetActive(false);
        m_shipIndex -= 1;
        if (m_shipIndex < 0)
        {
            m_shipIndex = ships.Count - 1;
        }
        m_currentShips = ships[m_shipIndex];
        m_currentShips.SetActive(true);
    }

    public void Ready()
    {
        m_ship.GetComponent<ShipsControls>().VariantObject = variants[m_shipIndex];
        m_ship.GetComponent<ShipsControls>().variant = variants[m_shipIndex];
        m_ship.GetComponent<ShipsControls>().enabled = true;
        FindObjectOfType<UIControllerInput>().ReadyPlayer(playerNum);
    }
}

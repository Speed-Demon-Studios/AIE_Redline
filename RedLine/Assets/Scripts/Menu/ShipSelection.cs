using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public TextMeshProUGUI shipName;
    public List<Slider> sliders;
    public Color readyColor;
    public Color notReady;
    public GameObject border;
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
        StopAllCoroutines();
        StartCoroutine(NameChange(variants[m_shipIndex].VariantName));
        sliders[0].value = variants[m_shipIndex].DefaultMaxSpeed;
        sliders[1].value = variants[m_shipIndex].TurnSpeed;
        sliders[2].value = variants[m_shipIndex].DefaultMaxAcceleration;
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
        StopAllCoroutines();
        StartCoroutine(NameChange(variants[m_shipIndex].VariantName));
        sliders[0].value = variants[m_shipIndex].DefaultMaxSpeed;
        sliders[1].value = variants[m_shipIndex].TurnSpeed;
        sliders[2].value = variants[m_shipIndex].DefaultMaxAcceleration;
    }

    public void Ready()
    {
        m_ship.GetComponent<ShipsControls>().VariantObject = variants[m_shipIndex];
        m_ship.GetComponent<ShipsControls>().variant = variants[m_shipIndex];
        m_ship.GetComponent<ShipsControls>().enabled = true;
        FindObjectOfType<UIControllerInput>().ReadyPlayer(playerNum);
        if(m_ship.GetComponent<ShipBlendAnimations>())
            m_ship.GetComponent<ShipBlendAnimations>().enabled = true;
        border.GetComponent<RawImage>().color = readyColor;

    }

    IEnumerator NameChange(string shipName)
    {
        string aToZ = "abcdefghijklmnopqrstuvwxyz";

        int stringLength = shipName.Length;

        string tempName = shipName;

        for (int j = 0; j < stringLength; j++)
        {
            yield return new WaitForSeconds(0.005f);

            char randomLetter = aToZ[Random.Range(0, 24)];

            tempName = tempName.Remove(j, 1);
            tempName = tempName.Insert(j, randomLetter.ToString());

            this.shipName.text = tempName;
        }

        for (int i = 0; i < stringLength; i++)
        {
            tempName = tempName.Remove(i, 1);
            tempName = tempName.Insert(i, shipName.ToCharArray()[i].ToString());

            for (int j = i + 1; j < stringLength; j++)
            {
                yield return new WaitForSeconds(0.001f);

                char randomLetter = aToZ[Random.Range(0, 24)];

                tempName = tempName.Remove(j, 1);
                tempName = tempName.Insert(j, randomLetter.ToString());
            }
            this.shipName.text = tempName;
            yield return new WaitForSeconds(0.008f);

        }
    }
}

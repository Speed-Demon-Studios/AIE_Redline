using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestingShips
{
    public class Testing : MonoBehaviour
    {
        public GameObject playerPref;
        [SerializeField] GameObject m_player;
        public ShipVariant m_variant;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SpawnShip()
        {
            m_player = Instantiate(playerPref);

            m_player.GetComponent<ShipsControls>().enabled = true;
            m_player.GetComponent<ShipsControls>().isTestShip = true;

            m_player.GetComponent<ActionMappingControl>().UpdateActionMapForRace();

            m_player.GetComponent<PlayerInputScript>().enabled = true;
            m_player.GetComponent<PlayerInputScript>().m_cam.gameObject.SetActive(true);

            m_player.GetComponent<ShipsControls>().VariantObject = m_variant;
            m_player.GetComponent<ShipsControls>().AttachModels();
        }

        public void SwitchModel()
        {
            m_player.GetComponent<ShipsControls>().VariantObject = m_variant;
            m_player.GetComponent<ShipsControls>().AttachModels();
        }
    }
}

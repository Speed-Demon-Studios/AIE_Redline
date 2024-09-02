using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CamManager : MonoBehaviour
{
    public List<GameObject> tracks = new();
    private int m_trackIndex;
    private GameObject m_currentTrack;

    private float m_trackPos = 0;
    // Start is called before the first frame update
    void Start()
    {
        m_currentTrack = tracks[m_trackIndex];
        m_trackPos = 0;
    }

    // Update is called once per frame
    void Update()
    {
        m_trackPos += 0.1f * Time.deltaTime;
        if(m_trackPos > 1)
        {
            m_currentTrack.gameObject.SetActive(false);
            m_trackIndex += 1;
            if(m_trackIndex > tracks.Count - 1)
            {
                m_trackIndex = 0;
            }
            m_trackPos = 0;
            m_currentTrack = tracks[m_trackIndex];
            m_currentTrack.gameObject.SetActive(true);
        }

        m_currentTrack.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = m_trackPos;
    }
}

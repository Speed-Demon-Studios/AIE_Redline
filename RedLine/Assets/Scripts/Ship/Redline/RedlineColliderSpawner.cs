using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RedlineColliderSpawner : MonoBehaviour
{
    List<GameObject> m_lineColliders = new();
    private int childIndex;

    public GameObject colliderPrefab;
    public Transform spawnPoint;
    public GameObject colliderParent;

    void OnEnable()
    {
        for (int i = 0; i < 35; i++)
        {
            SpawnCollider();
        }
    }

    public void ClearList()
    {
        m_lineColliders.Clear();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //AddBoost();
        if(!GameManager.gManager.raceFinished)
            ChangePositions();
        //CheckShipsInLine();
    }

    //private void AddBoost()
    //{
    //    for (int i = 0; i < m_shipsInColliders.Count; i++)
    //    {
    //        if (m_shipsInColliders[i] != transform.parent.gameObject)
    //        {
    //
    //            ShipsControls outTest;
    //            if (m_shipsInColliders[i].TryGetComponent<ShipsControls>(out outTest))
    //            {
    //                Debug.Log(outTest.gameObject + "Add to boosting");
    //                outTest.GetComponent<ShipsControls>().SwitchRedlineBool(true);
    //                outTest.AddToBoost();
    //            }
    //        }
    //    }
    //}

    //public void CheckShipsInLine()
    //{
    //    for (int i = 0; i < m_shipsInColliders.Count; i++)
    //    {
    //        if (!m_allShipsInColliders.Contains(m_shipsInColliders[i]))
    //        {
    //            m_shipsInColliders[i].GetComponent<ShipsControls>().SwitchRedlineBool(false);
    //            m_shipsInColliders.Remove(m_shipsInColliders[i]);
    //        }
    //    }
    //    for (int i = 0; i < m_allShipsInColliders.Count; i++)
    //    {
    //        if (!m_shipsInColliders.Contains(m_allShipsInColliders[i]))
    //        {
    //            m_shipsInColliders.Add(m_allShipsInColliders[i]);
    //        }
    //
    //    }
    //}

    private void ChangePositions()
    {
        if (childIndex < m_lineColliders.Count)
        {
            m_lineColliders[childIndex].gameObject.transform.position = spawnPoint.transform.position;
            childIndex += 1;
            if (childIndex > m_lineColliders.Count - 1)
                childIndex = 0;
        }
    }

    public void SpawnCollider()
    {
        GameObject a = Instantiate(colliderPrefab, spawnPoint.position, Quaternion.Euler(Vector3.zero), colliderParent.transform.parent.transform);
        a.transform.parent = null;

        m_lineColliders.Add(a);
    }
}

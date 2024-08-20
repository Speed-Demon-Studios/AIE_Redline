using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RedlineColliderSpawner : MonoBehaviour
{
    List<GameObject> m_lineColliders = new();
    private List<GameObject> m_shipsInColliders = new();
    public List<GameObject> m_allShipsInColliders;
    private int childIndex;

    public GameObject colliderPrefab;
    public Transform spawnPoint;
    public GameObject colliderParent;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 35; i++)
            SpawnCollider();
    }

    // Update is called once per frame
    void Update()
    {
        ChangePositions();
        CheckShipsInLine();
        AddBoost();
    }

    private void AddBoost()
    {
        for (int i = 0; i < m_shipsInColliders.Count; i++)
        {
            ShipsControls controls = m_shipsInColliders[i].GetComponent<ShipsControls>();

            Debug.Log(m_shipsInColliders[i].gameObject + "Add to boosting");

            controls.AddToBoost();
        }
    }

    public void CheckShipsInLine()
    {
        for (int i = 0; i < m_shipsInColliders.Count; i++)
        {
            if (m_allShipsInColliders.Contains(m_shipsInColliders[i]))
                return;
            else
            {
                m_shipsInColliders.Remove(m_shipsInColliders[i]);
            }
        }
        for (int i = 0; i < m_allShipsInColliders.Count; i++)
        {
            if (m_shipsInColliders.Contains(m_allShipsInColliders[i]))
                return;
            else
            {
                m_shipsInColliders.Add(m_allShipsInColliders[i]);
            }
        }
    }

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

            ColliderTrigger b = a.GetComponent<ColliderTrigger>();
            b.spawner = this;

            m_lineColliders.Add(a);
    }
}

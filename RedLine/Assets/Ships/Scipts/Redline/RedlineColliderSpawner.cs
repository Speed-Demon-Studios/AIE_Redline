using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RedlineColliderSpawner : MonoBehaviour
{
    List<GameObject> m_lineColliders = new();
    public List<GameObject> m_shipsInColliders = new();
    public List<GameObject> ships = new();
    private int childIndex;

    public GameObject colliderPrefab;
    public Transform spawnPoint;
    public GameObject colliderParent;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        SpawnCollider();
        CheckList();
        CheckShipsInLine();
    }

    public void CheckShipsInLine()
    {
        for (int i = 0; i < m_shipsInColliders.Count; i++)
        {
            if (ships.Contains(m_shipsInColliders[i]))
                return;
            else
            {
                m_shipsInColliders.Remove(m_shipsInColliders[i]);
            }
        }
        for (int i = 0; i < ships.Count; i++)
        {
            if (m_shipsInColliders.Contains(ships[i]))
                return;
            else
            {
                m_shipsInColliders.Add(ships[i]);
            }
        }

        for(int i = 0; i < m_shipsInColliders.Count; i++)
        {
            ShipsControls controls = m_shipsInColliders[i].GetComponent<ShipsControls>();

            controls.AddToBoost();
        }
    }

    private void CheckList()
    {
        m_lineColliders[childIndex].gameObject.transform.position = spawnPoint.transform.position;
        childIndex += 1;
        if (childIndex > m_lineColliders.Count - 1)
            childIndex = 0;
    }

    public void SpawnCollider()
    {
        if (m_lineColliders.Count < 50)
        {
            GameObject a = Instantiate(colliderPrefab, spawnPoint.position, Quaternion.Euler(Vector3.zero), colliderParent.transform);

            ColliderTrigger b = a.GetComponent<ColliderTrigger>();
            b.spawner = this;

            m_lineColliders.Add(a);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedlineMechanic : MonoBehaviour
{
    List<GameObject> lineColliders = new();

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
    }



    private void CheckList()
    {
        if(lineColliders.Count > 50)
        {
            GameObject a = lineColliders[0];

            lineColliders.RemoveAt(0);

            Destroy(a);
        }
    }

    public void SpawnCollider()
    {
        GameObject a = Instantiate(colliderPrefab, spawnPoint.position, Quaternion.Euler(Vector3.zero), colliderParent.transform);

        lineColliders.Add(a);
    }
}

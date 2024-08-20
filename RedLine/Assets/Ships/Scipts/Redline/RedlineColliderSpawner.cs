using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedlineColliderSpawner : MonoBehaviour
{
    public LineRenderer trail;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GenerateMeshCollider();
    }

    public void GenerateMeshCollider()
    {
        MeshCollider collider = GetComponent<MeshCollider>();

        if(collider == null)
            collider = gameObject.AddComponent<MeshCollider>();

        Mesh mesh = new();

        trail.BakeMesh(mesh, true);
        collider.sharedMesh = mesh;
    }

}

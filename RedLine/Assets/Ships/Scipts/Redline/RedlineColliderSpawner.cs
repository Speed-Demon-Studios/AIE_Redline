using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedlineColliderSpawner : MonoBehaviour
{
    public TrailRenderer trailLine;
    MeshCollider m_lineCollider = new();
    public bool start;
    // Start is called before the first frame update
    void Start()
    {
        m_lineCollider = GetComponent<MeshCollider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GenerateMeshCollider();
    }

    public void GenerateMeshCollider()
    {
        if(m_lineCollider == null)
            m_lineCollider = gameObject.AddComponent<MeshCollider>();

        Mesh mesh = new();

        trailLine.BakeMesh(mesh, true);

        m_lineCollider.sharedMesh = mesh;
    }

}

using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodes : MonoBehaviour
{
    Vector3 m_nodesPos;
    public Vector3 ReturnNodePos() { return m_nodesPos; }
    public float radius;

    private void Start()
    {
        m_nodesPos = transform.position;
        RandomPos();
    }
    public void RandomPos()
    {
        Vector3 returnPos = Random.insideUnitCircle * radius;

        m_nodesPos = returnPos;
    }
}

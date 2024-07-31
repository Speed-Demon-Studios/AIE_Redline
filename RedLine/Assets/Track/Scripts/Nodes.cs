using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Nodes : MonoBehaviour
{
    public Vector3 ReturnNodePos() { return transform.position; }

    public float radius;

    private void Start()
    {
    }

    public Vector3 RandomNavSphere(Vector3 origin)
    {
        Vector3 randDirection = Random.insideUnitSphere * radius;

        randDirection += origin;

        return randDirection;
    }
}

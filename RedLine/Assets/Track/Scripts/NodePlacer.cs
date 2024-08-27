using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Dreamteck.Splines;

public class NodePlacer : MonoBehaviour
{
    public Nodes prefabToSpawn;
    public SplineFollower follower;
    private Nodes prevNode;

    public void SpawnNode()
    {
        //if(prefabToSpawn == null)
        //{
        //    EditorUtility.DisplayDialog("Error", "No prefab to spawn", "OK");
        //    return;
        //}
        //
        //Nodes spawnNode = PrefabUtility.InstantiatePrefab(prefabToSpawn) as Nodes;
        //
        //spawnNode.gameObject.transform.localPosition = this.gameObject.transform.localPosition;
        //spawnNode.gameObject.transform.localRotation = this.gameObject.transform.localRotation;
        //
        //if(prevNode != null)
        //{
        //    prevNode.nextNode = spawnNode;
        //}
        //
        //prevNode = spawnNode;

    }
}

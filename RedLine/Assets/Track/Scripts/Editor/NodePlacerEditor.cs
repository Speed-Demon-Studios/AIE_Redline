using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NodePlacer))]
public class NodePlacerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        NodePlacer nodePlacer = (NodePlacer)target;

        nodePlacer.prefabToSpawn = EditorGUILayout.ObjectField("Prefab", nodePlacer.prefabToSpawn, typeof(Nodes), false) as Nodes;

        //nodePlacer.distance = EditorGUILayout.FloatField("distance", nodePlacer.distance);

        if (GUILayout.Button("Spawn Node"))
        {
            nodePlacer.SpawnNode();
        }

        //if (GUILayout.Button("Reset node position to 1"))
        //{
        //    nodePlacer.ResetPos();
        //}
        //
        //if(GUILayout.Button("Move Spline"))
        //{
        //    nodePlacer.MoveNodeOnSpline();
        //}
    }
}

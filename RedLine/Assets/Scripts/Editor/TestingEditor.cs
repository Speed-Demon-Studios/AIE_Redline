using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TestingShips;
using UnityEditor;

[CustomEditor(typeof(Testing))]
public class TestingEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();

        Testing testingScript = (Testing)target;

        GameObject ship = EditorGUILayout.ObjectField("Player Ship", testingScript.playerPref, typeof(GameObject), false) as GameObject;

        ShipVariant variant = EditorGUILayout.ObjectField("Player Variant", testingScript.m_variant, typeof(ShipVariant), false) as ShipVariant;

        if (GUILayout.Button("Spawn Ship"))
        {
            testingScript.SpawnShip();
        }

        if (GUILayout.Button("Switch Variant"))
        {
            testingScript.SwitchModel();
        }

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(testingScript, "Changed Script");

            testingScript.playerPref = ship;
            testingScript.m_variant = variant;
        }
    }
}

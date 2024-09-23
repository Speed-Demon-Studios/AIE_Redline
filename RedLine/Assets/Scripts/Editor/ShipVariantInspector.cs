using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ShipVariant))]
public class ShipVariantInspector : Editor
{
    public override void OnInspectorGUI()
    {
        ShipVariant variant = (ShipVariant)target;

        EditorGUILayout.LabelField("Ship Name");
        EditorGUILayout.TextField(variant.VariantName, GUILayout.Width(150), GUILayout.Height(25));

        GUILayout.Space(15f);

        EditorGUILayout.LabelField("Down Force");

        GUILayout.BeginHorizontal();
        variant.DownForce = EditorGUILayout.FloatField(variant.DefaultMaxAcceleration, GUILayout.Width(80), GUILayout.Height(25));
        GUILayout.EndHorizontal();

        GUILayout.Space(15f);

        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Model", GUILayout.Width(50));

        GUILayout.Space(145f);

        EditorGUILayout.LabelField("Collison", GUILayout.Width(50));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        variant.model = (GameObject)EditorGUILayout.ObjectField(variant.model, typeof(GameObject), true, GUILayout.Width(150), GUILayout.Height(25));

        GUILayout.Space(45f);

        variant.collision = (GameObject)EditorGUILayout.ObjectField(variant.collision, typeof(GameObject), true, GUILayout.Width(150), GUILayout.Height(25));
        GUILayout.EndHorizontal();

        GUILayout.Space(15f);

        EditorGUILayout.LabelField("Speed Stats");

        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Default Max Acceleration", GUILayout.Width(160));
        EditorGUILayout.LabelField("Max Acceleration", GUILayout.Width(180));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        variant.DefaultMaxAcceleration = EditorGUILayout.FloatField(variant.DefaultMaxAcceleration, GUILayout.Width(80), GUILayout.Height(25));
        GUILayout.Space(80f);
        variant.MaxAcceleration = EditorGUILayout.FloatField(variant.MaxAcceleration, GUILayout.Width(80), GUILayout.Height(25));
        GUILayout.EndHorizontal();
        EditorGUILayout.Space();

        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Default Max Speed", GUILayout.Width(125));
        EditorGUILayout.LabelField("Acceleration Multiplier", GUILayout.Width(145));
        EditorGUILayout.LabelField("Break Multiplier", GUILayout.Width(130));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        variant.DefaultMaxSpeed = EditorGUILayout.FloatField(variant.DefaultMaxSpeed, GUILayout.Width(100), GUILayout.Height(25));
        GUILayout.Space(35f);
        variant.AccelerationMultiplier = EditorGUILayout.FloatField(variant.AccelerationMultiplier, GUILayout.Width(100), GUILayout.Height(25));
        GUILayout.Space(35f);
        variant.BreakMultiplier = EditorGUILayout.FloatField(variant.BreakMultiplier, GUILayout.Width(100), GUILayout.Height(25));
        GUILayout.EndHorizontal();

        EditorGUILayout.LabelField("Speed Curve", GUILayout.Width(125));

        variant.SpeedCurve = EditorGUILayout.CurveField(variant.SpeedCurve, GUILayout.Height(80));

        GUILayout.Space(5f);

        EditorGUILayout.LabelField("Turn Speed", GUILayout.Width(125));

        variant.TurnSpeed = EditorGUILayout.FloatField(variant.TurnSpeed, GUILayout.Width(100), GUILayout.Height(25));

        EditorGUILayout.LabelField("Turn Speed Curve", GUILayout.Width(125));

        variant.TurnSpeedCurve = EditorGUILayout.CurveField(variant.TurnSpeedCurve, GUILayout.Height(80));

        GUILayout.Space(10f);

        EditorGUILayout.LabelField("AI Stats");

        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Distance", GUILayout.Width(125));
        EditorGUILayout.LabelField("Turn Multiplier", GUILayout.Width(125));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        variant.distance = EditorGUILayout.FloatField(variant.distance, GUILayout.Width(100), GUILayout.Height(25));
        GUILayout.Space(35f);
        variant.turnMultiplier = EditorGUILayout.FloatField(variant.turnMultiplier, GUILayout.Width(100), GUILayout.Height(25));
        GUILayout.EndHorizontal();

        EditorGUILayout.LabelField("Needed Speed Curve", GUILayout.Width(125));

        variant.NeededSpeedCurve = EditorGUILayout.CurveField(variant.NeededSpeedCurve, GUILayout.Height(80));
    }
}

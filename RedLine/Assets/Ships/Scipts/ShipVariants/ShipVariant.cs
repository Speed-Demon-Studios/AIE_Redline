using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShipVariant", menuName = "Ship Variants/Ship Variant", order = 1)]
public class ShipVariant : ScriptableObject
{
    public string VariantName;
    public float DownForce;
    [Space]
    [Header("Speed Variables")]
    public float MaxSpeed;
    public float MaxAcceleration;
    public float AccelerationMultiplier;
    public float BreakMultiplier;
    public AnimationCurve SpeedCurve;
    [Space]
    [Header("Turning Variables")]
    public float TurnSpeed;
    public AnimationCurve TurnSpeedCurve;
    [Space]
    [Header("Ai Movement Variables")]
    public AnimationCurve NeededSpeedCurve;
    public float MaxAngle;
    public float Radius;
    public float distance;
}

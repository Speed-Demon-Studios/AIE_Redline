using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShipVariant", menuName = "Ship Variants/Player Ship Variant", order = 1)]
public class ShipVariant : ScriptableObject
{
    public string VariantName;
    [Space]
    public float DownForce;
    [Space]
    [Header("Speed Variables")]
    public float MaxSpeed;
    public float MaxAcceleration;
    public float AccelerationMultiplier;
    public float BreakMultiplier;
    public AnimationCurve SpeedCurve;

    [Header("Turning Variables")]
    public float TurnSpeed;
    public AnimationCurve TurnSpeedCurve;

    [Header("Ai Movement Variables")]
    public AnimationCurve neededSpeedCurve;
    
}

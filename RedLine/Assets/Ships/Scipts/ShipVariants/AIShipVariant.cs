using UnityEngine;

[CreateAssetMenu(fileName = "ShipVariant", menuName = "Ship Variants/Ai Ship Variant", order = 2)]
public class AIShipVariant : ScriptableObject
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
    [Header("Ai Move Variables")]
    public AnimationCurve NeededSpeedCurve;
    public float MaxAngle;
    public float Radius;
    public float distance;
}

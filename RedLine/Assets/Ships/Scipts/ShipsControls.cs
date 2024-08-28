using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ShipsControls : MonoBehaviour
{
    [Header("Refrences")]
    [HideInInspector] public ShipVariant variant;
    public ShipVariant VariantObject;
    public Rigidbody ReturnRB() { return m_rb; }
    public Transform rotation;
    public Transform facingPoint;
    public GameObject shipModel;
    public Transform rayCastPoint;
    private Rigidbody m_rb;

    [Space]
    [Header("Speed Variables")]
    private float m_accelerateMultiplier;
    private float m_brakeMultiplier;
    private float m_acceleration;
    private float m_currentMaxSpeed;
    public void SetMaxSpeed(float speed) { m_currentMaxSpeed = speed; }
    public float GetMaxSpeed() { return m_currentMaxSpeed; }

    [Header("Turning Varibles")]
    private float m_targetAngle;
    private float m_currentAngle;
    private float m_shipAngle;
    private float m_strafeMultiplier;
    public float strafeStrength;

    [Header("TrackStick")]
    private Vector3 m_targetPos;
    private Vector3 m_currentPos;

    [Header("Boost Variables")]
    [SerializeField] private float m_currentBoost;
    public bool wantingToBoost;
    private bool m_isBoosting;
    public float forceMultiplier;
    [SerializeField, Range(0,3)] private int m_boostLevel;

    public void ResetAcceleration()
    {
        wantingToBoost = false;
        m_boostLevel = 0;
        m_acceleration = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        if (VariantObject != null)
        {
            variant = new ShipVariant();
            variant = Instantiate<ShipVariant>(VariantObject);
        }
        m_currentMaxSpeed = variant.DefaultMaxSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Strafe();
        Turn();
        Brake();
        Accelerate();
        Boost();
        DownForce();
        RotateShip();
    }

    /// <summary>
    /// Adding to the current boost when in a red line and changing the level of boost the character is at
    /// </summary>
    public void AddToBoost()
    {
        m_currentBoost += 2.5f * Time.deltaTime;
        if(m_currentBoost > 3)
        {
            m_currentBoost = 3;
        }
        switch (m_currentBoost)
        {
            case < 1:
                m_boostLevel = 0;
                break;
            case < 2:
                m_boostLevel = 1;
                break;
            case < 3:
                m_boostLevel = 2;
                break;
            case < 4:
                m_boostLevel = 3;
                break;
        }
    }

    /// <summary>
    /// Rotates the ship after the calculations
    /// </summary>
    private void RotateShip()
    {
        // this is similar to the ship turn lerp but its for the ship model to swing from side to side depending on which direction you are turning
        m_shipAngle = Mathf.Lerp(m_shipAngle, (m_currentAngle * 2f) * Mathf.Rad2Deg, 0.03f);

        // first it will look at facing position which in the empty object infront of the ship
        transform.LookAt(facingPoint, transform.up);

        // Rotate the ship to the normal of the track
        transform.rotation = Quaternion.FromToRotation(transform.up, m_currentPos) * transform.rotation;
    }

    /// <summary>
    /// Downforce keeps the car perpendicular to the track as well as add downforce so it stays on the track
    /// </summary>
    private void DownForce()
    {
        // raycasting to find the track and its normal.
        // raycasts from a 
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -rayCastPoint.up, out hit))
        {
            if (hit.transform.tag == "Road")
            {
                m_targetPos = hit.normal;
            }
        }

        m_currentPos.x = Mathf.Lerp(m_currentPos.x, m_targetPos.x, 0.05f);
        m_currentPos.y = Mathf.Lerp(m_currentPos.y, m_targetPos.y, 0.05f);
        m_currentPos.z = Mathf.Lerp(m_currentPos.z, m_targetPos.z, 0.05f);

        if(hit.distance > 1)
            m_rb.AddForce(-transform.up * variant.DownForce, ForceMode.Force);
    }

    /// <summary>
    /// Boosting ship when boost button is pressed
    /// </summary>
    private void Boost()
    {
        if (wantingToBoost && m_boostLevel > 0)
        {
            m_rb.mass = 10;
            m_rb.AddForce(transform.forward * forceMultiplier, ForceMode.Impulse);
            StartCoroutine(BoostTime(0.5f * m_boostLevel));
        }
        else
            wantingToBoost = false;
    }

    public void BoostPadBoost(float force)
    {
        Debug.Log("Boost pad speed");
        m_rb.AddForce(transform.forward * force, ForceMode.VelocityChange);
    }

    private IEnumerator BoostTime(float length)
    {
        yield return new WaitForSeconds(length);
        m_boostLevel = 0;
        m_currentBoost = 0;
        m_rb.mass = 90;
        wantingToBoost = false;
    }

    /// <summary>
    /// This activates the brake when the multiplier is more than zero
    /// </summary>
    private void Brake()
    {
        m_acceleration -= variant.AccelerationMultiplier * m_brakeMultiplier * variant.BreakMultiplier * Time.deltaTime;
    }

    /// <summary>
    /// Accelerate is very simple. It basicly makes the car go foward when you press the accelerator and brake when you press the brake
    /// </summary>
    private void Accelerate()
    {
        float multiplier = variant.SpeedCurve.Evaluate(m_rb.velocity.magnitude / m_currentMaxSpeed);

        if (m_accelerateMultiplier == 0)
            m_acceleration -= (variant.AccelerationMultiplier * 0.4f) * Time.deltaTime;
        else
            m_acceleration += variant.AccelerationMultiplier * m_accelerateMultiplier * Time.deltaTime;

        m_acceleration = Mathf.Clamp(m_acceleration, 0, variant.MaxAcceleration);

        m_rb.velocity += m_acceleration * transform.forward * multiplier;
    }

    /// <summary>
    /// Turn sounds simple but it doesnt turn the ship. it rotates an position in front of the ship then that becomes the foward direction
    /// </summary>
    private void Turn()
    {
        // this script has a targetAngle which is where the empty position wants to be but to make it smooth it lerps the current pos to the target pos
        m_currentAngle = Mathf.Lerp(m_currentAngle, m_targetAngle, 0.06f);

        // this multiplier changes the turn angle based on how fast you are going. The faster you go the less you turn
        float multiplier = variant.TurnSpeedCurve.Evaluate(m_rb.velocity.magnitude / m_currentMaxSpeed);

        // this rotation is for the turning of the ship which only happens on the ships local y axis
        rotation.localRotation = Quaternion.Euler(new Vector3(0, m_currentAngle * (variant.TurnSpeed * multiplier), 0));

        // this uses the shipAngle lerp to rotate both on the y axis and the z axis
        shipModel.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -m_shipAngle * 0.8f));
    }

    private void Strafe()
    {
        m_rb.AddForce(transform.right * m_strafeMultiplier * strafeStrength, ForceMode.VelocityChange);
    }

    /// <summary>
    /// both of these functions set the multipler when an input is sent
    /// </summary>
    /// <param name="multiplier"></param>
    public void SetSpeedMultiplier(float multiplier) { m_accelerateMultiplier = multiplier; }
    public void SetBrakeMultiplier(float multiplier) { m_brakeMultiplier = multiplier; }
    public void SetTurnMultipliers(float multiplier) { m_targetAngle = multiplier + (m_strafeMultiplier * 0.45f); }
    public void SetStrafeMultiplier(float multiplier) { m_strafeMultiplier = multiplier; SetTurnMultipliers(0); }
    public void IsBoosting(bool boosting) { wantingToBoost = boosting; }
}

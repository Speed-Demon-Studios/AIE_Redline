using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipsControls : MonoBehaviour
{
    [Header("Refrences")]
    [HideInInspector] public ShipVariant variant;
    public ShipVariant VariantObject;
    public Rigidbody ReturnRB() { return m_rb; }
    public Transform rotation;
    public Transform facingPoint;
    public Transform collisionParent;
    public GameObject shipModel;
    public Transform rayCastPoint;
    private Rigidbody m_rb;
    private List<GameObject> m_fire = new();
    private int m_fireIndex;

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
    private float m_turningAngle;

    public float GetTurnMultiplier() { return m_turningAngle + m_strafeMultiplier; }

    [Header("TrackStick")]
    private Vector3 m_targetPos;
    private Vector3 m_currentPos;

    [Header("Boost Variables")]
    private float m_currentBoost;
    public float ReturnBoost() { return m_currentBoost; }
    public int ReturnBoostLevel() { return m_boostLevel; }
    public bool wantingToBoost;
    private bool m_isBoosting;
    private bool m_isInRedline;
    public float forceMultiplier;
    public float accelerationForce;
    public float howFastYouGetBoost;
    public float howFastYouLooseBoost;
    public float boostTime;
    private bool m_isBoostingOnBoostPad;
    public bool ReturnIsBoosting() { return m_isBoosting; }
    public bool ReturnIsInRedline() { return m_isInRedline; }
    [SerializeField, Range(0,3)] private int m_boostLevel;

    public void SwitchRedlineBool(bool isTrue) { m_isInRedline = isTrue; }

    public void ResetAngles(float angle1, float angle2, float angle3)
    {
        m_currentAngle = angle1;
        m_targetAngle = angle2;
        m_shipAngle = angle3;
    }

    public void ResetPositions(Vector3 position)
    {
        m_currentPos = position;
        m_targetPos = position;
    }

    public void ResetAcceleration()
    {
        wantingToBoost = false;
        m_boostLevel = 0;
        m_acceleration = 0;
    }

    // Start is called before the first frame update
    void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
        if (VariantObject != null)
        {
            variant = new ShipVariant();
            variant = Instantiate<ShipVariant>(VariantObject);
        }
        if(variant != null)
            m_currentMaxSpeed = variant.DefaultMaxSpeed;
    }

    private void OnEnable()
    {
        if (variant != null && shipModel != null)
        {
            FindChildWithTag(shipModel.transform);
        }
    }

    private void FindChildWithTag(Transform childParent)
    {
        foreach (Transform child in childParent)
        {
            if (child.CompareTag("Fire"))
            {
                m_fire.Add(child.gameObject);
            }

            if (child.childCount > 0)
            {
                FindChildWithTag(child);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.enabled && GameManager.gManager.raceStarted)
        {
            AddToBoost();
            if(m_fire.Count > 0)
                SwitchFire();
            CheckBoost();
            Strafe();
            Turn();
            Brake();
            Accelerate();
            DownForce();
            RotateShip();
        }
    }

    public void ClearList()
    {
        m_fire.Clear();
    }

    /// <summary>
    /// switches the fire
    /// </summary>
    private void SwitchFire()
    {
        m_fireIndex = m_boostLevel;
        switch (m_fireIndex)
        {
            case 0:
                m_fire[0].SetActive(false);
                m_fire[1].SetActive(false);
                m_fire[2].SetActive(false);
                break;
            case 1:
                m_fire[0].SetActive(true);
                m_fire[1].SetActive(false);
                m_fire[2].SetActive(false);
                break;
            case 2:
                m_fire[1].SetActive(true);
                m_fire[2].SetActive(false);
                m_fire[0].SetActive(false);
                break;
            case 3:
                m_fire[2].SetActive(true);
                m_fire[0].SetActive(false);
                m_fire[1].SetActive(false);
                break;
        }

    }

    /// <summary>
    /// this will check if you are currently boosting and if not then slowly take away from the current boost
    /// </summary>
    private void CheckBoost()
    {
        if (!m_isInRedline && m_currentBoost > 0)
        {
            if(m_currentBoost > m_boostLevel)
            {
                m_currentBoost -= howFastYouLooseBoost * Time.deltaTime;
                if(m_currentBoost < m_boostLevel)
                {
                    m_currentBoost = m_boostLevel;
                }
            }

        }
        if (m_fire.Count > 0)
            SwitchFire();
    }

    /// <summary>
    /// Adding to the current boost when in a red line and changing the level of boost the character is at
    /// </summary>
    public void AddToBoost()
    {
        if (m_isInRedline)
        {
            float multiplier = 1f / (m_boostLevel + 1);
            m_currentBoost += howFastYouGetBoost * multiplier * Time.deltaTime;
            if (m_currentBoost > 3)
            {
                m_currentBoost = 3;
            }
            switch (m_currentBoost)
            {
                case < 1:
                    m_boostLevel = 0;
                    m_fireIndex = 0;
                    break;
                case < 2:
                    m_boostLevel = 1;
                    m_fireIndex = 1;
                    break;
                case < 3:
                    m_boostLevel = 2;
                    m_fireIndex = 2;
                    break;
                case < 4:
                    m_boostLevel = 3;
                    m_fireIndex = 3;
                    break;
            }
            CheckBoost();
        }
    }

    /// <summary>
    /// Rotates the ship after the calculations
    /// </summary>
    private void RotateShip()
    {
        // this is similar to the ship turn lerp but its for the ship model to swing from side to side depending on which direction you are turning
        m_shipAngle = Mathf.Lerp(Mathf.Clamp(m_shipAngle, -35, 35), (m_currentAngle * 2f) * Mathf.Rad2Deg, 0.03f);

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

                if (hit.distance < 1f)
                    m_rb.AddForce(transform.up * 4000, ForceMode.Force);
            }
        }

        m_currentPos.x = Mathf.LerpAngle(m_currentPos.x, m_targetPos.x, 0.05f);
        m_currentPos.y = Mathf.LerpAngle(m_currentPos.y, m_targetPos.y, 0.05f);
        m_currentPos.z = Mathf.LerpAngle(m_currentPos.z, m_targetPos.z, 0.05f);

        if (hit.distance > 1.5f)
            m_rb.AddForce(-transform.up * variant.DownForce, ForceMode.Force);


    }

    /// <summary>
    /// this is only for the reset box thats used outside the tunnel incase the player comes out upside down
    /// </summary>
    /// <param name="pointOfCast"></param>
    public void SetRotationToTrack(Transform pointOfCast)
    {
        RaycastHit hit;
        if (Physics.Raycast(pointOfCast.position, -pointOfCast.up, out hit))
        {
            if (hit.transform.tag == "Road")
            {
                m_targetPos = hit.normal;
            }
        }

        Debug.DrawLine(pointOfCast.position, hit.point);

        m_currentPos.z = Mathf.Lerp(m_currentPos.z, m_targetPos.z, 0.01f);
    }

    /// <summary>
    /// This is only for the boost pad. its called from when you hit the boost pad
    /// </summary>
    /// <param name="force"> How strong the boost is </param>
    public void BoostPadBoost(float force)
    {
        m_isBoostingOnBoostPad = true;
        m_rb.AddForce(transform.forward * force, ForceMode.VelocityChange);
        if (m_fire.Count > 0)
            SwitchFire();
        StartCoroutine(StopBoosting());
    }

    IEnumerator StopBoosting()
    {
        yield return new WaitForSeconds(2f);

        m_isBoostingOnBoostPad = false;
    }

    /// <summary>
    /// When the player hits boost you get the first jolt of boost which feels fast
    /// </summary>
    private void ShipBoost()
    {
        if (m_boostLevel > 0)
        {
            m_isBoosting = true;
            ControllerHaptics cRumble = this.gameObject.GetComponent<ControllerHaptics>();
            PlayerInputScript pInput = this.gameObject.GetComponent<PlayerInputScript>();
            if (cRumble != null && pInput != null)
            {
                switch (m_boostLevel)
                {
                    case 1:
                        {
                            cRumble.RumbleTiming(pInput.GetPlayerGamepad(), 1, 0.3f);
                            break;
                        }
                    case 2:
                        {
                            cRumble.RumbleTiming(pInput.GetPlayerGamepad(), 2, 0.5f);
                            break;
                        }
                    case 3:
                        {
                            cRumble.RumbleTiming(pInput.GetPlayerGamepad(), 3, 0.8f);
                            break;
                        }
                }
            }
            m_rb.AddForce(transform.forward * forceMultiplier, ForceMode.VelocityChange);
            StartCoroutine(ShipBoostAcceleration());
        }
    }
     /// <summary>
     /// after the first jolt the ship will maintain the the speed for a bit depending on the level of boost
     /// </summary>
     /// <returns></returns>
    IEnumerator ShipBoostAcceleration()
    {
        float time = m_boostLevel * boostTime;

        while (time > 0)
        {
            if (time > 0)
            {
                time -= 1f * Time.deltaTime;
            }

            m_rb.AddForce(transform.forward * accelerationForce, ForceMode.Acceleration);
        }

        m_fire[0].SetActive(false);
        m_fire[1].SetActive(false);
        m_fire[2].SetActive(false);
        wantingToBoost = false;
        m_currentBoost = 0f;
        m_boostLevel = 0;

        yield return new WaitForSeconds(1f);
        m_isBoosting = false;
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

        m_acceleration = Mathf.Clamp(m_acceleration, 0, variant.DefaultMaxAcceleration);

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
        float multiplier = 0.5f;
        if (!m_isBoosting && !m_isBoostingOnBoostPad)
            multiplier = variant.TurnSpeedCurve.Evaluate(m_rb.velocity.magnitude / m_currentMaxSpeed);

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
    public void SetTurnMultipliers(float multiplier) { m_turningAngle = multiplier; AddAnglesTogether(m_strafeMultiplier, m_turningAngle); }
    private void AddAnglesTogether(float angle1, float angle2) { m_targetAngle = angle1 + angle2; }
    public void SetStrafeMultiplier(float multiplier) { m_strafeMultiplier = multiplier; AddAnglesTogether(m_strafeMultiplier, m_turningAngle); }
    public void IsBoosting() { ShipBoost(); }
}

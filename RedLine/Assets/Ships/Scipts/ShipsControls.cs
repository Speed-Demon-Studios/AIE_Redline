using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipsControls : MonoBehaviour
{
    [Header("Refrences")]
    private Rigidbody m_rb;
    public Rigidbody ReturnRB() { return m_rb; }
    public Transform rotation;
    public Transform facingPoint;
    public GameObject shipModel;
    public Transform rayCastPoint;

    [Space]

    public float downForce;

    [Header("Speed Variables")]
    private float m_accelerateMultiplier;
    public float maxSpeed;
    public float acceleration;
    public AnimationCurve speedCurve;

    [Header("Turning Varibles")]
    private float m_targetAngle;
    private float m_currentAngle;
    public float turnSpeed;
    private float m_shipAngle;
    public AnimationCurve turnSpeedCurve;

    [Header("TrackStick")]
    private Vector3 m_targetPos;
    private Vector3 m_currentPos;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Turn();
        Accelerate();
        DownForce();
        RotateShip();
    }

    /// <summary>
    /// Rotates the ship after the calculations
    /// </summary>
    private void RotateShip()
    {
        // this is similar to the ship turn lerp but its for the ship model to swing from side to side depending on which direction you are turning
        m_shipAngle = Mathf.Lerp(m_shipAngle, m_currentAngle * Mathf.Rad2Deg, 0.04f);

        // first it will look at facing position which in the empty object infront of the ship
        transform.LookAt(facingPoint, transform.up);
        //transform.rotation = Quaternion.LookRotation((transform.position - facingPoint.position).normalized, m_targetPos);
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

        m_rb.AddForce(-transform.up * downForce, ForceMode.Force);
    }

    /// <summary>
    /// Accelerate is very simple. It basicly makes the car go foward or back depending on what you press
    /// </summary>
    private void Accelerate()
    {
        float multiplier = speedCurve.Evaluate(m_rb.velocity.magnitude / maxSpeed);
        Vector3 direction = (transform.position - facingPoint.position).normalized;
        m_rb.velocity += acceleration * direction * -m_accelerateMultiplier * multiplier;
    }

    /// <summary>
    /// Turn sounds simple but it doesnt turn the ship. it rotates an position in front of the ship then that becomes the foward direction
    /// </summary>
    private void Turn()
    {
        // this script has a targetAngle which is where the empty position wants to be but to make it smooth it lerps the current pos to the target pos
        m_currentAngle = Mathf.Lerp(m_currentAngle, m_targetAngle, 0.07f);

        // this multiplier changes the turn angle based on how fast you are going. The faster you go the less you turn
        float multiplier = turnSpeedCurve.Evaluate(m_rb.velocity.magnitude / maxSpeed);

        // this rotation is for the turning of the ship which only happens on the ships local y axis
        rotation.localRotation = Quaternion.Euler(new Vector3(0, m_currentAngle * (turnSpeed * multiplier), 0));

        // this uses the shipAngle lerp to rotate both on the y axis and the z axis
        shipModel.transform.localRotation = Quaternion.Euler(new Vector3(0, m_shipAngle, -m_shipAngle * 0.4f));
    }

    /// <summary>
    /// both of these functions set the multipler when an input is sent
    /// </summary>
    /// <param name="multiplier"></param>
    public void SetSpeedMultiplier(float multiplier) { m_accelerateMultiplier = multiplier; }
    public void SetTurnMultipliers(float multiplier) { m_targetAngle = multiplier; }
}

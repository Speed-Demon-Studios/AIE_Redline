using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipsControls : MonoBehaviour
{
    [Header("Refrences")]
    private Rigidbody m_rb;

    [Header("Public variables")]
    public float maxSpeed;
    public float acceleration;
    public float maxTurnSpeed;
    public float turnSpeed;
    public AnimationCurve curve;

    [Header("Private Variables")]
    private float m_accelerateMultiplier;
    private float m_turnMultiplier;
    private Vector3 m_facing;


    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponentInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Accelerate();
        Turn();
    }

    private void Accelerate()
    {
        float multiplier = curve.Evaluate(m_rb.velocity.magnitude / maxSpeed);
        m_rb.velocity += acceleration * m_facing * multiplier * m_accelerateMultiplier;
    }

    private void Turn()
    {
        m_rb.angularVelocity += new Vector3(0, turnSpeed * m_turnMultiplier, 0);
        m_facing = m_rb.angularVelocity;
    }

    public void SetSpeedMultiplier(float multiplier) { m_accelerateMultiplier = multiplier; }
    public void SetTurnMultiplier(float multiplier) { m_turnMultiplier = multiplier; }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipsControls : MonoBehaviour
{
    [Header("Refrences")]
    private Rigidbody m_rb;
    public Transform rotation;
    public Transform facingPoint;
    public GameObject shipModel;

    [Space]

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
    }

    private void Accelerate()
    {
        float multiplier = speedCurve.Evaluate(m_rb.velocity.magnitude / maxSpeed);
        Vector3 direction = (transform.position - facingPoint.position).normalized;
        m_rb.velocity += acceleration * direction * -m_accelerateMultiplier * multiplier;
    }

    private void Turn()
    {
        transform.LookAt(facingPoint);

        m_currentAngle = Mathf.Lerp(m_currentAngle, m_targetAngle, 0.07f);

        float multiplier = turnSpeedCurve.Evaluate(m_rb.velocity.magnitude / maxSpeed);

        m_shipAngle = Mathf.Lerp(m_shipAngle, m_currentAngle * Mathf.Rad2Deg, 0.05f);

        shipModel.transform.localRotation = Quaternion.Euler(new Vector3(0, m_shipAngle, -m_shipAngle * 0.4f));

        rotation.localRotation = Quaternion.Euler(new Vector3(0, m_currentAngle * (turnSpeed * multiplier), 0));
    }

    public void SetSpeedMultiplier(float multiplier) { m_accelerateMultiplier = multiplier; }
    public void SetTurnMultipliers(float multiplier) { m_targetAngle = multiplier; }
}

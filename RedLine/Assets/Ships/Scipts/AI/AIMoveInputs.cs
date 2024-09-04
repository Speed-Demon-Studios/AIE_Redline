using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public class AIMoveInputs : MonoBehaviour
{
    [SerializeField] private ShipVariant Variant;
    private float m_speed;
    private ShipsControls m_controls;
    private Vector3 m_randomPos;
    private float m_firstDistanceToNode;
    private float m_currentDistanceToNode;
    private float m_targetTurnAngle;
    private float m_currentTurnAngle;

    public float turnSpeed;

    public GameObject desiredNode;
    public GameObject nodeParent;
    public TMP_Text test;
    public TMP_Text test2;

    // Start is called before the first frame update
    void Start()
    {
        m_controls = GetComponent<ShipsControls>();

        m_randomPos = desiredNode.GetComponent<Nodes>().RandomNavSphere(desiredNode.transform.position);

        m_firstDistanceToNode = Vector3.Distance(this.transform.position, m_randomPos);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Accelerate();
        Turning();
        //HowFastShouldIGo();
    }

    private void Turn()
    {
        // checks if it is at the current node
        // if so then it will change the current node to the next node
        // if not then it will continue
        if (Vector3.Distance(this.gameObject.transform.position, desiredNode.transform.position) < Variant.distance)
        {
            desiredNode = desiredNode.GetComponent<Nodes>().nextNode.gameObject;
            m_randomPos = desiredNode.GetComponent<Nodes>().RandomNavSphere(desiredNode.transform.position);
            m_firstDistanceToNode = Vector3.Distance(this.transform.position, m_randomPos);
        }

        // nodeDirection is finding the direction to the current node
        // directionFoward is the forward facing of the ship
        // nodeDirectionNext is the direction from the ship to the next node
        Vector3 nodeDirection = (transform.position - m_randomPos).normalized;
        Vector3 directionFoward = (transform.position - m_controls.facingPoint.position).normalized;

        // finding the angle between the ship and the current node then changing it to radians
        float angle = Vector3.SignedAngle(nodeDirection, directionFoward, transform.up);

        m_speed = 1;
        Debug.DrawLine(this.transform.position, m_randomPos);
        angle = Mathf.Clamp(angle, -45, 45);
        m_controls.SetStrafeMultiplier(-angle);
    }

    private void HowFastShouldIGo()
    {
        // directionFoward is the forward facing of the ship
        // nodeDirectionNext is the direction from the ship to the next node
        Vector3 directionFoward = (transform.position - m_controls.facingPoint.position).normalized;
        Vector3 nodeDirectionNext = (transform.position - desiredNode.GetComponent<Nodes>().nextNode.transform.position).normalized;

        // this chunk is finding both current nodes up and next nodes up and getting the difference between them
        // this will be used later
        Transform pointA = desiredNode.transform;
        Transform pointB = desiredNode.GetComponent<Nodes>().nextNode.transform;
        Vector3 up = (pointB.up + pointA.up);
        float distance = Vector3.Distance(pointA.up, pointB.up);

        // finding the angle between the ship and the next node and changing it to radians
        float secondAngle = Vector3.SignedAngle(nodeDirectionNext, directionFoward, transform.up);
        float secondAngleRad = secondAngle * Mathf.Deg2Rad;

        float neededSpeed;

        // finding out if it left of right less than 0 if left, more than 0 is right
        if (secondAngleRad < 0)
        {
            // changing the radian to a non-negative
            float secondTempAngleRad = -secondAngleRad;
            // finding out how much speed you need basd on the curve
            float neededSpeedNextNode = Variant.NeededSpeedCurve.Evaluate(secondTempAngleRad - distance);
            // setting tempSpeed to that speed found by the curve
            neededSpeed = neededSpeedNextNode;
        }
        else
        {
            // finding out how much speed you need basd on the curve
            float neededSpeedNextNode = Variant.NeededSpeedCurve.Evaluate(secondAngleRad - distance);
            // setting tempSpeed to that speed found by the curve
            neededSpeed = neededSpeedNextNode;
        }

        float currentSpeedPer = m_controls.ReturnRB().velocity.magnitude / m_controls.GetMaxSpeed();

        if (neededSpeed < currentSpeedPer)
        {
            m_controls.SetBrakeMultiplier(currentSpeedPer - neededSpeed);
            m_speed = 0;
        }
        else
        {
            m_controls.SetBrakeMultiplier(0);
            m_speed = neededSpeed;
        }

        if (test != null)
        {
            test.text = neededSpeed.ToString();
        }

        if (test2 != null)
        {
            test2.text = currentSpeedPer.ToString();
        }
    }

    private void Turning()
    {
        // checks if it is at the current node
        // if so then it will change the current node to the next node
        // if not then it will continue
        if (Vector3.Distance(this.gameObject.transform.position, desiredNode.transform.position) < Variant.distance || Vector3.Distance(this.gameObject.transform.position, desiredNode.GetComponent<Nodes>().nextNode.transform.position) < Variant.distance)
        {
            desiredNode = desiredNode.GetComponent<Nodes>().nextNode.gameObject;
            m_randomPos = desiredNode.GetComponent<Nodes>().RandomNavSphere(desiredNode.transform.position);
            m_firstDistanceToNode = Vector3.Distance(this.transform.position, m_randomPos);
        }
        m_currentDistanceToNode = Vector3.Distance(this.transform.position, m_randomPos);

        // this chunk is finding both current nodes up and next nodes up and getting the difference between them
        // this will be used later
        Transform pointA = desiredNode.transform;
        Transform pointB = desiredNode.GetComponent<Nodes>().nextNode.transform;
        Vector3 up = (pointB.up + pointA.up);
        float distance = Vector3.Distance(pointA.up, pointB.up);

        // nodeDirection is finding the direction to the current node
        // directionFoward is the forward facing of the ship
        // nodeDirectionNext is the direction from the ship to the next node
        Vector3 nodeDirection = (transform.position - m_randomPos).normalized;
        Vector3 directionFoward = (transform.position - m_controls.facingPoint.position).normalized;
        Vector3 nodeDirectionNext = (transform.position - desiredNode.GetComponent<Nodes>().nextNode.transform.position).normalized;

        // finding the angle between the ship and the next node and changing it to radians
        float secondAngle = Vector3.SignedAngle(nodeDirectionNext, directionFoward, up);
        float secondAngleRad = secondAngle * Mathf.Deg2Rad;

        m_targetTurnAngle = secondAngleRad;

        float neededSpeed;

        // finding out if it left of right less than 0 if left, more than 0 is right
        if (secondAngleRad < 0)
        {
            // changing the radian to a non-negative
            float secondTempAngleRad = -secondAngleRad;
            // finding out how much speed you need basd on the curve
            float neededSpeedNextNode = Variant.NeededSpeedCurve.Evaluate(secondTempAngleRad - distance);
            // setting tempSpeed to that speed found by the curve
            neededSpeed = neededSpeedNextNode;
        }
        else
        {
            // finding out how much speed you need basd on the curve
            float neededSpeedNextNode = Variant.NeededSpeedCurve.Evaluate(secondAngleRad - distance);
            // setting tempSpeed to that speed found by the curve
            neededSpeed = neededSpeedNextNode;
        }

        float currentSpeedPer = m_controls.ReturnRB().velocity.magnitude / m_controls.GetMaxSpeed();

        //if(neededSpeed < currentSpeedPer)
        //{
        //    m_controls.SetBrakeMultiplier(currentSpeedPer - neededSpeed);
        //    m_speed = 0;
        //}
        //else
        //{
        //    m_controls.SetBrakeMultiplier(0);
        //    m_speed = neededSpeed;
        //}

        m_speed = 0.90f;

        float percentage = CalculatePercentage();

        if(test != null)
        {
            test.text = percentage.ToString();
        }

        m_currentTurnAngle = m_targetTurnAngle * percentage / 100;

        Debug.DrawLine(this.transform.position, desiredNode.GetComponent<Nodes>().nextNode.transform.position);

        m_controls.SetStrafeMultiplier(-m_currentTurnAngle * turnSpeed);
    }

    private float CalculatePercentage()
    {
        float maxTakeMin = m_firstDistanceToNode - Variant.distance;
        float customPieNumber = 100 / maxTakeMin;
        float differnceOfMaxNX = m_firstDistanceToNode - m_currentDistanceToNode;
        float extraPercentage = maxTakeMin - differnceOfMaxNX;
        float percentage = extraPercentage * customPieNumber;
        return percentage;
    }

    private void Accelerate()
    {
        m_controls.SetSpeedMultiplier(m_speed);
    }
}

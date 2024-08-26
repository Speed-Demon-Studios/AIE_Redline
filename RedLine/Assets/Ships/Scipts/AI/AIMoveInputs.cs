using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class AIMoveInputs : MonoBehaviour
{
    [SerializeField] private ShipVariant Variant;
    private List<Nodes> m_nodes = new();
    private float m_speed;
    private ShipsControls m_controls;
    private Vector3 randomPos;
    public GameObject nodeParent;
    public GameObject m_desiredNode;
    public Text test;
    public Text test2;

    // Start is called before the first frame update
    void Start()
    {
        m_controls = GetComponent<ShipsControls>();

        float startNodeDistance = 5000;
        for(int i = 0; i < nodeParent.transform.childCount; i++)
        {
            if(Vector3.Distance(this.transform.position, nodeParent.transform.GetChild(i).gameObject.transform.position) < startNodeDistance)
            {
                startNodeDistance = Vector3.Distance(this.transform.position, nodeParent.transform.GetChild(i).gameObject.transform.position);
            }
            m_nodes.Add(nodeParent.transform.GetChild(i).gameObject.GetComponent<Nodes>());
        }
        randomPos = m_desiredNode.GetComponent<Nodes>().RandomNavSphere(m_desiredNode.transform.position);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Accelerate();
        Turning();
    }

    private void Turning()
    {
        // checks if it is at the current node
        // if so then it will change the current node to the next node
        // if not then it will continue
        if (Vector3.Distance(this.gameObject.transform.position, m_desiredNode.transform.position) < Variant.distance)
        {
            m_desiredNode = m_desiredNode.GetComponent<Nodes>().nextNode.gameObject;
            randomPos = m_desiredNode.GetComponent<Nodes>().RandomNavSphere(m_desiredNode.transform.position);
        }

        // this chunk is finding both current nodes up and next nodes up and getting the difference between them
        // this will be used later
        Transform pointA = m_desiredNode.transform;
        Transform pointB = m_desiredNode.GetComponent<Nodes>().nextNode.transform;
        Vector3 up = (pointB.up + pointA.up);
        float distance = Vector3.Distance(pointA.up, pointB.up);

        // nodeDirection is finding the direction to the current node
        // directionFoward is the forward facing of the ship
        // nodeDirectionNext is the direction from the ship to the next node
        Vector3 nodeDirection = (transform.position - randomPos).normalized;
        Vector3 directionFoward = (transform.position - m_controls.facingPoint.position).normalized;
        Vector3 nodeDirectionNext = (transform.position - m_desiredNode.GetComponent<Nodes>().nextNode.transform.position).normalized;

        // finding the angle between the ship and the current node then changing it to radians
        float angle = Vector3.SignedAngle(nodeDirection, directionFoward, up);
        float angleRad = angle * Mathf.Deg2Rad;
        // finding the angle between the shit and the next node and changing it to radians
        float secondAngle = Vector3.SignedAngle(nodeDirectionNext, directionFoward, up);
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

        if(test != null)
        {
            test.text = neededSpeed.ToString();
        }

        float currentSpeedPer = m_controls.ReturnRB().velocity.magnitude / m_controls.GetMaxSpeed();

        if(test2 != null)
        {
            test2.text = currentSpeedPer.ToString();
        }

        if(neededSpeed < currentSpeedPer)
        {
            m_controls.SetBrakeMultiplier(currentSpeedPer - neededSpeed);
            m_speed = 0;
        }
        else
        {
            m_controls.SetBrakeMultiplier(0);
            m_speed = neededSpeed;
        }

        Debug.DrawLine(this.transform.position, randomPos);

        m_controls.SetTurnMultipliers(-angleRad * 2);
    }

    private void Accelerate()
    {
        m_controls.SetSpeedMultiplier(m_speed);
    }
}

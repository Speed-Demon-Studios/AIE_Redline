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
        if (Vector3.Distance(this.gameObject.transform.position, m_desiredNode.transform.position) < Variant.distance)
        {
            m_desiredNode = m_desiredNode.GetComponent<Nodes>().nextNode.gameObject;
            randomPos = m_desiredNode.GetComponent<Nodes>().RandomNavSphere(m_desiredNode.transform.position);
        }

        Transform pointA = m_desiredNode.transform;
        Transform pointB = m_desiredNode.GetComponent<Nodes>().nextNode.transform;

        Vector3 up = (pointB.up + pointA.up);

        float distance = Vector3.Distance(pointA.up, pointB.up);

        Vector3 nodeDirection = (transform.position - randomPos).normalized;
        Vector3 directionFoward = (transform.position - m_controls.facingPoint.position).normalized;
        Vector3 nodeDirectionNext = (transform.position - m_desiredNode.GetComponent<Nodes>().nextNode.transform.position).normalized;

        float angle = Vector3.SignedAngle(nodeDirection, directionFoward, up);
        float angleRad = angle * Mathf.Deg2Rad;

        float secondAngle = Vector3.SignedAngle(nodeDirectionNext, directionFoward, up);
        float secondAngleRad = secondAngle * Mathf.Deg2Rad;

        float tempSpeed;

        if (secondAngleRad < 0)
        {
            float secondTempAngleRad = -secondAngleRad;
            float neededSpeedNextNode = Variant.NeededSpeedCurve.Evaluate(secondTempAngleRad + distance);
            //float nextSpeedPercent = m_controls.ReturnRB().velocity.magnitude / (m_controls.variant.DefaultMaxSpeed * 0.7f); // ** Max Speed
            tempSpeed = neededSpeedNextNode;
            if (test != null)
            {
                test.text = "Neg" + (secondAngleRad - distance).ToString();
            }
        }
        else
        {
            float neededSpeedNextNode = Variant.NeededSpeedCurve.Evaluate(secondAngleRad - distance);
            //float nextSpeedPercent = m_controls.ReturnRB().velocity.magnitude / (m_controls.variant.DefaultMaxSpeed * 0.7f); // ** Max Speed
            tempSpeed = neededSpeedNextNode;
            if (test != null)
            {
                test.text = "Pos" + (secondAngleRad - distance).ToString();
            }
        }

        m_speed = tempSpeed;

        if (m_speed < 0)
            m_speed *= 5f;



        Debug.DrawLine(this.transform.position, randomPos);

        m_controls.SetTurnMultipliers(-angleRad);
    }

    private void Accelerate()
    {
        m_controls.SetSpeedMultiplier(m_speed);
    }
}

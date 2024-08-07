using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AIMoveInputs : MonoBehaviour
{
    private List<Nodes> m_nodes = new();
    public GameObject nodeParent;
    public float distance;
    public AnimationCurve neededSpeedCurve;
    private float m_speed;
    private ShipsControls m_controls;

    private Vector3 randomPos;
    public GameObject m_desiredNode;
    public float radius;

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
    void Update()
    {
        Accelerate();
        Turning();

    }

    private void Turning()
    {
        if (Vector3.Distance(this.gameObject.transform.position, m_desiredNode.transform.position) < distance)
        {
            m_desiredNode = m_desiredNode.GetComponent<Nodes>().nextNode.gameObject;
            randomPos = m_desiredNode.GetComponent<Nodes>().RandomNavSphere(m_desiredNode.transform.position);
        }

        Vector3 directionFoward = (transform.position - m_controls.facingPoint.position).normalized;
        Vector3 nodeDirectionNext = (transform.position - m_desiredNode.GetComponent<Nodes>().nextNode.transform.position).normalized;

        float secondAngle = Vector3.SignedAngle(nodeDirectionNext, directionFoward, Vector3.up);
        float secondAngleRad = secondAngle * Mathf.Deg2Rad;

        float tempSpeed2;

        if (secondAngleRad < 0)
        {
            float secondTempAngleRad = -secondAngleRad;
            float neededSpeedNextNode = neededSpeedCurve.Evaluate(secondTempAngleRad);
            float nextSpeedPercent = m_controls.ReturnRB().velocity.magnitude / (m_controls.maxSpeed * 0.7f);
            tempSpeed2 = neededSpeedNextNode - nextSpeedPercent;
        }
        else
        {
            float neededSpeedNextNode = neededSpeedCurve.Evaluate(secondAngleRad);
            float nextSpeedPercent = m_controls.ReturnRB().velocity.magnitude / (m_controls.maxSpeed * 0.7f);
            tempSpeed2 = neededSpeedNextNode - nextSpeedPercent;
        }

        m_speed = tempSpeed2;

        if (m_speed < 0)
            m_speed *= 5f;
        Debug.DrawLine(this.transform.position, randomPos);
        Debug.Log(m_speed);

        m_controls.SetTurnMultipliers(-secondAngleRad);
    }

    private void Accelerate()
    {
        m_controls.SetSpeedMultiplier(m_speed);
    }
}

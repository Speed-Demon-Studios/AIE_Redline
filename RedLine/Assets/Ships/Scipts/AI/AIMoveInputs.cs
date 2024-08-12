using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;

public class AIMoveInputs : MonoBehaviour
{
    [SerializeField] private ShipVariant Variant;
    private List<Nodes> m_nodes = new();
    private float m_speed;
    private ShipsControls m_controls;
    private Vector3 randomPos;
    public GameObject nodeParent;
    public GameObject m_desiredNode;

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
        if (Vector3.Distance(this.gameObject.transform.position, m_desiredNode.transform.position) < Variant.distance)
        {
            m_desiredNode = m_desiredNode.GetComponent<Nodes>().nextNode.gameObject;
            randomPos = m_desiredNode.GetComponent<Nodes>().RandomNavSphere(m_desiredNode.transform.position);
        }

        Vector3 nodeDirection = (transform.position - randomPos).normalized;
        Vector3 directionFoward = (transform.position - m_controls.facingPoint.position).normalized;
        Vector3 nodeDirectionNext = (transform.position - m_desiredNode.GetComponent<Nodes>().nextNode.transform.position).normalized;

        float angle = Vector3.SignedAngle(nodeDirection, directionFoward, transform.up);
        float angleRad = angle * Mathf.Deg2Rad;

        float secondAngle = Vector3.SignedAngle(nodeDirectionNext, directionFoward, transform.up);
        float secondAngleRad = secondAngle * Mathf.Deg2Rad;

        float tempSpeed2;

        if (secondAngleRad < 0)
        {
            float secondTempAngleRad = -secondAngleRad;
            float neededSpeedNextNode = Variant.NeededSpeedCurve.Evaluate(secondTempAngleRad);
            float nextSpeedPercent = m_controls.ReturnRB().velocity.magnitude / (m_controls.Variant.MaxSpeed * 0.7f); // ** Max Speed
            tempSpeed2 = neededSpeedNextNode - nextSpeedPercent;
        }
        else
        {
            float neededSpeedNextNode = Variant.NeededSpeedCurve.Evaluate(secondAngleRad);
            float nextSpeedPercent = m_controls.ReturnRB().velocity.magnitude / (m_controls.Variant.MaxSpeed * 0.7f); // ** Max Speed
            tempSpeed2 = neededSpeedNextNode - nextSpeedPercent;
        }

        m_speed = tempSpeed2;

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

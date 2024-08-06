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
    private int m_currentNodeIndex;
    public AnimationCurve neededSpeedCurve;
    private float m_speed;
    public float maxAngle;
    private ShipsControls m_controls;

    private bool m_didISkipLastCheckPoint;
    private Vector3 m_desiredNode;
    public float radius;

    // Start is called before the first frame update
    void Start()
    {
        m_controls = GetComponent<ShipsControls>();

        float startNodeDistance = 5000;
        int startNodeIndex = 0;
        for(int i = 0; i < nodeParent.transform.childCount; i++)
        {
            if(Vector3.Distance(this.transform.position, nodeParent.transform.GetChild(i).gameObject.transform.position) < startNodeDistance)
            {
                startNodeDistance = Vector3.Distance(this.transform.position, nodeParent.transform.GetChild(i).gameObject.transform.position);
                startNodeIndex = i;
            }
            m_nodes.Add(nodeParent.transform.GetChild(i).gameObject.GetComponent<Nodes>());
        }
        m_currentNodeIndex = startNodeIndex;
        m_desiredNode = m_nodes[m_currentNodeIndex].RandomNavSphere(m_nodes[m_currentNodeIndex].ReturnNodePos());
    }

    // Update is called once per frame
    void Update()
    {
        Accelerate();
        Turning();
        Debug.DrawLine(this.transform.position, m_desiredNode);
    }

    private void Turning()
    {
        if (Vector3.Distance(this.gameObject.transform.position, m_desiredNode) < distance)
        {
            m_currentNodeIndex += 1;
            if (m_currentNodeIndex > m_nodes.Count - 1)
                m_currentNodeIndex = 0;
            m_desiredNode = m_nodes[m_currentNodeIndex].RandomNavSphere(m_nodes[m_currentNodeIndex].ReturnNodePos());
        }

        Vector3 direction = (transform.position - m_controls.facingPoint.position).normalized;

        Vector3 nodeDirection = (transform.position - m_desiredNode).normalized;

        float angle = Vector3.SignedAngle(nodeDirection, direction, Vector3.up);

        float angleRad = angle * Mathf.Deg2Rad;

        if(angleRad < 0)
        {
            float tempAngleRad = -angleRad;
            float neededSpeed = neededSpeedCurve.Evaluate(tempAngleRad);
            float currentSpeedPercent = m_controls.ReturnRB().velocity.magnitude / (m_controls.maxSpeed * 0.7f);
            m_speed = neededSpeed - currentSpeedPercent;
        }
        else
        {
            float neededSpeed = neededSpeedCurve.Evaluate(angleRad);
            float currentSpeedPercent = m_controls.ReturnRB().velocity.magnitude / (m_controls.maxSpeed * 0.7f);
            m_speed = neededSpeed - currentSpeedPercent;
        }

        if(m_speed < 0)
        {
            m_speed = m_speed * 2;
        }

        Debug.Log(m_speed);

        m_controls.SetTurnMultipliers(-angleRad);
    }

    private void Accelerate()
    {
        m_controls.SetSpeedMultiplier(m_speed);
    }
}

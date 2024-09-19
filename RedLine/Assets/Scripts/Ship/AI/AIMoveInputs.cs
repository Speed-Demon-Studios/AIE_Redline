using System;
using System.Collections;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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

    public Nodes desiredNode;
    private Nodes m_nextNode;
    public GameObject nodeParent;
    public TMP_Text test;
    public TMP_Text test2;

    // Start is called before the first frame update
    void Start()
    {
        m_controls = GetComponent<ShipsControls>();

        if(desiredNode.nextNode.Count > 1)
        {
            int nodeChoice = Random.Range(0, desiredNode.GetComponent<Nodes>().nextNode.Count);
            m_nextNode = desiredNode.nextNode[nodeChoice];
        }
        else
        {
            m_nextNode = desiredNode.nextNode[0];
        }

        m_randomPos = desiredNode.RandomNavSphere(desiredNode.transform.position);

        m_firstDistanceToNode = Vector3.Distance(this.transform.position, m_randomPos);

        m_speed = Random.Range(0.6f, 1f);
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
        if (Vector3.Distance(this.gameObject.transform.position, desiredNode.transform.position) < Variant.distance || Vector3.Distance(this.gameObject.transform.position, m_nextNode.transform.position) < Variant.distance)
        {
            desiredNode = m_nextNode;

            if(desiredNode.nextNode.Count > 1)
            {
                int nodeChoice = Random.Range(0, desiredNode.nextNode.Count);
                m_nextNode = desiredNode.nextNode[nodeChoice];
                m_randomPos = desiredNode.RandomNavSphere(desiredNode.transform.position);
            }
            else { m_nextNode = desiredNode.nextNode[0]; m_randomPos = desiredNode.RandomNavSphere(desiredNode.transform.position); }

            m_firstDistanceToNode = Vector3.Distance(this.transform.position, m_randomPos);
        }

        m_currentDistanceToNode = Vector3.Distance(this.transform.position, m_randomPos);

        // this chunk is finding both current nodes up and next nodes up and getting the difference between them
        // this will be used later
        Transform pointA = this.transform;
        Transform pointB = m_nextNode.gameObject.transform;
        Vector3 up = (pointB.up + pointA.up);

        // nodeDirection is finding the direction to the current node
        // directionFoward is the forward facing of the ship
        // nodeDirectionNext is the direction from the ship to the next node
        Vector3 nodeDirection = (transform.position - m_randomPos).normalized;
        Vector3 directionFoward = (transform.position - m_controls.facingPoint.position).normalized;
        Vector3 nodeDirectionNext = (transform.position - m_nextNode.transform.position).normalized;

        // finding the angle between the ship and the next node and changing it to radians
        float secondAngle = Vector3.SignedAngle(nodeDirectionNext, directionFoward, up);
        float secondAngleRad = secondAngle * Mathf.Deg2Rad;

        m_targetTurnAngle = secondAngleRad;

        float percentage = CalculatePercentage();

        if(test != null)
        {
            test.text = percentage.ToString();
        }

        float turnAnglePer = m_targetTurnAngle * percentage / 100; ;

        m_currentTurnAngle = m_targetTurnAngle - turnAnglePer;

        Debug.DrawLine(this.transform.position, desiredNode.transform.position);

        m_controls.SetStrafeMultiplier(-m_currentTurnAngle * Variant.turnMultiplier);
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

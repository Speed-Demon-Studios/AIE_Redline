using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipToWallCollision : MonoBehaviour
{
    [SerializeField] private ShipsControls sControlScript;
    [SerializeField] private float knockbackPercentage = 1.3f;
    private ShipVariant shipVariant;
    private float changedTopSpeed;
    private float changedAcceleration;
    private float defaultTopSpeed;
    private float defaultAcceleration;
    bool intoWall = false;

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag.ToLower() == "walls")
        {
            intoWall = true;
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.transform.tag.ToLower() == "walls" && intoWall == true)
        {
            shipVariant.MaxSpeed = changedTopSpeed;
            shipVariant.MaxAcceleration = changedAcceleration;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.transform.tag.ToLower() == "walls" && intoWall == true)
        {
            intoWall = false;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        shipVariant = sControlScript.Variant;
        defaultTopSpeed = shipVariant.MaxSpeed;
        defaultAcceleration = shipVariant.MaxAcceleration;
        changedTopSpeed = (defaultTopSpeed * 0.45f);
        changedAcceleration = (defaultAcceleration * 0.35f);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (intoWall == false)
        {
            if (shipVariant.MaxSpeed < defaultTopSpeed)
            {
                shipVariant.MaxSpeed += 20.0f * Time.deltaTime;
            }
            else if (shipVariant.MaxSpeed > defaultTopSpeed)
            {
                shipVariant.MaxSpeed = defaultTopSpeed;
            }
            if (shipVariant.MaxAcceleration < defaultAcceleration)
            {
                shipVariant.MaxAcceleration += 1.5f * Time.deltaTime;
            }
            else if (shipVariant.MaxAcceleration > defaultAcceleration)
            {
                shipVariant.MaxAcceleration = defaultAcceleration;
            }
        }
    }
}

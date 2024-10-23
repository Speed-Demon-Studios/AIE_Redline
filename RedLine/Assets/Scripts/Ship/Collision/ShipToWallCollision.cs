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
    private bool intoWall = false;
    private bool detailsSet = false;

    public void ResetDetails()
    {
        defaultTopSpeed = 0f;
        defaultAcceleration = 0f;
        changedTopSpeed = 0f;
        changedAcceleration = 0f;
        detailsSet = false;
    }

    public void UpdateDetails()
    {
        detailsSet = true;
        shipVariant = sControlScript.VariantObject;
        defaultTopSpeed = sControlScript.GetDefaultMaxSpeed();
        //defaultAcceleration = shipVariant.MaxAcceleration;

        changedTopSpeed = (defaultTopSpeed * 0.53f); // The speed that ships will be capped at while colliding with walls.
        //changedAcceleration = (defaultAcceleration * 0.55f); // The acceleration that ships will be capped at while colliding with walls.
    }

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
            sControlScript.SetCurrentMaxSpeed(changedTopSpeed);
            //shipVariant.MaxAcceleration = changedAcceleration;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.transform.tag.ToLower() == "walls" && intoWall == true)
        {
            intoWall = false;
        }
    }

    private void Update()
    {
        if (detailsSet == false && GameManager.gManager.raceStarted)
        {
            UpdateDetails();
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (intoWall == false && detailsSet == true && GameManager.gManager.raceStarted)
        {
            if (sControlScript.GetCurrentMaxSpeed() < defaultTopSpeed)
            {
                sControlScript.SetCurrentMaxSpeed((sControlScript.GetCurrentMaxSpeed() + (defaultTopSpeed / 0.33f) * Time.deltaTime));
            }
            else if (shipVariant.DefaultMaxAcceleration > defaultTopSpeed)
            {
                sControlScript.SetCurrentMaxSpeed(defaultTopSpeed);
            }
            //if (shipVariant.MaxAcceleration < defaultAcceleration)
            //{
            //    shipVariant.MaxAcceleration += 1.5f * Time.deltaTime;
            //}
            //else if (shipVariant.MaxAcceleration > defaultAcceleration)
            //{
            //    shipVariant.MaxAcceleration = defaultAcceleration;
            //}
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICatchUpManager : MonoBehaviour
{
    List<GameObject> m_playerOBJS = new();
    public float multiplier;
    public float aheadMultiplier;
    float m_catchUpChange;

    public void Inistialize()
    {
        m_playerOBJS = GameManager.gManager.players;
    }

    private void Update()
    {
        float score = 1f;
        foreach (GameObject AIOBJ in GameManager.gManager.racerObjects)
        {
            if(ChangeCatchUp(AIOBJ) > 0)
                score *= ChangeCatchUp(AIOBJ);
        }

        float originalScore = score;
        float modFactor = 1 - (1 / GameManager.gManager.racerObjects.Count);
        float makeupValue = (1 - originalScore) * modFactor;
        float percentage = originalScore + (makeupValue * originalScore);

        m_catchUpChange = percentage;

        foreach (GameObject AIOBJ in GameManager.gManager.racerObjects)
        {
            AIOBJ.GetComponent<ShipsControls>().MaxSpeedCatchupChange(percentage);
        }
    }

    float ChangeCatchUp(GameObject ai)
    {
        RacerDetails aIRacerDets = ai.GetComponent<RacerDetails>();
        float numberOfCheckPoints = GameManager.gManager.checkpointParent.GetNumberOfChildren();
        float score = 0f;

        foreach(GameObject playerObj in m_playerOBJS)
        {
            RacerDetails playerRacerDets = playerObj.GetComponent<RacerDetails>();

            float playerCheckPointPercentage = (playerRacerDets.currentCheckpoint / numberOfCheckPoints) + playerRacerDets.currentLap;
            float aiCheckPointPercentage = (aIRacerDets.currentCheckpoint / numberOfCheckPoints) + aIRacerDets.currentLap;

            float difference = aiCheckPointPercentage - playerCheckPointPercentage;
            if (difference > 0)
            {
                float reversDiff = 1 - difference;
                score += reversDiff * multiplier;
            }
        }

        float originalScore = score;
        float modFactor = 1 - (1 / m_playerOBJS.Count);
        float makeupValue = (1 - originalScore) * modFactor;
        float percentage = originalScore + (makeupValue * originalScore);

        return percentage;
    }
}

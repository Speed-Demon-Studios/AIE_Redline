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
        if (GameManager.gManager.raceStarted && !GameManager.gManager.raceFinished)
        {
            float score = 1f;
            foreach (GameObject AIOBJ in GameManager.gManager.racerObjects)
            {
                if (ChangeCatchUp(AIOBJ) > 0)
                    score *= ChangeCatchUp(AIOBJ);
            }

            float originalScore = score;
            float modFactor = 1.4f - (1.4f / GameManager.gManager.racerObjects.Count);
            float makeupValue = (1.4f - originalScore) * modFactor;
            float percentage = originalScore + (makeupValue * originalScore);

            m_catchUpChange = percentage;

            m_catchUpChange = Mathf.Clamp(m_catchUpChange, 0.5f, 1.4f);

            foreach (GameObject AIOBJ in GameManager.gManager.racerObjects)
            {
                AIOBJ.GetComponent<ShipsControls>().MaxSpeedCatchupChange(percentage);
            }
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

            if (playerCheckPointPercentage < 1)
                playerCheckPointPercentage += 1;
            if (aiCheckPointPercentage < 1)
                aiCheckPointPercentage += 1;

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

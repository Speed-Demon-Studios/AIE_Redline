using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICatchUpManager : MonoBehaviour
{
    List<GameObject> m_playerOBJS = new();
    public float multiplier;

    public void Inistialize()
    {
        m_playerOBJS = GameManager.gManager.players;
    }

    private void Update()
    {
        foreach(GameObject AIOBJ in GameManager.gManager.racerObjects)
        {
            ChangeCatchUp(AIOBJ);
        }
    }

    void ChangeCatchUp(GameObject ai)
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
            float reversDiff = 1 - difference;
            score += reversDiff;
        }

        float originalScore = score;
        float modFactor = 1 - (1 / m_playerOBJS.Count);
        float makeupValue = (1 - originalScore) * modFactor;
        float percentage = originalScore + (makeupValue * originalScore);

        if (percentage < 1)
            percentage *= multiplier;

        ai.GetComponent<ShipsControls>().MaxSpeedCatchupChange(percentage);
    }
}

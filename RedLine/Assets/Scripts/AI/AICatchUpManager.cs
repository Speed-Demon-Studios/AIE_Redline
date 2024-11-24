using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICatchUpManager : MonoBehaviour
{
    List<GameObject> m_playerOBJS = new();

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
        int numberOfCheckPoints = GameManager.gManager.checkpointParent.GetNumberOfChildren();
        float score = 1f;

        foreach(GameObject playerObj in m_playerOBJS)
        {
            RacerDetails playerRacerDets = playerObj.GetComponent<RacerDetails>();

            float playerCheckPointPercentage = playerRacerDets.currentCheckpoint / numberOfCheckPoints;
            float aiCheckPointPercentage = aIRacerDets.currentCheckpoint / numberOfCheckPoints;

            float difference = playerCheckPointPercentage - aiCheckPointPercentage;

            score *= difference;
        }

        float originalScore = score;
        float modFactor = 1 - (1 / m_playerOBJS.Count);
        float makeupValue = (1 - originalScore) * modFactor;
        float percentage = originalScore + (makeupValue * originalScore);

        ai.GetComponent<ShipsControls>().MaxSpeedCatchupChange(percentage);
    }
}

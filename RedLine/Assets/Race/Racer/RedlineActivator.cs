using UnityEngine;

public class RedlineActivator : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (GameManager.gManager.raceStarted == true)
        {
            foreach (GameObject racerOBJ in GameManager.gManager.racerObjects)
            {
                RedlineColliderSpawner redlineScript = racerOBJ.GetComponent<RacerDetails>().rCS;

                if (redlineScript != null)
                {
                    if (redlineScript.enabled == false)
                    {
                        redlineScript.enabled = true;
                    }
                }
            }
            GameManager.gManager.redlineActivated = true;
        }


        if (GameManager.gManager.raceStarted == false)
        {
            foreach (GameObject racerOBJ in GameManager.gManager.racerObjects)
            {
                RedlineColliderSpawner redlineScript = racerOBJ.GetComponentInChildren<RedlineColliderSpawner>();
                redlineScript.enabled = true;

            }
        }
    }
}

using UnityEngine;

public class RedlineActivator : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (GameManager.gManager.CurrentScene == "Race" && GameManager.gManager.redlineActivated == false && GameManager.gManager.raceStarted == true)
        {
            GameManager.gManager.redlineActivated = true;
            foreach (GameObject racerOBJ in GameManager.gManager.racerObjects)
            {
                RedlineColliderSpawner redlineScript = racerOBJ.GetComponentInChildren<RedlineColliderSpawner>();

                if (redlineScript != null)
                {
                    redlineScript.enabled = true;
                }    
            }
        }
    }
}

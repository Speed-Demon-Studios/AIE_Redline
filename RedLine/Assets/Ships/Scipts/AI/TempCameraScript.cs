using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempCameraScript : MonoBehaviour
{
    public GameObject camerasParent;

    private List<GameObject> cameras = new();
    private GameObject currentCamera;

    public Transform average;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < camerasParent.transform.childCount; i++)
        {
            cameras.Add(camerasParent.transform.GetChild(i).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float distance = 5000;

        for (int i = 0; i < camerasParent.transform.childCount; i++)
        {
            if(Vector3.Distance(average.position, camerasParent.transform.GetChild(i).gameObject.transform.position) < distance)
            {
                currentCamera = camerasParent.transform.GetChild(i).gameObject;
                distance = Vector3.Distance(average.position, camerasParent.transform.GetChild(i).gameObject.transform.position);
            }

            camerasParent.transform.GetChild(i).gameObject.SetActive(false);
        }

        currentCamera.SetActive(true);
    }
}

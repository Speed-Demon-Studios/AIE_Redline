using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class WhatsInVisionCone : MonoBehaviour
{
    public List<GameObject> objects;
    public List<VisionCone> visionCons;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        objects.Clear();
       foreach(VisionCone cone in visionCons)
        {
            foreach(GameObject obj in cone.objects)
            {
                objects.Add(obj);
            }
        } 
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        foreach (var obj in objects)
        {
            Gizmos.DrawSphere(obj.transform.position, 2f);
        }
    }
}

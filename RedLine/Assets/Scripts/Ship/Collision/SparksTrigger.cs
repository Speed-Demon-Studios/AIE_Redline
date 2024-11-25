using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SparksTrigger : MonoBehaviour
{
    [SerializeField] private SparksParticlesController m_spc;

    public GameObject[] sparks;
    public bool isColliding = false;
    public bool waiting = false;

    public void SetSPC(SparksParticlesController spcScript)
    {
        m_spc = spcScript;
    }

    public SparksParticlesController GetSPC()
    {
        return m_spc;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.ToLower() == "walls")
        {
            isColliding = true;
        }
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.tag.ToLower() == "walls")
        {
            //foreach (GameObject sparkParticle in sparks)
            //{
            //    //sparkParticle.SetActive(false);
            //    m_spc.DeactivateSparks(sparkParticle);
            //}
            isColliding = false;
        }
    }


}

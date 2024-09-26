using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SparksTrigger : MonoBehaviour
{
    private SparksParticlesController m_spc;


    //public int[] colliderIndexes;
    public ParticleSystem[] sparks;
    public bool isColliding = false;

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
            //if (colliderIndexes.Length < 2)
            //{
            //    m_spc.ActivateSparks(colliderIndexes[0]);
            //}
            //else if (colliderIndexes.Length >= 2)
            //{
            foreach (ParticleSystem sparkParticle in sparks)
            {
                m_spc.ActivateSparks(sparkParticle);
            }
            //}
            isColliding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //if (colliderIndexes.Length < 2)
        //{
            if (other.tag.ToLower() == "walls")
            {
            //    m_spc.DeactivateSparks(colliderIndexes[0]);
            //}
            //else if (colliderIndexes.Length >= 2)
            //{
                foreach (ParticleSystem sparkParticle in sparks)
                {
                    m_spc.DeactivateSparks(sparkParticle);
                    isColliding = false;
                }
            }
        //}    
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class SparksParticlesController : MonoBehaviour
{
    public SparksTrigger[] sparksList;
    public ParticleSystem[] sparksParticles;

    public void ActivateSparks(ParticleSystem particleToActivate)
    {
        ParticleSystem.MainModule mainModule = particleToActivate.main;
        mainModule.loop = true;
        if (particleToActivate.isPlaying == false)
        {
            mainModule.duration = 0.1f;
            particleToActivate.Play();
        }
    }

    public void DeactivateSparks(ParticleSystem particleToActivate)
    {
        ParticleSystem.MainModule mainModule = particleToActivate.main;
        mainModule.loop = false;
    }
    
    private IEnumerator DeactivateSPRKS(int particleToDeactivate)
    {
        ParticleSystem.MainModule mainModule = sparksParticles[particleToDeactivate - 1].main;
        mainModule.loop = false;

        yield return new WaitForEndOfFrame();
        StopCoroutine(DeactivateSPRKS(particleToDeactivate));
    }

    void Awake()
    {
        foreach (SparksTrigger sP in sparksList)
        {
            sP.SetSPC(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (SparksTrigger sT in sparksList)
        {
            if (sT != null)
            {
                if (sT.GetSPC() != null)
                {
                    if (sT.isColliding == true)
                    {
                        //foreach (ParticleSystem sparksPE in sT.sparks)
                        //{
                        //    ActivateSparks(sparksPE);
                        //}    
                    }
                }
            }
        }
    }
}

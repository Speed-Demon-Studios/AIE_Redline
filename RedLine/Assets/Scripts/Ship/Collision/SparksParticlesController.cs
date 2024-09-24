using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class SparksParticlesController : MonoBehaviour
{
    public SparksTrigger[] sparksList;
    public ParticleSystem[] sparksParticles;

    public void ActivateSparks(int particleToActivate)
    {
        ParticleSystem.MainModule mainModule = sparksParticles[particleToActivate - 1].main;
        if (sparksParticles[particleToActivate - 1].isPlaying == false)
        {
            mainModule.duration = 0.08f;
            sparksParticles[particleToActivate - 1].Play();
        }
    }

    public void DeactivateSparks(int particleToDeactivate)
    {
        ParticleSystem.MainModule mainModule = sparksParticles[particleToDeactivate - 1].main;
        sparksParticles[particleToDeactivate].Stop();
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
                        for (int i = 0; i < sT.colliderIndexes.Length; i++)
                        {
                            ActivateSparks(i);
                        }
                    }
                }
            }
        }
    }
}

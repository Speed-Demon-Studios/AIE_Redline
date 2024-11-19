using EAudioSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class SparksParticlesController : MonoBehaviour
{
    [HideInInspector] public PlayerAudioController PAC;

    public SparksTrigger[] sparksList;
    public ParticleSystem[] sparksParticles;

    public void ActivateSparks(GameObject particleToActivate)
    {
        if (particleToActivate != null && particleToActivate.activeSelf == false)
        {
            if (PAC.IsEmitterPlaying(1, 0) == false)
            {
                PAC.PlayGPSFX(0, 0);
            }
            particleToActivate.SetActive(true);
        }
    }

    public void DeactivateSparks(GameObject particleToDeactivate, SparksTrigger sT = null)
    {
        if (particleToDeactivate != null && sT != null && particleToDeactivate.activeSelf == true)
        {
            if (sT.waiting == false)
            {
                sT.waiting = true;
                StartCoroutine(DeactivateSPRKS(particleToDeactivate, sT));
            }
        }
    }
    
    private IEnumerator DeactivateSPRKS(GameObject particleToDeactivate, SparksTrigger sT)
    {
        yield return new WaitForEndOfFrame();

        particleToDeactivate.SetActive(false);

        sT.waiting = false;
        StopCoroutine(DeactivateSPRKS(particleToDeactivate, sT));
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
                        foreach (GameObject sparksPE in sT.sparks)
                        {
                            if (sparksPE != null && sT != null)
                            {
                                ActivateSparks(sparksPE);
                            }
                        }    
                    }
                    else if (sT.isColliding == false)
                    {
                        foreach (GameObject sparksPE in sT.sparks)
                        {
                            if (sparksPE != null)
                            {
                                DeactivateSparks(sparksPE, sT);
                            }
                        }
                    }
                }
            }
        }
    }
}

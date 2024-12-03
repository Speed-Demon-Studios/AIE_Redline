using EAudioSystem;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;
using UnityEngine.VFX;

public class SparksParticlesController : MonoBehaviour
{
    [HideInInspector] public PlayerAudioController PAC;

    public SparksTrigger[] sparksList;
    public ParticleSystem[] sparksParticles;
    public VisualEffect vfx;

    public void ActivateSparks(VisualEffect particleToActivate)
    {
        if (particleToActivate != null)
        {
            particleToActivate.Play();
            if (PAC.IsEmitterPlaying(1, 0) == false)
            {
                PAC.PlayGPSFX(0);
            }
        }
    }

    public void DeactivateSparks(VisualEffect particleToDeactivate, SparksTrigger sT = null)
    {
        if (particleToDeactivate != null && sT != null && particleToDeactivate.isActiveAndEnabled)
        {
            particleToDeactivate.Stop();
            if (sT.waiting == false)
            {
                sT.waiting = true;
                StartCoroutine(DeactivateSPRKS(particleToDeactivate, sT));
            }
        }
    }
    
    private IEnumerator DeactivateSPRKS(VisualEffect particleToDeactivate, SparksTrigger sT)
    {
        yield return new WaitForEndOfFrame();

        particleToDeactivate.Stop();

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
                        foreach (VisualEffect sparksPE in sT.sparks)
                        {
                            if (PAC != null && sparksPE != null && sparksPE.isActiveAndEnabled && sT != null && sT.isActiveAndEnabled)
                            {
                                this.ActivateSparks(sparksPE);
                            }
                        }
                    }
                    else if (sT.isColliding == false)
                    {
                        foreach (VisualEffect sparksPE in sT.sparks)
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

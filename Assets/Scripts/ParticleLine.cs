using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLine : MonoBehaviour
{
    ParticleSystem particlesSys;
    public ParticleSystem.Particle[] particles;
    public Transform target;

    void Start()
    {
        particlesSys = GetComponent<ParticleSystem>();
    }


    void LateUpdate()
    {

        ParticleSystem.Particle[] p = new ParticleSystem.Particle[particlesSys.particleCount + 1];
        int l = particlesSys.GetParticles(p);

        int i = 0;
        while (i < l)
        {
            p[i].velocity = new Vector3(0, p[i].remainingLifetime / p[i].startLifetime * 10F, 0);
            i++;
        }

        particlesSys.SetParticles(p, l);

    }
}

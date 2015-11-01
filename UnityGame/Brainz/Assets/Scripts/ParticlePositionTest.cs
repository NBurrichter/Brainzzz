using UnityEngine;
using System.Collections;

public class ParticlePositionTest : MonoBehaviour
{

    private ParticleSystem pSystem;

    void Awake()
    {
        pSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        ParticleSystem.Particle[] particleList = new ParticleSystem.Particle[pSystem.particleCount];
        pSystem.GetParticles(particleList);

        for (int i = 0; i < particleList.Length; i++)
        {
            Debug.DrawLine(transform.TransformPoint(particleList[i].position)/transform.lossyScale.x, transform.position, Color.red);

            pSystem.SetParticles(particleList, pSystem.particleCount);
        }

    }
}

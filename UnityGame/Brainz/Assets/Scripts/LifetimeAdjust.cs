using UnityEngine;
using System.Collections;

[ExecuteInEditMode()]
public class LifetimeAdjust : MonoBehaviour {

    public GameObject target;

    private ParticleSystem pSystem;
    private ParticleSystem.Particle[] particles;
    private float dist;

    void Awake()
    {
        pSystem = GetComponent<ParticleSystem>();
	}
	
	void Update ()
    {
        if (target != null)
        {
            dist = Vector3.Distance(transform.position, target.transform.position);
            pSystem.startLifetime = dist / pSystem.startSpeed;
            transform.LookAt(target.transform.position);
        }
        
        
        if (Application.isPlaying)
        {
            /*
            // GetParticles is allocation free because we reuse the m_Particles buffer between updates
            ParticleSystem.Particle[] numParticlesAlive = pSystem.GetParticles(particles);

            // Change only the particles that are alive
            for (var i = 0; i < numParticlesAlive; i++)
            {
                m_Particles[i].velocity += Vector3.up * m_Drift;
            }
            // Apply the particle changes to the particle system
            m_System.SetParticles(m_Particles, numParticlesAlive);

            for (int i = 0; i < particles.Length; i++)
            {
                if (Vector3.Distance(particles[i].position, transform.position) > dist)
                {
                    particles[i].lifetime = 0;
                }
            }
            */


            
        }
        /*
        foreach (ParticleSystem.Particle particle in particles)
        {
            if (Vector3.Distance(particle.position,transform.position) > dist)
            {
                particle.lifetime = 0;
            }
        }
        */
    }
}

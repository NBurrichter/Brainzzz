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
            //calculate Lifetime of Particle 
            dist = Vector3.Distance(transform.position, target.transform.position);
            float lifetime = dist / pSystem.startSpeed;
            pSystem.startLifetime = lifetime;
            transform.LookAt(target.transform.position);

            if (Application.isPlaying)
            {
                ParticleSystem.Particle[] particleList = new ParticleSystem.Particle[pSystem.particleCount];
                pSystem.GetParticles(particleList);

                for (int i = 0; i < particleList.Length; i++)
                {
                    if (Vector3.Distance(particleList[i].position,transform.position) > dist )
                    {
                        particleList[i].startLifetime = lifetime;
                    }
                    particleList[i].color = new Color(Random.Range(0.0f,1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
                }

                pSystem.SetParticles(particleList, pSystem.particleCount);
            }
        }

        //---TestCode---
         /*
         ParticleSystem m_currentParticleEffect = (ParticleSystem)GetComponent("ParticleSystem");
         ParticleSystem.Particle []ParticleList = new    ParticleSystem.Particle[m_currentParticleEffect.particleCount];
         m_currentParticleEffect.GetParticles(ParticleList);
         for(int i = 0; i < ParticleList.Length; ++i)
         {
             float LifeProcentage = (ParticleList[i].lifetime / ParticleList[i].startLifetime);
             ParticleList[i].color = Color.Lerp(Color.clear, Color.red, LifeProcentage);
         }   
         
         m_currentParticleEffect.SetParticles(ParticleList, m_currentParticleEffect.particleCount);
            */



        
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

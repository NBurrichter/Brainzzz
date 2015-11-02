using UnityEngine;
using System.Collections;

[ExecuteInEditMode()]
public class LifetimeAdjust : MonoBehaviour
{

    public enum ColorMode { RandomColorEveryFrame, RandomStartColor, PresetColor, };

    public GameObject target;

    public ColorMode colorMode = ColorMode.RandomColorEveryFrame;

    public Color[] presetColors;

    private ParticleSystem.Particle[] gizmoSpherePositions;
    private ParticleSystem pSystem;
    private ParticleSystem.Particle[] particles;
    private float dist;

    void Awake()
    {
        pSystem = GetComponent<ParticleSystem>();
    }

    void Update()
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
                    switch (colorMode)
                    {
                        case ColorMode.RandomColorEveryFrame:
                            {
                                particleList[i].color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
                                break;
                            }
                        case ColorMode.PresetColor:
                            {
                                if (particleList[i].color == Color.white)
                                {
                                    //particleList[i].color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
                                    particleList[i].color = presetColors[Random.Range(0,presetColors.Length)];                            
                                }
                                break;
                            }
                        case ColorMode.RandomStartColor:
                            {
                                if (particleList[i].color == Color.white)
                                {
                                    particleList[i].color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
                                    //particleList[i].color = presetColors[Random.Range(0,presetColors.Length)];                            
                                }
                                break;
                            }
                    }

                    if (Vector3.Distance(transform.TransformPoint(particleList[i].position), transform.position) > dist)
                    {
                        particleList[i].size = 0;
                        //Debug.DrawLine(transform.TransformPoint(particleList[i].position) / transform.lossyScale.x, Vector3.zero, Color.red);
                        //Gizmos.color = Color.red;
                        //Gizmos.DrawSphere(transform.TransformPoint(particleList[i].position) / transform.lossyScale.x,0.1f);
                    }
                    else
                    {
                        //Debug.DrawLine(transform.TransformPoint(particleList[i].position) / transform.lossyScale.x, Vector3.zero, Color.green);
                        //Gizmos.color = Color.green;
                        //Gizmos.DrawSphere(transform.TransformPoint(particleList[i].position) / transform.lossyScale.x, 0.1f);
                    }

                }

                GiveGizmoSpheres(particleList);

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

    void OnDrawGizmos()
    {
        if (target != null)
        {
            for (int i = 0; i < gizmoSpherePositions.Length; i++)
            {
                Gizmos.DrawSphere(transform.TransformPoint(gizmoSpherePositions[i].position), 0.1f);
            }
        }
    }

    void GiveGizmoSpheres(ParticleSystem.Particle[] sphereArray)
    {
        gizmoSpherePositions = sphereArray;
    }

    /*private Vector3 VectorMultiplication(Vector3 vec1, Vector3 vec2)
    {
        Vector3 product;

        product.x = vec1.x * vec2.x;
        product.x = vec1.y * vec2.y;
        product.x = vec1.z * vec2.z;

        return produ/ct;
    }*/

}

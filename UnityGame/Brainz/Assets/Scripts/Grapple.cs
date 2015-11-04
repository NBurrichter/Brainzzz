﻿using UnityEngine;
using System.Collections;

public class Grapple : MonoBehaviour
{

    private float fGrappleForce;
    public GameObject goPlayer;
    private GameObject goBlopOne;
    private GameObject goBlopTwo;
    private Rigidbody rb;
    public static Grapple Si_Grappple;

    private bool isGrappling;

    private float fGrappleSpeed = 7.5f;

    private IEnumerator grappleCoroutine;

    //grapple Particles
    private ParticleSystem grapplePaticleSystem;
    private GameObject grappleParticleObject;
    private float dist = 0;

    void Start()
    {
        Si_Grappple = this;
        goPlayer = this.gameObject;
        rb = GetComponent<Rigidbody>();
        isGrappling = false;

        foreach (Transform child in transform)
        {
            if (child.name == "GrappleParticle")
            {
                grappleParticleObject = child.gameObject;
            }
        }
        grapplePaticleSystem = grappleParticleObject.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        SearchBlops();
        int i = GetBlopToDragTo();

        if (Input.GetButtonDown("Grapple"))
        {

            //start grappeling

            if (i != 0 && !isGrappling)
            {
                grappleCoroutine = DragToBlop(i);
                goPlayer.GetComponent<PlayerControl>().SetGrapple(true);
                StartCoroutine(grappleCoroutine);
                isGrappling = true;
            }
            else
            {
                goPlayer.GetComponent<PlayerControl>().SetGrapple(false);
                StopCoroutine(grappleCoroutine);
                isGrappling = false;
                grapplePaticleSystem.Stop();
                grapplePaticleSystem.Clear();
            }

        }

        if(isGrappling)
        {
            // grapple to bBlop 1
            if (i == 1)
            {
                grappleParticleObject.transform.LookAt(goBlopOne.transform.position);
                dist = Vector3.Distance(transform.position, goBlopOne.transform.position);
                float lifetime = dist / grapplePaticleSystem.startSpeed;
                grapplePaticleSystem.startLifetime = lifetime;
                grapplePaticleSystem.Play(true);
            }
            // Grapple to Blop 2
            if (i == 2)
            {
                grappleParticleObject.transform.LookAt(goBlopTwo.transform.position);
                dist = Vector3.Distance(transform.position, goBlopTwo.transform.position);
                float lifetime = dist / grapplePaticleSystem.startSpeed;
                grapplePaticleSystem.startLifetime = lifetime;
                grapplePaticleSystem.Play(true);
            }

            
            ParticleSystem.Particle[] particleList = new ParticleSystem.Particle[grapplePaticleSystem.particleCount];
            grapplePaticleSystem.GetParticles(particleList);
            for (int n = 0; n < particleList.Length; n++)
            {
                Debug.DrawLine(grappleParticleObject.transform.TransformPoint(particleList[n].position), Vector3.zero);
                if (Vector3.Distance(grappleParticleObject.transform.TransformPoint(particleList[n].position), transform.position) > dist)
                {
                    
                    particleList[n].size = 0;

                }
            }

            grapplePaticleSystem.SetParticles(particleList, grapplePaticleSystem.particleCount);

        }

        if (i == 0)
        {
            if (grappleCoroutine != null)
            {
                goPlayer.GetComponent<PlayerControl>().SetGrapple(false);
                StopCoroutine(grappleCoroutine);
            }
        }

    }


    // search for the two blops
    private void SearchBlops()
    {
        try
        {
            goBlopOne = GameObject.FindGameObjectWithTag("Blop1");
        }
        catch (UnityException e)
        {
            goBlopOne = null;
        }

        try
        {
            goBlopTwo = GameObject.FindGameObjectWithTag("Blop2");
        }
        catch (UnityException e)
        {
            goBlopOne = null;
        }
    }


    // return which Blop should be grappled to 1=Blop1, 2=Blop2, 0= no Blop
    private int GetBlopToDragTo()
    {
        // check for Blop one and if he has an attachment
        if (goBlopOne != null && goBlopTwo == null && goBlopOne.GetComponent<Blop1Control>().HasAttachedObject() == true)
        {
            return 1;
        }
        else
        {
            // check for Blop two and if he has an attachment
            if (goBlopOne == null && goBlopTwo != null && goBlopTwo.GetComponent<Blop2Control>().HasAttachedObject() == true)
            {
                return 2;
            }
            else
            {
                return 0;
            }
        }

    }

    // Coroutine to handle the grapple
    IEnumerator DragToBlop(int i)
    {
        float timeSinceStart = 1f;

        while (true)
        {


            timeSinceStart += Time.deltaTime;
            if (i == 1)
            {

                //Drag to Blop1
                Vector3 dir = goBlopOne.transform.position - goPlayer.transform.position;
                dir = dir.normalized;
                rb.velocity = dir * fGrappleSpeed;
                Debug.DrawLine(goPlayer.transform.position, goBlopOne.transform.position + dir, Color.red);
            }
            else
            {
                // let the player move to the position of the blop2
                Vector3 dir = goBlopTwo.transform.position - goPlayer.transform.position;
                dir = dir.normalized;
                rb.velocity = dir * fGrappleSpeed;
                Debug.DrawLine(goPlayer.transform.position, goBlopTwo.transform.position + dir, Color.red);
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    //Stop corotine if collision with a blop or its attachment
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Blop1" || col.gameObject.tag == "Blop2" ||
           col.gameObject.tag == "Blop1_Attachment" || col.gameObject.tag == "Blop2_Attachment")
        {
            if (grappleCoroutine != null)
            {
                StopCoroutine(grappleCoroutine);
                isGrappling = false;
                grapplePaticleSystem.Stop();
                grapplePaticleSystem.Clear();
            }
            goPlayer.GetComponent<PlayerControl>().SetGrapple(false);
            grappleCoroutine = null;

        }
    }

}

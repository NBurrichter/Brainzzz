using UnityEngine;
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

    private IEnumerator grappleCoroutine;

    //grapple Particles
    private ParticleSystem grapplePaticleSystem;
    private GameObject grappleParticleObject;

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
            if (i == 1)
            {
                grappleParticleObject.transform.LookAt(goBlopOne.transform.position);
                float dist = Vector3.Distance(transform.position, goBlopOne.transform.position);
                float lifetime = dist / grapplePaticleSystem.startSpeed;
                grapplePaticleSystem.startLifetime = lifetime;
                grapplePaticleSystem.Play(true);
            }
            if (i == 2)
            {
                grappleParticleObject.transform.LookAt(goBlopTwo.transform.position);
                float dist = Vector3.Distance(transform.position, goBlopTwo.transform.position);
                float lifetime = dist / grapplePaticleSystem.startSpeed;
                grapplePaticleSystem.startLifetime = lifetime;
                grapplePaticleSystem.Play(true);
            }
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

    private int GetBlopToDragTo()
    {
        if (goBlopOne != null && goBlopTwo == null)
        {
            return 1;
        }
        else
        {
            if (goBlopOne == null && goBlopTwo != null)
            {
                return 2;
            }
            else
            {
                return 0;
            }
        }

    }


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
                rb.velocity = dir;
                Debug.DrawLine(goPlayer.transform.position, goBlopOne.transform.position + dir, Color.red);
            }
            else
            {
                // let the player move to the position of the blop2
                Vector3 dir = goBlopTwo.transform.position - goPlayer.transform.position;
                rb.velocity = dir;
                Debug.DrawLine(goPlayer.transform.position, goBlopTwo.transform.position + dir, Color.red);
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

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

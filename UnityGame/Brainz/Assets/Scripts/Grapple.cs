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

	void Start ()
	{
		Si_Grappple = this;
        goPlayer = this.gameObject;
        rb = GetComponent<Rigidbody>();
        isGrappling = false;
	}

	void Update ()
	{
		SearchBlops ();
		int i = GetBlopToDragTo();

		if (Input.GetButtonDown ("Grapple")) 
		{

			//start grappeling

			if(i!=0 && !isGrappling)
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
            }

		}

		if(i==0)
		{
			if(grappleCoroutine!=null)
			{
				goPlayer.GetComponent<PlayerControl>().SetGrapple(false);
			    StopCoroutine(grappleCoroutine);
			}
		}

	}

	private void SearchBlops ()
	{
		try 
		{
			goBlopOne = GameObject.FindGameObjectWithTag ("Blop1");
		} 
		catch (UnityException e) 
		{
			goBlopOne = null;
		}

		try 
		{
			goBlopTwo = GameObject.FindGameObjectWithTag ("Blop2");
		} 
		catch (UnityException e) 
		{
			goBlopOne = null;
		}
	}

	private int GetBlopToDragTo ()
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


	IEnumerator DragToBlop (int i)
	{
		float timeSinceStart = 1f;

		while (true) 
		{


			timeSinceStart += Time.deltaTime;
			if(i==1)
			{

                //Drag to Blop1
                Vector3 dir = goBlopOne.transform.position - goPlayer.transform.position;
                rb.velocity=dir;
                Debug.DrawLine(goPlayer.transform.position, goBlopOne.transform.position + dir, Color.red);
			}
			else{
                // let the player move to the position of the blop2
                Vector3 dir = goBlopOne.transform.position - goPlayer.transform.position;
                rb.AddForce(dir*100,ForceMode.VelocityChange);
            }
			
			yield return new WaitForSeconds(0.1f);
		}
	}

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "Blop1" || col.gameObject.tag == "Blop2"||
		   col.gameObject.tag == "Blop1_Attachment" || col.gameObject.tag == "Blop2_Attachment")
		{
			if (grappleCoroutine != null)
			{
				StopCoroutine(grappleCoroutine);
                isGrappling = false;
			}
			goPlayer.GetComponent<PlayerControl>().SetGrapple(false);
			grappleCoroutine = null;

		}
	}

}

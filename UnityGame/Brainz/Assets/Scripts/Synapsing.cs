using UnityEngine;
using System.Collections;

public class Synapsing : MonoBehaviour
{
	[SerializeField]
	private float merginForce;

	public PhysicMaterial noFrictionMaterial;
	public float blopMass;
	public int noCollisionLayer;

	GameObject Blop1;
	GameObject Blop2;

    Blop1Control Blop1Script;
    Blop2Control Blop2Script;

	private bool bMergeEnabled;
	private IEnumerator merginCoroutine;

	private Rigidbody blopOneBody;
	private Rigidbody blopTwoBody;

	public static Synapsing Singleton;

	// Use this for initialization
	void Start ()
	{
		Singleton= this;
		bMergeEnabled = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetButtonDown ("Mergin")) 
		{
			SearchForBlops();

            LifetimeAdjust particleScript1 = Blop1.GetComponentInChildren<LifetimeAdjust>();
            particleScript1.target = Blop2;
            LifetimeAdjust particleScript2 = Blop2.GetComponentInChildren<LifetimeAdjust>();
            particleScript2.target = Blop1;
            ParticleSystem particleSystem1 = Blop1.GetComponentInChildren<ParticleSystem>();
            particleSystem1.Play();
            ParticleSystem particleSystem2 = Blop2.GetComponentInChildren<ParticleSystem>();
            particleSystem2.Play();


            if (bMergeEnabled == false) {
				bMergeEnabled = true;
			}
		}

		if (bMergeEnabled == true && merginCoroutine == null) {
			merginCoroutine = Mergin ();
			StartCoroutine(merginCoroutine);
		}
			
	}

	private void SearchForBlops()
	{
		if (Blop1 && Blop2)
		{
			return;
		}

		try {	
			Blop1 = GameObject.FindGameObjectWithTag ("Blop1");
			Blop2 = GameObject.FindGameObjectWithTag ("Blop2");

            Blop1Script = Blop1.GetComponent<Blop1Control>();
            Blop2Script = Blop2.GetComponent<Blop2Control>();

			blopOneBody = Blop1.GetComponent<Rigidbody>();
			blopTwoBody = Blop2.GetComponent<Rigidbody>();
			
		} catch (UnityException e) {
            Debug.Log("Blop1 + Blop 2 :" + e);
			Blop1 = null;
			Blop2 = null;

			print ("No two Blops");
		}
	}

	IEnumerator Mergin ()
	{
		float timeSinceStart = 1f;

		while (true) 
		{
			timeSinceStart += Time.deltaTime;

            // check if Blops are existing and they have an attachment
			if (Blop1 != null && Blop2 != null && Blop1Script.HasAttachedObject() == true && Blop2Script.HasAttachedObject() == true  ) {
            

				Vector3 dir = Blop1.transform.position - Blop2.transform.position;

				blopOneBody.AddForce(-dir * merginForce * timeSinceStart);
				blopTwoBody.AddForce(dir * merginForce * timeSinceStart);
			}

			yield return new WaitForSeconds(0.1f);
		}
	}

	public void StopMergin ()
	{
		bMergeEnabled = false;

		if (merginCoroutine != null)
		{
		StopCoroutine(merginCoroutine);
		}

			merginCoroutine = null;
	}
}

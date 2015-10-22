using UnityEngine;
using System.Collections;

public class Blop1_Control : MonoBehaviour
{
	
	public GameObject Blop;

	private FixedJoint attachedObject;
	
	Rigidbody rb;
	
	// Use this for initialization
	void Start ()
	{
		Blop = this.gameObject;
		rb = GetComponent<Rigidbody> ();
		rb.velocity = new Vector3 (0, 5, 5);
		Blop.tag = "Blop1";
	}

	void OnCollisionEnter(Collision c)
	{
		if (!attachedObject && !c.gameObject.CompareTag("Player") && !c.gameObject.CompareTag("Ground"))
		{
			//Attachment att = c.gameObject.AddComponent<Attachment>();
			//att.PseudoParent = transform;
			//rb.isKinematic = true;


			// Add Tag and Components to the attachement
			c.gameObject.tag = "Blop1_Attachment";

			Rigidbody otherBody;
			
			// check if There is already a Rigidbody
			if(c.gameObject.GetComponent<Rigidbody>())
			{
				otherBody = c.gameObject.GetComponent<Rigidbody>();
			}
			else{
				otherBody = c.gameObject.AddComponent<Rigidbody> ();
			}
			attachedObject = c.gameObject.AddComponent<FixedJoint>();
			attachedObject.connectedBody = rb;

			//Set layer so it cannot collide with other attached object(s)
			c.gameObject.layer = Synapsing.Singleton.noCollisionLayer;
			gameObject.layer = Synapsing.Singleton.noCollisionLayer;

			//Set physic material of other collider
			c.gameObject.GetComponent<Collider>().material = Synapsing.Singleton.noFrictionMaterial;

			//Set drag to 0
			rb.drag = 0;
			rb.mass = 0;
			otherBody.drag = 0;

			otherBody.mass = Synapsing.Singleton.blopMass;
		}
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag == "Blop2") 
		{
			this.gameObject.transform.DetachChildren ();
			Synapsing.Singleton.StopMergin ();
			Destroy (this.gameObject);
			attachedObject.tag="Untagged";
			Destroy(attachedObject);
			
			//Re-enable collision between attached objects
			attachedObject.gameObject.layer = 0;
		}
	}
}

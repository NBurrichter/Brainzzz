﻿using UnityEngine;
using System.Collections;

public class Blop2_control : MonoBehaviour
{
	Rigidbody rb;

	private FixedJoint attachedObject;

	// Use this for initialization
	void Start ()
	{
		this.gameObject.tag = "Blop2";
		rb = GetComponent<Rigidbody> ();
        rb.velocity = Aiming_control.aimingControlSingleton.GetHitDirection();
    }
	
	// Update is called once per frame
	void Update ()
	{
        if (attachedObject != null)
        {
            if (attachedObject.GetComponent<cube_control>().StopMergin() == true)
                StopMergin();
        }
	}



	void OnCollisionEnter (Collision c)
	{
		if (!attachedObject && !c.gameObject.CompareTag ("Player") && !c.gameObject.CompareTag ("Ground")) {
			//Attachment att = c.gameObject.AddComponent<Attachment>();
			//att.PseudoParent = transform;
			//rb.isKinematic = true;



			// Add Tag and Components to the attachement
			c.gameObject.tag = "Blop2_Attachment";
            c.gameObject.GetComponent<cube_control>().SetMergin(true);
            Rigidbody otherBody;

			// check if There is already a Rigidbody
			if(c.gameObject.GetComponent<Rigidbody>())
			{
				otherBody = c.gameObject.GetComponent<Rigidbody>();
			}
			else{
			otherBody = c.gameObject.AddComponent<Rigidbody> ();
			}
			attachedObject = c.gameObject.AddComponent<FixedJoint> ();
			attachedObject.connectedBody = rb;
			
			/*Set layer so it cannot collide with other attached object(s)
			c.gameObject.layer = Synapsing.Singleton.noCollisionLayer;*/

			//Set physic material of other collider
			c.gameObject.GetComponent<Collider> ().material = Synapsing.Singleton.noFrictionMaterial;

			//Set drag to 0
			rb.drag = 0;
			rb.mass = 0;
			otherBody.drag = 0;

			otherBody.mass = Synapsing.Singleton.blopMass;
		}

        
	}

    public void StopMergin()
    {
        this.gameObject.transform.DetachChildren();
        Synapsing.Singleton.StopMergin();
        Destroy(this.gameObject);
        attachedObject.tag = "Untagged";
        Destroy(attachedObject);

        //Re-enable collision between attached objects
        attachedObject.gameObject.layer = 0;
    }

	void OnTriggerEnter (Collider collider)
	{

		if (collider.gameObject.tag == "Blop1") {
            StopMergin();
		}

        if (collider.gameObject.tag == "Blop1_Attachment")
        {
            StopMergin();
            GameObject go = GameObject.FindGameObjectWithTag("Blop1");
            go.GetComponent<Blop1_Control>().StopMergin();
        }
    }
}

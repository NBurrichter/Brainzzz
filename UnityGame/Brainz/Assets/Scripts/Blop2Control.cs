using UnityEngine;
using System.Collections;

public class Blop2Control : MonoBehaviour
{
    Rigidbody rb;
    private GameObject[] goBlop2Array;
    private FixedJoint attachedObject;

    private GameObject particleObject;

    // Use this for initialization
    void Start()
    {
        this.gameObject.tag = "Blop2";
        rb = GetComponent<Rigidbody>();
        rb.velocity = AimingControl.aimingControlSingleton.GetHitDirection();
        goBlop2Array = GameObject.FindGameObjectsWithTag("Blop2");
        foreach (Transform child in transform)
        {
            if (child.name == "LineEffect")
            {
                particleObject = child.gameObject;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (attachedObject != null)
        {
            if (attachedObject.GetComponent<CubeControl>().GetMerginStatus() == true)
                StopMergin();
        }

        if (Input.GetAxis("XBox Trigger") == 1)
        {
            for (int i = 0; i <= goBlop2Array.Length - 1; i++)
            {
                if (goBlop2Array[i] != null)
                    goBlop2Array[i].GetComponent<Blop2Control>().FreeOtherBlops();
            }
        }
    }



    void OnCollisionEnter(Collision c)
    {
        if (!attachedObject && !c.gameObject.CompareTag("Player") && !c.gameObject.CompareTag("Ground"))
        {

            if (c.gameObject.GetComponent<CubeControl>() == null)
            {
                Destroy(this.gameObject);
                return;
            }
                

            DeletePreviousBlops();



            // Add Tag and Components to the attachement
            c.gameObject.tag = "Blop2_Attachment";
            Rigidbody otherBody;

            // check if There is already a Rigidbody
            if (c.gameObject.GetComponent<Rigidbody>())
            {
                otherBody = c.gameObject.GetComponent<Rigidbody>();
            }
            else
            {
                otherBody = c.gameObject.AddComponent<Rigidbody>();
            }
            attachedObject = c.gameObject.AddComponent<FixedJoint>();
            attachedObject.connectedBody = rb;

            /*Set layer so it cannot collide with other attached object(s)
			c.gameObject.layer = Synapsing.Singleton.noCollisionLayer;*/

            //Set physic material of other collider
            c.gameObject.GetComponent<Collider>().material = Synapsing.Singleton.noFrictionMaterial;

            //Set drag to 0
            rb.drag = 0;
            rb.mass = 0;
            otherBody.drag = 0;

            otherBody.mass = Synapsing.Singleton.blopMass;
        }


    }

    public void StopMergin()
    {
        attachedObject.GetComponent<CubeControl>().StopMergin();
        this.gameObject.transform.DetachChildren();
        Synapsing.Singleton.StopMergin();
        Destroy(this.gameObject);
        attachedObject.tag = "Untagged";
        Destroy(attachedObject);

        //Re-enable collision between attached objects
        attachedObject.gameObject.layer = 0;
    }

    void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.tag == "Blop1")
        {
            StopMergin();
        }

        if (collider.gameObject.tag == "Blop1_Attachment")
        {
            StopMergin();
            GameObject go = GameObject.FindGameObjectWithTag("Blop1");
            go.GetComponent<Blop1Control>().StopMergin();
        }
    }


    private void DeletePreviousBlops()
    {
        for (int i = 0; i <= goBlop2Array.Length - 2; i++)
        {
            if(goBlop2Array[i]!=null)
            goBlop2Array[i].GetComponent<Blop2Control>().FreeOtherBlops();
            
        }
    }

    public void FreeOtherBlops()
    {
        this.gameObject.transform.DetachChildren();
        Destroy(this.gameObject);
        if (attachedObject)
        {
            attachedObject.tag = "Untagged";
            // Destroy(attachedObject);

            //Re-enable collision between attached objects
            attachedObject.gameObject.layer = 0;
        }

    }

    void OnDestroy()
    {
        Destroy(particleObject);
    }

    public bool HasAttachedObject()
    {
        if (attachedObject != null)
            return true;
        return false;
    }
}

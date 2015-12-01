using UnityEngine;
using System.Collections;

public class Blop1Control : MonoBehaviour
{
	
	public GameObject Blop;
    // Array to save the previous Blops
    private GameObject[] goBlop1Array;
	private FixedJoint attachedObject;

    private GameObject particleObject;

    new Vector3 vMoveDirection;

	Rigidbody rb;
	
	// Use this for initialization
	void Start ()
	{
		Blop = this.gameObject;
		rb = GetComponent<Rigidbody> ();
        vMoveDirection = AimingControl.aimingControlSingleton.GetHitDirection(); 
		Blop.tag = "Blop1";
        goBlop1Array = GameObject.FindGameObjectsWithTag("Blop1");
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
        if(attachedObject==null)
            transform.position += vMoveDirection * Time.smoothDeltaTime;

        if (attachedObject != null)
        {
            if (attachedObject.GetComponent<CubeControl>().GetMerginStatus() == true)
            {
                StopMergin();
            }
        }

        if (Input.GetAxis("XBox Trigger") == -1)
        {
            for (int i = 0; i <= goBlop1Array.Length - 1; i++)
            {
                if (goBlop1Array[i] != null)
                    goBlop1Array[i].GetComponent<Blop1Control>().DestroyThisBlop();
            }
        }
    }
          

    

    void OnCollisionEnter(Collision c)
	{

		if (!attachedObject && !c.gameObject.CompareTag("Player") && !c.gameObject.CompareTag("Ground") &&
            !c.gameObject.CompareTag("Blop1") && !c.gameObject.CompareTag("Blop2"))
        {
            if (c.gameObject.GetComponent<CubeControl>() == null)
            {
                Destroy(this.gameObject);
                return;
            }


            // Delete previous Blops
            DeletePreviousBlops();
            

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


			//Set physic material of other collider
			c.gameObject.GetComponent<Collider>().material = Synapsing.Singleton.noFrictionMaterial;

			//Set drag to 0
			rb.drag = 0;
			rb.mass = 0; // the mass should in the end not be lowered, search for a better solution
			otherBody.drag = 0;

			otherBody.mass = Synapsing.Singleton.fBlopMass;

            //Check if Block is a NPC
            if (c.gameObject.GetComponent<CubeControl>().blocktype == CubeControl.BlockType.NPC)
            {
                c.gameObject.GetComponent<NavmeshTestNavigation>().SetActivationMode(false);
            }
            if (c.gameObject.GetComponent<CubeControl>().blocktype == CubeControl.BlockType.NPCAStar)
            {
                c.gameObject.GetComponent<FindTestPath>().enabled = false;
                c.gameObject.GetComponent<Seeker>().enabled = false;
                c.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                c.gameObject.GetComponent<CharacterController>().enabled = false;
            }
        }
	}

    /// <summary>
    /// call this when the mergin is finished
    /// </summary>
    public void StopMergin()
    {
        attachedObject.GetComponent<CubeControl>().StopMergin();
        this.gameObject.transform.DetachChildren();
        Synapsing.Singleton.StopMergin();
        Destroy(this.gameObject);
        attachedObject.tag = "Untagged";
        Destroy(attachedObject);


    }


    void OnTriggerEnter(Collider collider)
	{
        

        if (attachedObject != null)
        {
            if (collider.gameObject.tag == "Blop2")
            {
                StopMergin();
            }

            if (collider.gameObject.tag == "Blop2_Attachment")
            {
                StopMergin();
                GameObject go = GameObject.FindGameObjectWithTag("Blop2");
                go.GetComponent<Blop2Control>().StopMergin();
            }
        }
        else
        {
            if (collider.gameObject.tag == "Blop2_Attachment")
            {
                GameObject goBlop2 = GameObject.FindGameObjectWithTag("Blop2");
                goBlop2.GetComponent<Blop2Control>().StopMergin();
            }
        }
	 }

    void OnCollisionStay(Collision col)
    {
        if (attachedObject != null)
        {
            if (col.gameObject.tag == "Blop2")
            {
                StopMergin();
            }

            if (col.gameObject.tag == "Blop2_Attachment")
            {
                StopMergin();
                GameObject go = GameObject.FindGameObjectWithTag("Blop2");
                go.GetComponent<Blop2Control>().StopMergin();
            }
        }
        else
        {
            if (col.gameObject.tag == "Blop2_Attachment")
            {
                GameObject goBlop2 = GameObject.FindGameObjectWithTag("Blop2");
                goBlop2.GetComponent<Blop2Control>().StopMergin();
            }
        }

    }

    /// <summary>
    /// Delete all Blops that were shot previously
    /// </summary>
    private void DeletePreviousBlops()
    {
        for (int i = 0;i<=goBlop1Array.Length-2;i++)
        {
            if (goBlop1Array[i] != null)
                goBlop1Array[i].GetComponent<Blop1Control>().DestroyThisBlop();

        }
    }


    /// <summary>
    /// Destroy the Blops and releases all connections it had with other objects
    /// </summary>
    public void DestroyThisBlop()
    {
        this.gameObject.transform.DetachChildren();
        Destroy(this.gameObject);
        if (attachedObject)
        {
            if(attachedObject.gameObject.GetComponent<CubeControl>().blocktype == CubeControl.BlockType.NPC)
            {
                Debug.Log("luke ich bin dein Vater");
                attachedObject.gameObject.GetComponent<NavmeshTestNavigation>().SetActivationMode(true);
            }
            if (attachedObject.gameObject.GetComponent<CubeControl>().blocktype == CubeControl.BlockType.NPCAStar)
            {
                Debug.Log("Meep");
                attachedObject.gameObject.GetComponent<FindTestPath>().enabled = true;
                attachedObject.gameObject.GetComponent<FindTestPath>().Start();
                attachedObject.gameObject.GetComponent<Seeker>().enabled = true;
                attachedObject.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                attachedObject.gameObject.GetComponent<CharacterController>().enabled = true;
            }

            attachedObject.tag = "Untagged";
            Destroy(attachedObject);


            if (Grapple.Si_Grappple.IsGrappling() == true)
            {
                Grapple.Si_Grappple.StopGrapple();
            }

        }


    }

    /// <summary>
    /// called when Blop gets Destroyed
    /// </summary>
    void OnDestroy()
    {
        Destroy(particleObject);
    }


    /// <summary>
    /// check if the Blop has an attachment
    /// </summary>
    /// <returns></returns>
    public bool HasAttachedObject()
    {
        if (attachedObject != null)
            return true;
        return false;
    }

    
}

using UnityEngine;
using System.Collections;

public class Blop2Control : MonoBehaviour
{
    Rigidbody rb;
    private GameObject[] goBlop2Array;
    private FixedJoint attachedObject;

    private GameObject particleObject;

    // Use this for initialization
    public void Start()
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
                    goBlop2Array[i].GetComponent<Blop2Control>().DestroyThisBlop();
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
                
            // Destroy previous Blops
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


            //Set physic material of other collider
            c.gameObject.GetComponent<Collider>().material = Synapsing.Singleton.noFrictionMaterial;

            //Set drag to 0
            rb.drag = 0;
            rb.mass = 0;
            otherBody.drag = 0;

            otherBody.mass = Synapsing.Singleton.blopMass;

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
    /// handles when the mergin is finíshed or stoppped
    /// </summary>
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
        if (collider.gameObject.tag == "Blop1_Attachment")
        {
            GameObject goBlop1 = GameObject.FindGameObjectWithTag("Blop1");
            goBlop1.GetComponent<Blop1Control>().StopMergin();
        }

        if (attachedObject != null)
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
    }

    /// <summary>
    /// Deletes the previous shot Blops
    /// </summary>
    private void DeletePreviousBlops()
    {
        for (int i = 0; i <= goBlop2Array.Length - 2; i++)
        {
            if(goBlop2Array[i]!=null)
            goBlop2Array[i].GetComponent<Blop2Control>().DestroyThisBlop();
            
        }
    }


    /// <summary>
    /// Destroys the Blop and releases all connections it has with other objects
    /// </summary>
    public void DestroyThisBlop()
    {
        this.gameObject.transform.DetachChildren();
        Destroy(this.gameObject);
        if (attachedObject)
        {
            if (attachedObject.gameObject.GetComponent<CubeControl>().blocktype == CubeControl.BlockType.NPC)
            {
                Debug.Log("luke ich bin dein Vater");
                attachedObject.gameObject.GetComponent<NavmeshTestNavigation>().SetActivationMode(true);
            }
            if (attachedObject.gameObject.GetComponent<CubeControl>().blocktype == CubeControl.BlockType.NPCAStar)
            {
                attachedObject.gameObject.GetComponent<FindTestPath>().enabled = true;
                attachedObject.gameObject.GetComponent<FindTestPath>().Start();
                attachedObject.gameObject.GetComponent<Seeker>().enabled = true;
                attachedObject.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                attachedObject.gameObject.GetComponent<CharacterController>().enabled = true;
            }

            attachedObject.tag = "Untagged";
            Destroy(attachedObject);

            //Re-enable collision between attached objects
            attachedObject.gameObject.layer = 0;
        }

    }


    /// <summary>
    /// callled when Blop gets destroyed
    /// </summary>
    void OnDestroy()
    {
        Destroy(particleObject);
    }


    /// <summary>
    /// check if Blop has an attachment
    /// </summary>
    /// <returns></returns>
    public bool HasAttachedObject()
    {
        if (attachedObject != null)
            return true;
        return false;
    }
}

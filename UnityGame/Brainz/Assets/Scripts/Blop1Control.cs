using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Blop1Control : MonoBehaviour
{
    
    public GameObject Blop;
    // Array to save the previous Blops
    private GameObject[] goBlop1Array;
    private FixedJoint attachedObject;

    private GameObject particleObject;
    private GameObject sparkObjcet;

    private Vector3 vMoveDirection;

    private List<GameObject> listGameObjectsInTrigger = new List<GameObject>();
    private List<bool> listPreviousKinematicStatus = new List<bool>();

    Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        Blop = this.gameObject;
        rb = GetComponent<Rigidbody>();
        //vMoveDirection = AimingControl.aimingControlSingleton.GetHitDirection();
        rb.velocity = AimingControl.aimingControlSingleton.GetHitDirection();
        Blop.tag = "Blop1";
        goBlop1Array = GameObject.FindGameObjectsWithTag("Blop1");
        foreach (Transform child in transform)
        {
            if (child.name == "LineEffect")
            {
                particleObject = child.gameObject;
            }
            if (child.name == "FX_Lightning 1")
            {
                sparkObjcet = child.gameObject;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (attachedObject == null)
        {
            transform.position += vMoveDirection * Time.smoothDeltaTime;
            Debug.DrawLine(Vector3.zero, transform.position, Color.black, 2);
        }


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
        

        if (c.gameObject.name != "Player" && c.gameObject.name != "Player (1)")
        {
            // sets the velocity to zero
            rb.velocity = Vector3.zero;
            
        }
        

        for (int i = 0; i < listGameObjectsInTrigger.Count; i++)
        {
            
            // Resets the kinematicstates of all listet Objects
            listGameObjectsInTrigger[i].GetComponent<Rigidbody>().isKinematic = listPreviousKinematicStatus[i];
            
        }

        if (attachedObject != null)
        {
            if (c.gameObject.tag == "Blop2")
            {
                StopMergin();
            }

            if (c.gameObject.tag == "Blop2_Attachment")
            {
                StopMergin();
                GameObject go = GameObject.FindGameObjectWithTag("Blop2");
                go.GetComponent<Blop2Control>().StopMergin();
            }
        }
        else
        {
            if (c.gameObject.tag == "Blop2_Attachment")
            {
                
                GameObject goBlop2 = GameObject.FindGameObjectWithTag("Blop2");
                goBlop2.GetComponent<Blop2Control>().StopMergin();
            }
        }



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
            if (c.gameObject.GetComponent<Rigidbody>())
            {
                otherBody = c.gameObject.GetComponent<Rigidbody>();
            }
            else
            {
                otherBody = c.gameObject.AddComponent<Rigidbody>();
            }

            if (c.gameObject.GetComponent<CubeControl>().blocktype == CubeControl.BlockType.NPCAStar)
            {
                transform.position = GameObject.Find("BlopPoint").transform.position;
            }

            attachedObject = c.gameObject.AddComponent<FixedJoint>();
            attachedObject.connectedBody = rb;


            //Set physic material of other collider
            c.gameObject.GetComponentInChildren<Collider>().material = Synapsing.Singleton.noFrictionMaterial;
            

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
                c.gameObject.GetComponent<FindTestPath>().GetBlop();
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
        
        //add boolean or similare to avoid an object being saved multiple times
        if (attachedObject == null)
        {
            if (collider.gameObject.GetComponent<CubeControl>() != null)
            {

                if (collider.GetComponent<CubeControl>().blocktype == CubeControl.BlockType.NPCAStar)
                {
                    return;
                }

                for (int i = 0; i < listGameObjectsInTrigger.Count; i++)
                {
                    // Return if object is already in list

                    if (listGameObjectsInTrigger[i].name == collider.gameObject.name)
                        return;
                }

                listGameObjectsInTrigger.Add(collider.gameObject);

                listPreviousKinematicStatus.Add(collider.gameObject.GetComponent<Rigidbody>().isKinematic);

                collider.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            }
            else if (collider.gameObject.transform.parent != null)
            {
                if (collider.gameObject.transform.parent.GetComponent<CubeControl>() != null)
                {

                    if (collider.gameObject.transform.parent.GetComponent<CubeControl>().blocktype == CubeControl.BlockType.NPCAStar)
                    {
                        return;
                    }

                    for (int i = 0; i < listGameObjectsInTrigger.Count; i++)
                    {
                        // Return if object is already in list

                        if (listGameObjectsInTrigger[i].name == collider.gameObject.transform.parent.gameObject.name)
                            return;
                    }

                    listGameObjectsInTrigger.Add(collider.gameObject.transform.parent.gameObject);

                    listPreviousKinematicStatus.Add(collider.gameObject.transform.parent.GetComponent<Rigidbody>().isKinematic);

                    collider.gameObject.GetComponentInParent<Rigidbody>().isKinematic = true;
                }
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
        for (int i = 0; i <= goBlop1Array.Length - 2; i++)
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
            if (attachedObject.gameObject.GetComponent<CubeControl>().blocktype == CubeControl.BlockType.NPC)
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
        Destroy(sparkObjcet);
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

    public GameObject GetAttachedObject()
    {
        if (attachedObject == null)
            return null;
        return attachedObject.gameObject;
    }

}

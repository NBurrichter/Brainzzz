using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Blop2Control : MonoBehaviour
{
    Rigidbody rb;
    private GameObject[] goBlop2Array;
    private FixedJoint attachedObject;

    private GameObject particleObject;

    private Vector3 vMoveDirection;

    public Material matBlop2;

    private List<GameObject> listGameObjectsInTrigger = new List<GameObject>();
    private List<bool> listPreviousKinematicStatus = new List<bool>();

    // Use this for initialization
    public void Start()
    {
        this.gameObject.tag = "Blop2";
        rb = GetComponent<Rigidbody>();
        vMoveDirection = AimingControl.aimingControlSingleton.GetHitDirection();
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
        if (attachedObject == null)
            transform.position += vMoveDirection * Time.smoothDeltaTime;

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
        for (int i = 0; i < listGameObjectsInTrigger.Count; i++)
        {

            listGameObjectsInTrigger[i].GetComponent<Rigidbody>().isKinematic = listPreviousKinematicStatus[i];
        }


        if (attachedObject != null)
        {
            if (c.gameObject.tag == "Blop1")
            {
                StopMergin();
            }

            if (c.gameObject.tag == "Blop1_Attachment")
            {
                StopMergin();
                GameObject go = GameObject.FindGameObjectWithTag("Blop1");
                go.GetComponent<Blop1Control>().StopMergin();
            }
        }
        else
        {
            if (c.gameObject.tag == "Blop1_Attachment")
            {
                GameObject goBlop1 = GameObject.FindGameObjectWithTag("Blop1");
                goBlop1.GetComponent<Blop1Control>().StopMergin();
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
                
            // Destroy previous Blops
            DeletePreviousBlops();

            BlopGun.BlopGunSingelton.ChangeTexture(matBlop2);

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
            //c.gameObject.GetComponent<Collider>().material = Synapsing.Singleton.noFrictionMaterial;



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


    }

    void OnTriggerEnter(Collider collider)
    {
        if (attachedObject == null)
        {
            if (collider.gameObject.GetComponent<CubeControl>() != null)
            {

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
        }
    }

    void OnCollisionStay(Collision col)
    {
        if (attachedObject != null)
        {
            if (col.gameObject.tag == "Blop1")
            {
                StopMergin();
            }

            if (col.gameObject.tag == "Blop1_Attachment")
            {
                StopMergin();
                GameObject go = GameObject.FindGameObjectWithTag("Blop1");
                go.GetComponent<Blop1Control>().StopMergin();
            }
        }
        else
        {
            if (col.gameObject.tag == "Blop1_Attachment")
            {
                GameObject goBlop1 = GameObject.FindGameObjectWithTag("Blop1");
                goBlop1.GetComponent<Blop1Control>().StopMergin();
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


            if (Grapple.Si_Grappple.IsGrappling() == true)
            {
                Grapple.Si_Grappple.StopGrapple();
            }
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

    public GameObject GetAttachedObject()
    {
        return attachedObject.gameObject;
    }

}

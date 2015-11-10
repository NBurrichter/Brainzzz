using UnityEngine;
using System.Collections;

public class CubeControl : MonoBehaviour
{


    private bool bIsMergin;
    public enum BlockType { Cube, Ramp, NPC, NPCAStar };
    public BlockType blocktype;

    private Blop1Control Blop1Script;
    private Blop2Control Blop2Script;

    //Only needed for NPC
    private NavmeshTestNavigation navigation;

    // Use this for initialization
    void Start()
    {
        bIsMergin = false;

        if (blocktype == BlockType.NPC)
        {
            navigation = GetComponent<NavmeshTestNavigation>();
        }


    }

    // Update is called once per frame
    void Update()
    {
        // Reset if its not an attachment
        if (this.gameObject.tag == "Untagged")
        {
            ResetObject();
        }

        // Get access to the scripts of the two Blops
        if (GameObject.FindGameObjectWithTag("Blop1") != null)
            Blop1Script = GameObject.FindGameObjectWithTag("Blop1").GetComponent<Blop1Control>();
        if (GameObject.FindGameObjectWithTag("Blop2") != null)
            Blop2Script = GameObject.FindGameObjectWithTag("Blop2").GetComponent<Blop2Control>();

    }

    /// <summary>
    /// Resets the object
    /// </summary> 
	void ResetObject()
    {
        if (this.gameObject.GetComponent<Rigidbody>() && blocktype == BlockType.Cube)
        {
            this.gameObject.GetComponent<BoxCollider>().material = null; // remove the no-friction material
            Rigidbody rb = this.gameObject.GetComponent<Rigidbody>();
            rb.mass = 1000;
            rb.useGravity = true;
        }
    }


    /// <summary>
    /// handles what should be done when the mergin is stopped
    /// </summary>
    public void StopMergin()
    {
        if (blocktype == BlockType.Ramp)
        {
            Debug.Log("Remove Joint");
            this.gameObject.GetComponent<Rigidbody>().freezeRotation = true;

        }

        if (blocktype == BlockType.NPC)
        {
            navigation.SetActivationMode(true);
        }
        if (blocktype == BlockType.NPCAStar)
        {
            GetComponent<FindTestPath>().enabled = true;
            GetComponent<FindTestPath>().Start();
            GetComponent<Seeker>().enabled = true;
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<CharacterController>().enabled = true;
        }

        ResetObject();

    }


    /// <summary>
    /// returns if the objects is mergin or not
    /// </summary>
    public bool GetMerginStatus()
    {
        return bIsMergin;
    }


    /// <summary>
    /// set if the objects is mergin or not
    /// </summary>
    /// <param name="b"></param>
    public void SetMergin(bool b)
    {
        bIsMergin = b;
    }

    void OnCollisionEnter(Collision c)
    {

        // collision with other attachment
        if (this.gameObject.tag == "Blop1_Attachment" && c.gameObject.tag == "Blop2_Attachment")
        {
            Debug.Log("Stop Mergin");
            bIsMergin = false;
            Blop1Script.StopMergin();
            Blop2Script.StopMergin();

        }

        if (this.gameObject.tag == "Blop2_Attachment" && c.gameObject.tag == "Blop1_Attachment")
        {
            Debug.Log("Stop Mergin");
            bIsMergin = false;
            Blop1Script.StopMergin();
            Blop2Script.StopMergin();
        }
    }
}

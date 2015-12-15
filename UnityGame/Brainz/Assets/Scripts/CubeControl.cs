using UnityEngine;
using System.Collections;

public class CubeControl : MonoBehaviour
{


    private bool bIsMergin;
    public enum BlockType { Cube, Ramp, NPC, NPCAStar };
    public BlockType blocktype;

    private Blop1Control Blop1Script;
    private Blop2Control Blop2Script;

    private Rigidbody rbody;

    //Only needed for NPC
    private NavmeshTestNavigation navigation;

    //Needed to update the grid graph
    private bool saveSleeping;

    public bool showSleeping;

    //needed for pathfinnding
    private bool finished;

    // Use this for initialization
    void Start()
    {
        bIsMergin = false;

        saveSleeping = false;
        rbody = GetComponent<Rigidbody>();

        if (blocktype == BlockType.NPC)
        {
            navigation = GetComponent<NavmeshTestNavigation>();
        }

        finished = false;
    }

    // Update is called once per frame
    void Update()
    {
        showSleeping = rbody.IsSleeping();

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

        if (!rbody.IsSleeping())
        {
            saveSleeping = false;
        }
        if (!saveSleeping && rbody.IsSleeping() && blocktype != BlockType.NPCAStar)
        {
            //Debug.Log("Update GridGraph from Object " + name);
            UpdateGraph.S.UpdateGridGraph();
            saveSleeping = true;
        }

        if(blocktype == BlockType.NPCAStar && finished)
        {

            if (Physics.Raycast(transform.position, Vector3.down, 1) && !bIsMergin)
            {
                StartCoroutine(ResetNPC());
                GetComponent<FindTestPath>().Landed();
                finished = false;
            }
        }
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
        }
    }


    /// <summary>
    /// handles what should be done when the mergin is stopped
    /// </summary>
    public void StopMergin()
    {
        //UpdateGraph.S.UpdateGridGraph();

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
            finished = true;

            /*
            //UpdateGraph.S.UpdateGridGraph();
            FindTestPath myTestPath = GetComponent<FindTestPath>();
            myTestPath.enabled = true;
            myTestPath.Start();
            //myTestPath.StartCouroutineFindPath();
            GetComponent<Seeker>().enabled = true;
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<CharacterController>().enabled = true;
            rbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            */
        }

        ResetObject();
        this.gameObject.GetComponent<Rigidbody>().useGravity = true;
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
            bIsMergin = false;
            Blop1Script.StopMergin();
            Blop2Script.StopMergin();

        }

        if (this.gameObject.tag == "Blop2_Attachment" && c.gameObject.tag == "Blop1_Attachment")
        {
            bIsMergin = false;
            Blop1Script.StopMergin();
            Blop2Script.StopMergin();
        }
    }

    void OnCollisionStay(Collision col)
    {
        // collision with other attachment
        if (this.gameObject.tag == "Blop1_Attachment" && col.gameObject.tag == "Blop2_Attachment")
        {
            bIsMergin = false;
            Blop1Script.StopMergin();
            Blop2Script.StopMergin();
            //Show particles to indicate that they are combined

        }

        if (this.gameObject.tag == "Blop2_Attachment" && col.gameObject.tag == "Blop1_Attachment")
        {
            bIsMergin = false;
            Blop1Script.StopMergin();
            Blop2Script.StopMergin();
            // Show particles to indicate that they are combined
        }

    }

    IEnumerator ResetNPC()
    {
        yield return new WaitForSeconds(1);

        //UpdateGraph.S.UpdateGridGraph();
        FindTestPath myTestPath = GetComponent<FindTestPath>();
        myTestPath.enabled = true;
        myTestPath.Start();
        //myTestPath.StartCouroutineFindPath();
        GetComponent<Seeker>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<CharacterController>().enabled = true;
        GetComponent<FindTestPath>().Landed();
        GetComponent<FindTestPath>().ResetFallCicle();
        rbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }


}

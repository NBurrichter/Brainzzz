using UnityEngine;
using System.Collections;

public class CubeControl : MonoBehaviour {


    private bool bIsMergin;
    public enum  BlockType {Cube,Ramp,NPC };
    public BlockType blocktype;

    private Blop1Control Blop1Script;
    private Blop2Control Blop2Script;

    //Only needed for NPC
    private NavmeshTestNavigation navigation;

	// Use this for initialization
	void Start () {
        bIsMergin = false;

        if (blocktype == BlockType.NPC)
        {
            navigation = GetComponent<NavmeshTestNavigation>();
        }

       
    }
	
	// Update is called once per frame
	void Update () {
		if(this.gameObject.tag=="Untagged")
		{
			ResetMass();
		}

        if(GameObject.FindGameObjectWithTag("Blop1")!=null)
            Blop1Script = GameObject.FindGameObjectWithTag("Blop1").GetComponent<Blop1Control>();
        if(GameObject.FindGameObjectWithTag("Blop2")!=null)
            Blop2Script = GameObject.FindGameObjectWithTag("Blop2").GetComponent<Blop2Control>();

    }

	void ResetMass()
	{
		if(this.gameObject.GetComponent<Rigidbody>())
		{
			Rigidbody rb = this.gameObject.GetComponent<Rigidbody>();
			rb.mass=1000;
		}
	}

    public void StopMergin()
    {

            Debug.Log("Stop Mergin in cube control");
            if (blocktype == BlockType.Ramp)
            {
                Debug.Log("Remove Joint");
                this.gameObject.GetComponent<Rigidbody>().freezeRotation = true;
                
            }

            if (blocktype == BlockType.NPC)
            {
                navigation.SetActivationMode(true);
            }

            this.gameObject.GetComponent<Rigidbody>().useGravity = true;
             
    }

    public bool GetMerginStatus()
    {
        return bIsMergin;
    }


    public void SetMergin(bool b)
    {
        bIsMergin = b;
    }

    void OnCollisionEnter(Collision c)
    {
        

        if(this.gameObject.tag=="Blop1_Attachment" && c.gameObject.tag=="Blop2_Attachment")
        {
            Debug.Log("Stop Mergin");
            bIsMergin = false;
            Blop1Script.StopMergin();
            Blop2Script.StopMergin();

        }

        if (this.gameObject.tag == "Blop2_Attachment" && c.gameObject.tag=="Blop1_Attachment")
        {
            Debug.Log("Stop Mergin");
            bIsMergin = false;
            Blop1Script.StopMergin();
            Blop2Script.StopMergin();
        }
    }
}

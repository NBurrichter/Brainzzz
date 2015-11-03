using UnityEngine;
using System.Collections;

public class CubeControl : MonoBehaviour {


    private bool bIsMergin;
    public enum BlockType {Cube,Ramp };
    public BlockType blocktype;

	// Use this for initialization
	void Start () {
        bIsMergin = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(this.gameObject.tag=="Untagged")
		{
			ResetMass();
		}

	
	}

	void ResetMass()
	{
		if(this.gameObject.GetComponent<Rigidbody>())
		{
			Rigidbody rb = this.gameObject.GetComponent<Rigidbody>();
			rb.mass=1000;
		}
	}

    public bool StopMergin()
    {
        if (bIsMergin == false)
        {
            Debug.Log("mergin is false");
            if (blocktype == BlockType.Ramp)
            {
                Debug.Log("Remove Joint");
                this.gameObject.GetComponent<Rigidbody>().freezeRotation = true;
                
            }
            this.gameObject.GetComponent<Rigidbody>().useGravity = true;
            return true;
        }

        return false;
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
        }

        if (this.gameObject.tag == "Blop2_Attachment" && c.gameObject.tag=="Blop1_Attachment")
        {
            Debug.Log("Stop Mergin");
            bIsMergin = false;
        }
    }
}

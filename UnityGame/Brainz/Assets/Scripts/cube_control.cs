using UnityEngine;
using System.Collections;

public class cube_control : MonoBehaviour {


    private bool bIsMergin;

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
			rb.mass=10;
		}
	}

    public bool StopMergin()
    {
        if (bIsMergin == false)
            return true;

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

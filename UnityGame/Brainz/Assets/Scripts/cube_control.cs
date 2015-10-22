using UnityEngine;
using System.Collections;

public class cube_control : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
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
}

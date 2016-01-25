using UnityEngine;
using System.Collections;

public class StopElevator : MonoBehaviour {

    public float stopOnY;

	void Update ()
    {
	    if (transform.position.y >=stopOnY)
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            transform.position = new Vector3(transform.position.x, stopOnY, transform.position.z);
        }
	}
}

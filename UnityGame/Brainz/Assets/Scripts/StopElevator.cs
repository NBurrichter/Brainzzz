using UnityEngine;
using System.Collections;

public class StopElevator : MonoBehaviour {

    public float stopOnY;

    private bool bPostionFixed = false;

	void LateUpdate ()
    {
        if (transform.position.y >= stopOnY && bPostionFixed == false)
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            transform.position = new Vector3(transform.position.x, stopOnY, transform.position.z);

            // Reset the Synapsing-state
            GameObject.FindGameObjectWithTag("Blop1").GetComponent<Blop1Control>().StopMergin();
            GameObject.FindGameObjectWithTag("Blop2").GetComponent<Blop2Control>().StopMergin();

            bPostionFixed = true;
        }
	}
}

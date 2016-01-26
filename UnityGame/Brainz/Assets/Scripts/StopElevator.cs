using UnityEngine;
using System.Collections;

public class StopElevator : MonoBehaviour {

    public float stopOnY;

    private bool bPostionFixed = false;

	void Update ()
    {
        if (transform.position.y >= stopOnY && bPostionFixed == false)
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            transform.position = new Vector3(transform.position.x, stopOnY, transform.position.z);

            Synapsing.Singleton.StopMergin(); // nur eine Notlösung, versuche bessere zu finden
            Debug.LogWarning("Stop synapsing");
            GameObject.FindGameObjectWithTag("Blop1").GetComponent<Blop1Control>().StopMergin();
            Debug.LogWarning("Stop Blop1");
            GameObject.FindGameObjectWithTag("Blop2").GetComponent<Blop2Control>().StopMergin();
            Debug.LogWarning("Stop Blop2");
            bPostionFixed = true;
        }
	}
}

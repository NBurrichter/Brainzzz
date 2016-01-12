using UnityEngine;
using System.Collections;

public class LerpOnlyZ : MonoBehaviour {

    public GameObject lerpToPoint;
    public float easing;

    void Start()
    {
        transform.localPosition = lerpToPoint.transform.localPosition;
    }

	void FixedUpdate ()
    {
        transform.localPosition = new Vector3(0, 0, transform.localPosition.z);
        transform.position = Vector3.Lerp(transform.position, lerpToPoint.transform.position, easing);
	}
}

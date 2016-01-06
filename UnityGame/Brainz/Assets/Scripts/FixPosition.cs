using UnityEngine;
using System.Collections;

public class FixPosition : MonoBehaviour {

    private Vector3 startPosition;

	void Start () {
        startPosition = transform.localPosition;
	}
	
	void Update () {
        transform.localPosition = startPosition;
        transform.localRotation = Quaternion.Euler(0, 0, 0);
	}
}

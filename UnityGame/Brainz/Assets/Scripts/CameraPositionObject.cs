using UnityEngine;
using System.Collections;

public class CameraPositionObject : MonoBehaviour {

    public GameObject focusPoint;
    public Vector3 offset;

	void Start ()
    {
	
	}

    void Update ()
    {
        transform.localPosition = focusPoint.transform.position + offset;
	}
}

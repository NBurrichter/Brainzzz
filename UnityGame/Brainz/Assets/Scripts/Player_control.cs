using UnityEngine;
using System.Collections;

public class Player_control : MonoBehaviour {

	private float fMovementSpeed = 4.0f;
	public float fRotationSpeed = 40.0f;
	private float fXRotation;
	private float fYRotation;
	private Vector3 vPosition;
	private Quaternion qRotation;
	private GameObject player;

	private bool isGrappling;

	// Use this for initialization
	void Start () {
		player = this.gameObject;
		vPosition = player.transform.position;
		qRotation = player.transform.rotation;
		isGrappling=false;
		print (""+Screen.height + "  " + Screen.width);
	}
	
	// Update is called once per frame
	void Update () {

		// check if mouse is out of screen

			fXRotation += Input.GetAxis("Mouse X") * fRotationSpeed * Time.deltaTime;

			fYRotation += Input.GetAxis("Mouse Y") * fRotationSpeed * Time.deltaTime *-1.0f;



		/*
		qRotation[1] += fXRotation;
		fXRotation = 0;
		qRotation[0] += fYRotation;
		fYRotation = 0;
		*/

		qRotation = Quaternion.Euler(fYRotation,fXRotation,0);

		vPosition.x += Input.GetAxis("Horizontal") * fMovementSpeed * Time.deltaTime;
		vPosition.z += Input.GetAxis("Vertical") * fMovementSpeed * Time.deltaTime;

		if(isGrappling==false)
		{
		player.transform.rotation = qRotation;
		player.transform.position = vPosition;
		}


	}

	public void SetGrapple(bool b)
	{
		isGrappling = b;
	}


}

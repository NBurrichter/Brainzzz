using UnityEngine;
using System.Collections;

public class Aiming_control : MonoBehaviour {


	RaycastHit hit;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


		if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
		{

		}
		Debug.DrawLine(Camera.main.transform.position,hit.point,Color.green);
	}
}

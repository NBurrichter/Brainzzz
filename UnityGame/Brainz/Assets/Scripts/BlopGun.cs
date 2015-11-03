using UnityEngine;
using System.Collections;

public class BlopGun : MonoBehaviour {

	public GameObject Blop1Prefab;
	public GameObject Blop2Prefab;

	private Vector3 vPlayerPos;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		vPlayerPos = this.gameObject.transform.position;
        vPlayerPos += this.gameObject.transform.forward;

		if(Input.GetButtonDown("Fire1"))
		{

            Instantiate(Blop1Prefab, vPlayerPos + AimingControl.aimingControlSingleton.GetSpawnPosition(),Quaternion.identity);

		}

		if(Input.GetButtonDown("Fire2"))
		   {

			Instantiate(Blop2Prefab, vPlayerPos + AimingControl.aimingControlSingleton.GetSpawnPosition(), Quaternion.identity);
		}
	}
}

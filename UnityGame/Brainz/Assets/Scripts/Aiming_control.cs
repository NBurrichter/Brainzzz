using UnityEngine;
using System.Collections;

public class Aiming_control : MonoBehaviour {


	RaycastHit hit;
    private GameObject goPlayer;
    public static Aiming_control aimingControlSingleton;

	// Use this for initialization
	void Start () {
        goPlayer = GameObject.FindGameObjectWithTag("Player");
        aimingControlSingleton = this;
	}
	
	// Update is called once per frame
	void Update () {


		if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
		{

		}
		Debug.DrawLine(Camera.main.transform.position,hit.point,Color.green);
        
	}

    public Vector3 GetHitDirection()
    {
        Vector3 vHitDirection = hit.point - goPlayer.transform.position;
        vHitDirection = vHitDirection * 2;

        return vHitDirection;
    }
}

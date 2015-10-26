using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public GameObject player;
	private Vector3 offset;
	private Camera[] cameraArray;
	
	// Use this for initialization
	void Start ()
	{
		offset = new Vector3(0,1,0);
		/*cameraArray = Camera.allCameras;
		Camera.main.enabled=false;
		for(int i=0;i<cameraArray.Length;i++)
		{
			if(cameraArray[i].tag=="PlayerCamera")
			{
				cameraArray[i].enabled=true;
			}
		}
		*/
		transform.position = player.transform.position + offset;
	}


}

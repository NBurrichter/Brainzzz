using UnityEngine;
using System.Collections;

public class UpdateGraph : MonoBehaviour {

	void Start ()
    {
	
	}
	
	void Update ()
    {
        AstarPath.active.Scan();
	}
}

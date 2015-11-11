using UnityEngine;
using System.Collections;

public class UpdateGraph : MonoBehaviour {

	void Start ()
    {
	
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            AstarPath.active.Scan();
        }
    }
}

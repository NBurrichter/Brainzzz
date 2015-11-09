using UnityEngine;
using System.Collections;

public class TestAStarMovement : Pathfinding {

    public Vector3 endPos;

	void Start ()
    {
	
	}
	
	void Update ()
    {
	    if(Input.GetKeyDown(KeyCode.P))
        {
            FindPath(transform.position, endPos);
        }
        if (Path.Count > 0)
        {
            Move();
        }
	}
}

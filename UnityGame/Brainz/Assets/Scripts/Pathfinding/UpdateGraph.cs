using UnityEngine;
using System.Collections;

public class UpdateGraph : MonoBehaviour {

    public static UpdateGraph S;

	void Start ()
    {
        S = this;
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            AstarPath.active.Scan();
        }
    }

    public void UpdateGridGraph()
    {
        //AstarPath.active.Scan();
    }
}

using UnityEngine;
using System.Collections;

public class UpdateGraph : MonoBehaviour {

    public static UpdateGraph S;

    private FindTestPath sonPath;

	void Start ()
    {
        S = this;
        sonPath = GameObject.Find("Son").GetComponent<FindTestPath>();
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
        AstarPath.active.Scan();
        //Send a request tto son to update path
        sonPath.Start();
    }
}

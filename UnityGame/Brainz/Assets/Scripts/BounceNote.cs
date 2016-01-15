using UnityEngine;
using System.Collections;

public class BounceNote : MonoBehaviour {

    public float delay;
    public float speed;
    public float amplification;

    private Vector3 startPos;
    private float time;

    void Start()
    {
        startPos = transform.localPosition;

    }

	void Update ()
    {
        time += Time.deltaTime * speed;
        transform.localPosition = new Vector3(startPos.x, startPos.y - Mathf.Sin(time-delay) * amplification, startPos.z);
            
	}


}

using UnityEngine;
using System.Collections;

public class WipObject : MonoBehaviour {

    private Quaternion startRotaion;
    private float time;

    public float amplification;
    public float speed;

	void Start ()
    {
        startRotaion = transform.localRotation;
        time = 0;
    }
	
	void Update ()
    {
        time += Time.deltaTime *speed;
        transform.localRotation = Quaternion.Euler(startRotaion.eulerAngles.x, startRotaion.eulerAngles.y, startRotaion.eulerAngles.z + Mathf.Sin(time)*amplification);
	}
}

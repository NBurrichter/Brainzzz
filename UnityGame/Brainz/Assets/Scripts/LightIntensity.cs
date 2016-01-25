using UnityEngine;
using System.Collections;

public class LightIntensity : MonoBehaviour {

    public float speed;
    public float amplification;

    private Light light;

	// Use this for initialization
	void Start () {
        light = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
        light.intensity = 1 + Mathf.Sin(Time.time * speed) * amplification;
	}
}

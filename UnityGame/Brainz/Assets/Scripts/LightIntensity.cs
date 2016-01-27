using UnityEngine;
using System.Collections;

public class LightIntensity : MonoBehaviour {

    public float speed;
    public float amplification;
    public float offset;

    private Light changingLight;

    private Renderer rend;

	// Use this for initialization
	void Start () {
        changingLight = GetComponent<Light>();
        rend = transform.parent.GetComponentInChildren<Renderer>();
    }
	
	// Update is called once per frame
	void Update () {
        rend.material.SetColor("_EmissionColor", new Color(Mathf.Sin(Time.time * speed)*0.14f + 0.14f, 0.297f, 0.297f));

        changingLight.intensity = offset + Mathf.Sin(Time.time * speed) * amplification;
	}
}

using UnityEngine;
using System.Collections;

public class PlaySoundlines : MonoBehaviour {

    private DamageIndicator sonDamageScript;

    private AudioSource source;

    public AudioClip[] talkClips;

	void Start ()
    {
        sonDamageScript = GameObject.Find("Son").GetComponent<DamageIndicator>();
	}
	
	void Update ()
    {
	
	}

    public void 
}

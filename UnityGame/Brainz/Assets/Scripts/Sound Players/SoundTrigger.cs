using UnityEngine;
using System.Collections;

public class SoundTrigger : MonoBehaviour {

    private bool played;

    private AudioSource source;

    public AudioClip soundClips;


    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    void Start ()
    {
        played = false;
	}

    void OnTriggerEnter(Collider col)
    {
        Debug.Log(col.name);
        if (col.name == "Son" && !played)
        {
            Debug.Log("PlayFrom : " + name);
            played = true;
            source.PlayOneShot(soundClips);

        }
    }

}

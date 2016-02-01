using UnityEngine;
using System.Collections;

public class StartScreenBackgroundVideo : MonoBehaviour {

    public MovieTexture movie;
    private AudioSource audsource;

    // Use this for initialization
    void Start()
    {
        audsource = this.gameObject.AddComponent<AudioSource>();
        audsource.clip = movie.audioClip;
        movie.Play();
        movie.loop = true;
        audsource.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

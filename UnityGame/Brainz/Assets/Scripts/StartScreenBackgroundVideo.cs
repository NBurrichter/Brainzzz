using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartScreenBackgroundVideo : MonoBehaviour {

    public MovieTexture movieBackground;
    public MovieTexture movieIntro;
    public RawImage Image;
    public GameObject buttonExit;
    private AudioSource audsource;


    public static StartScreenBackgroundVideo Singleton;


    // Use this for initialization
    void Start()
    {
        audsource = this.gameObject.AddComponent<AudioSource>();
        audsource.clip = movieBackground.audioClip;
        movieBackground.Play();
        movieBackground.loop = true;
        audsource.Play();

        Singleton = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayIntroVideo()
    {
        Image.texture = movieIntro;
        audsource.clip = movieIntro.audioClip;
        movieIntro.Play();
        movieIntro.loop = false;
        audsource.Play();
        Destroy(buttonExit);
    }

    public MovieTexture GetVideoPlayed()
    {
        if (Image.texture == movieIntro)
            return movieIntro;

        return movieBackground;
    }
}

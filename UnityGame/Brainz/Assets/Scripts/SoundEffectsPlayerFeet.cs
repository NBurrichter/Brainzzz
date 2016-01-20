using UnityEngine;
using System.Collections;

public class SoundEffectsPlayerFeet : MonoBehaviour {

    private AudioSource audioPlayer;

    public AudioClip clipFootstepsForward;
    public float fClipPitchForward;

    public AudioClip clipFootstepsBackward;
    public float fClipPitchBackward;

    public AudioClip clipFootstepsSidewards;

	// Use this for initialization
	void Start () {
        audioPlayer = this.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetAxis("Vertical") > 0 && audioPlayer.isPlaying == false)
        {
            //Stop if other sound was played
            if (audioPlayer.clip != clipFootstepsForward)
                audioPlayer.Stop();

            // switch to correct sound and play it
            audioPlayer.clip = clipFootstepsForward;
            audioPlayer.pitch = fClipPitchForward;
            audioPlayer.Play();

        }
        if (Input.GetAxis("Vertical") < 0 && audioPlayer.isPlaying == false)
        {
            //Stop if other sound was played
            if (audioPlayer.clip != clipFootstepsBackward)
                audioPlayer.Stop();

            // switch to correct sound and play it
            audioPlayer.clip = clipFootstepsBackward;
            audioPlayer.pitch = fClipPitchBackward;
            audioPlayer.Play();
        }


        if (Input.GetAxis("Horizontal") != 0 && audioPlayer.isPlaying == false)
        {
            Debug.LogWarning("play back sound");
            //Stop if other sound was played
            if (audioPlayer.clip != clipFootstepsSidewards)
                audioPlayer.Stop();

            // switch to correct sound and play it
            audioPlayer.clip = clipFootstepsSidewards;
            audioPlayer.pitch = 1;
            audioPlayer.Play();
        }
        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
        {
            audioPlayer.Stop();
        }


    }
}

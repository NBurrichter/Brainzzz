using UnityEngine;
using System.Collections;

public class SoundEffectsGun : MonoBehaviour {

    private AudioSource audioPlayer;

    public AudioClip clipGunShot;

	// Use this for initialization
	void Start () {
        audioPlayer = this.gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
        if((Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2")) && audioPlayer.isPlaying == false )
        {
            //Stop if other sound was played
            if (audioPlayer.clip != clipGunShot)
                audioPlayer.Stop();

            // switch to correct sound and play it
            audioPlayer.clip = clipGunShot;
            audioPlayer.Play();
        }
	}
}

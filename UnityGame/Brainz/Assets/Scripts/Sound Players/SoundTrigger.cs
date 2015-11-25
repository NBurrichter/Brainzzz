using UnityEngine;
using System.Collections;

public class SoundTrigger : MonoBehaviour {

    public int soundClipNumber;

    private bool played;

	void Start ()
    {
        played = false;
	}

    void OnTriggerEnter(Collider col)
    {
        Debug.Log(col.name);
        if (col.name == "Son" && !played)
        {
            played = true;

            PlaySoundlines.S.PlayWeightedSounds(soundClipNumber);
        }
    }

}

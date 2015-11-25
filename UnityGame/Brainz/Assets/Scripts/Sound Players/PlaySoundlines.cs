using UnityEngine;
using System.Collections;

public class PlaySoundlines : MonoBehaviour {

    public static PlaySoundlines S;

    private DamageIndicator sonDamageScript;
    private AudioSource source;

    private int speachCounter;

    //Needs to be 0.f numbers
    public float damagedBorder;
    public float retardedBorder;

    public AudioClip[] normalClips;
    public AudioClip[] damagedClips;
    public AudioClip[] retardedClips;

	void Start ()
    {
        sonDamageScript = GameObject.Find("Son").GetComponent<DamageIndicator>();
	}
	
	void Update ()
    {
        /*
        int i = Random.Range(0, 10);
        if (i == 1)
        {
            PlayWeightedSounds(1);
        }
        */
    }

    public void PlayWeightedSounds(int clipNumber)
    {
        float damagePercentage = 1- ( sonDamageScript.GetHealth() / sonDamageScript.maxHealth);
        Debug.Log(sonDamageScript.GetHealth().ToString()+ "  " + sonDamageScript.maxHealth.ToString());
        Debug.Log(damagePercentage.ToString());

        if (damagePercentage < damagedBorder)
        {
            //Play normal clip
            Debug.Log("Play Normal clip");
        }
        else
        {
            if (damagePercentage >= damagedBorder && damagePercentage <retardedBorder)
            {
                //Play damaged clip
                Debug.Log("Play damaged clip");
            }
            else
            {
                if (damagePercentage >= retardedBorder)
                {
                    //Play retarded Clip
                    Debug.Log("Play retarded clip");
                }
            }
        }
    }
}

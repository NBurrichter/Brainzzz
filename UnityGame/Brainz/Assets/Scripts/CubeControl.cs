﻿using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class CubeControl : MonoBehaviour
{
    public static AudioClip hitSound;

    public AudioClip hitSoundAssign;

    private bool bIsMergin;
    public enum BlockType { Cube, Ramp, NPC, NPCAStar, Elevator};
    public BlockType blocktype;

    // Needed to acces the other Scripts
    private Blop1Control Blop1Script;
    private Blop2Control Blop2Script;

    private Rigidbody rbody;

    private string stOldTag;

    //Only needed for NPC
    private NavmeshTestNavigation navigation;

    //Needed to update the grid graph
    private bool saveSleeping;

    public bool showSleeping;

    //needed for pathfinnding
    private bool finished;

    //Audio Components
    private AudioSource audioPlayer;
    public AudioClip clipObjectMoving;
    public AudioMixerGroup audioMixerGroup;
    public float fAudioPitch = 0.6f;
    private bool bAudioHasPlayed = false;  // only there cause the sounds are not looping perfectly

    // Use this for initialization
    void Start()
    {
        if(hitSoundAssign != null)
        {
            hitSound = hitSoundAssign;
        }
        bIsMergin = false;

        saveSleeping = false;
        rbody = GetComponent<Rigidbody>();

        if (blocktype == BlockType.NPC)
        {
            navigation = GetComponent<NavmeshTestNavigation>();
        }

        finished = false;

        if (this.gameObject.GetComponent<AudioSource>() == null)
        {
            audioPlayer = this.gameObject.AddComponent<AudioSource>();
            audioPlayer.outputAudioMixerGroup = audioMixerGroup;
            audioPlayer.playOnAwake = false;
            audioPlayer.clip = clipObjectMoving;
            audioPlayer.pitch = fAudioPitch;
            //audioPlayer.spatialBlend = 1;
        }
        else
        {
            audioPlayer = this.GetComponent<AudioSource>();
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(bIsMergin == true && rbody.isKinematic==false && audioPlayer.isPlaying==false && 
           blocktype != BlockType.NPCAStar && bAudioHasPlayed == false )
        {
            PlaySound();
        }

        showSleeping = rbody.IsSleeping();
        
        // Reset if object was an attachment in the previous frame and is no attachment now
        if (this.gameObject.tag == "Untagged" && stOldTag !="Untagged")
        {
            ResetObject();
        }

        // Get access to the scripts of the two Blops
        if (GameObject.FindGameObjectWithTag("Blop1") != null)
            Blop1Script = GameObject.FindGameObjectWithTag("Blop1").GetComponent<Blop1Control>();
        if (GameObject.FindGameObjectWithTag("Blop2") != null)
            Blop2Script = GameObject.FindGameObjectWithTag("Blop2").GetComponent<Blop2Control>();

        if (!rbody.IsSleeping())
        {
            saveSleeping = false;
        }
        if (!saveSleeping && rbody.IsSleeping() && blocktype != BlockType.NPCAStar)
        {
            if (Time.time > 10)
            {
                //Debug.Log("Update GridGraph from Object " + name);
                UpdateGraph.S.UpdateGridGraph();
            }
            saveSleeping = true;
        }

        if(blocktype == BlockType.NPCAStar && finished)
        {

            if (Physics.Raycast(transform.position, Vector3.down, 1) && !bIsMergin)
            {
                StartCoroutine(ResetNPC());
                GetComponent<FindTestPath>().Landed();
                finished = false;
            }
        }

        // Get the Tag of the Object
        stOldTag = this.gameObject.tag;
    }

    /// <summary>
    /// Resets the object
    /// </summary> 
	void ResetObject()
    {
        if (this.gameObject.GetComponent<Rigidbody>() && blocktype == BlockType.Cube)
        {
            this.gameObject.GetComponentInChildren<Collider>().material = null; // remove the no-friction material
            Rigidbody rb = this.gameObject.GetComponent<Rigidbody>();        
        }
    }


    /// <summary>
    /// handles what should be done when the mergin is stopped
    /// </summary>
    public void StopMergin()
    {
        bAudioHasPlayed = false;
        audioPlayer.Stop();

        //UpdateGraph.S.UpdateGridGraph();
        if (blocktype == BlockType.NPCAStar)
        {
            audioPlayer.Stop();
        }
        bIsMergin = false;
        if (blocktype == BlockType.Ramp)
        {
            Debug.Log("Remove Joint");
            this.gameObject.GetComponent<Rigidbody>().freezeRotation = true;

        }

        if (blocktype == BlockType.NPC)
        {
            navigation.SetActivationMode(true);
        }
        if (blocktype == BlockType.NPCAStar)
        {
            finished = true;

            /*
            //UpdateGraph.S.UpdateGridGraph();
            FindTestPath myTestPath = GetComponent<FindTestPath>();
            myTestPath.enabled = true;
            myTestPath.Start();
            //myTestPath.StartCouroutineFindPath();
            GetComponent<Seeker>().enabled = true;
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<CharacterController>().enabled = true;
            rbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            */
        }

        ResetObject();
        this.gameObject.GetComponent<Rigidbody>().useGravity = true;
    }


    /// <summary>
    /// returns if the objects is mergin or not
    /// </summary>
    public bool GetMerginStatus()
    {
        return bIsMergin;
    }


    /// <summary>
    /// set if the objects is mergin or not
    /// </summary>
    /// <param name="b"></param>
    public void SetMergin(bool b)
    {
        bIsMergin = b;
    }

    void OnCollisionEnter(Collision c)
    {

        //audioPlayer.PlayOneShot(hitSound);

        // collision with other attachment
        if (this.gameObject.tag == "Blop1_Attachment" && c.gameObject.tag == "Blop2_Attachment")
        {
            bIsMergin = false;
            Blop1Script.StopMergin();
            Blop2Script.StopMergin();

        }

        if (this.gameObject.tag == "Blop2_Attachment" && c.gameObject.tag == "Blop1_Attachment")
        {
            bIsMergin = false;
            Blop1Script.StopMergin();
            Blop2Script.StopMergin();
        }
    }

    void OnCollisionStay(Collision col)
    {
        // collision with other attachment
        if (this.gameObject.tag == "Blop1_Attachment" && col.gameObject.tag == "Blop2_Attachment")
        {
            bIsMergin = false;
            Blop1Script.StopMergin();
            Blop2Script.StopMergin();
            //Show particles to indicate that they are combined

        }

        if (this.gameObject.tag == "Blop2_Attachment" && col.gameObject.tag == "Blop1_Attachment")
        {
            bIsMergin = false;
            Blop1Script.StopMergin();
            Blop2Script.StopMergin();
            // Show particles to indicate that they are combined
        }

    }

    IEnumerator ResetNPC()
    {
        yield return new WaitForSeconds(1);

        //UpdateGraph.S.UpdateGridGraph();
        FindTestPath myTestPath = GetComponent<FindTestPath>();
        myTestPath.enabled = true;
        myTestPath.Start();
        //myTestPath.StartCouroutineFindPath();
        GetComponent<Seeker>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<CharacterController>().enabled = true;
        GetComponent<FindTestPath>().Landed();
        GetComponent<FindTestPath>().ResetFallCicle();
        rbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void PlaySound()
    {
        audioPlayer.Play();

        bAudioHasPlayed = true;
    }


}

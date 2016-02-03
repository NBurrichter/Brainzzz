using UnityEngine;
using System.Collections;

public class BlopLifetime : MonoBehaviour {


    public float fBlopLifetime; // the Lifetime of the Blop
    public GameObject goBlopDestructionParticlePrefab;

    private float fTimer;

    private Blop1Control ScriptBlop1;
    private Blop2Control ScriptBlop2;
    private GameObject Blop1DestructionParticle;
    private GameObject Blop2DestructionParticle;

    private bool isUsed;

	// Use this for initialization
	void Start () {
        if (this.gameObject.tag == "Blop1")
        {
            ScriptBlop1 = this.gameObject.GetComponent<Blop1Control>();
        }
        else
        {
            ScriptBlop2 = this.gameObject.GetComponent<Blop2Control>();
        }

        fTimer = 0.0f;
        isUsed = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (ScriptBlop1 != null && ScriptBlop1.GetAttachedObject()!=null)
        {
            isUsed = true;
        }
        else if(ScriptBlop2 != null && ScriptBlop2.GetAttachedObject() != null)
        {
            isUsed = true;
        }

        fTimer += Time.deltaTime;
        if(fTimer >= fBlopLifetime && isUsed == false)
        {
            if(ScriptBlop1!=null)
            {
                PlayDestructionParticles1();
                
                

            }
            else
            {
                PlayDestructionParticles2();
            }
        }
	}

    public void PlayDestructionParticles1()
    {
        Blop1DestructionParticle = Instantiate(goBlopDestructionParticlePrefab, this.transform.position, Quaternion.identity) as GameObject;
        Destroy(Blop1DestructionParticle, 1f);
        ScriptBlop1.DestroyThisBlop();
    }

    public void PlayDestructionParticles2()
    {
        Blop2DestructionParticle = Instantiate(goBlopDestructionParticlePrefab, this.transform.position, Quaternion.identity) as GameObject;
        Destroy(Blop2DestructionParticle, 1f);
        ScriptBlop2.DestroyThisBlop();
    }
}

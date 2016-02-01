using UnityEngine;
using System.Collections;

public class BlopLifetime : MonoBehaviour {


    public float fBlopLifetime; // the Lifetime of the Blop
    public GameObject goBlopDestructionParticlePrefab;

    private float fTimer;

    private Blop1Control ScriptBlop1;
    private Blop2Control ScriptBlop2;
    private GameObject Blop1DesructionParticle;
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
                Blop1DesructionParticle = Instantiate(goBlopDestructionParticlePrefab, this.transform.position, Quaternion.identity) as GameObject;
                Debug.LogWarning(Blop1DesructionParticle.name);
                Destroy(Blop1DesructionParticle, 1.5f);
                Debug.LogWarning("Destroy Particle");
                ScriptBlop1.DestroyThisBlop();
                
                

            }
            else
            {
                Blop2DestructionParticle = Instantiate(goBlopDestructionParticlePrefab, this.transform.position, Quaternion.identity) as GameObject;
                ScriptBlop2.DestroyThisBlop();
                Destroy(Blop2DestructionParticle,1.5f);


            }
        }
	}
}

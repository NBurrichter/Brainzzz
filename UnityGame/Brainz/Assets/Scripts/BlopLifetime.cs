using UnityEngine;
using System.Collections;

public class BlopLifetime : MonoBehaviour {


    public float fBlopLifetime; // the Lifetime of the Blop
    private float fTimer;

    private Blop1Control ScriptBlop1;
    private Blop2Control ScriptBlop2;

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
                ScriptBlop1.DestroyThisBlop();
            }
            else
            {
                ScriptBlop2.DestroyThisBlop();
            }
        }
	}
}

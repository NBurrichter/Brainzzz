using UnityEngine;
using System.Collections;

public class Synapsing : MonoBehaviour
{
    [SerializeField]
    private float fMerginForceMultiplier;

    public PhysicMaterial noFrictionMaterial;

    GameObject Blop1;
    GameObject Blop2;

    Blop1Control Blop1Script;
    Blop2Control Blop2Script;

    private bool bMergeEnabled;
    private IEnumerator merginCoroutine;

    private Rigidbody blopOneBody;
    private Rigidbody blopTwoBody;

    public static Synapsing Singleton;

    public LayerMask mask = -1;  // the layer, the walls should be in


    // Use this for initialization
    void Start()
    {
        Singleton = this;
        bMergeEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        SearchForBlops();

        if (Blop1 == null)
            return;

        if (Blop2 == null)
            return;

        if (Blop1.GetComponent<Blop1Control>().HasAttachedObject() == false)
            return;

        if (Blop2.GetComponent<Blop2Control>().HasAttachedObject() == false)
            return;


        LifetimeAdjust particleScript1 = Blop1.GetComponentInChildren<LifetimeAdjust>();
        particleScript1.target = Blop2;
        LifetimeAdjust particleScript2 = Blop2.GetComponentInChildren<LifetimeAdjust>();
        particleScript2.target = Blop1;
        ParticleSystem particleSystem1 = Blop1.GetComponentInChildren<ParticleSystem>();
        particleSystem1.Play();
        ParticleSystem particleSystem2 = Blop2.GetComponentInChildren<ParticleSystem>();
        particleSystem2.Play();


        if (bMergeEnabled == false)
        {
            bMergeEnabled = true;
        }


        if (bMergeEnabled == true && merginCoroutine == null)
        {
            merginCoroutine = Mergin();
            StartCoroutine(merginCoroutine);
            if(Blop1Script.GetAttachedObject().GetComponent<CubeControl>().blocktype == CubeControl.BlockType.NPCAStar)
            {
                Blop1Script.GetAttachedObject().GetComponent<FindTestPath>().StartFlying();
                //Blop1Script.GetAttachedObject().GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }
            if (Blop2Script.GetAttachedObject().GetComponent<CubeControl>().blocktype == CubeControl.BlockType.NPCAStar)
            {
                Blop2Script.GetAttachedObject().GetComponent<FindTestPath>().StartFlying();
                //Blop1Script.GetAttachedObject().GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }
        }

    }


    /// <summary>
    /// searches for the two Blops
    /// </summary>
	private void SearchForBlops()
    {
        if (Blop1 && Blop2)
        {
            return;
        }

        if (GameObject.FindGameObjectWithTag("Blop1") == null)
            return;
        Blop1 = GameObject.FindGameObjectWithTag("Blop1");

        if (GameObject.FindGameObjectWithTag("Blop2") == null)
            return;
        Blop2 = GameObject.FindGameObjectWithTag("Blop2");

        Blop1Script = Blop1.GetComponent<Blop1Control>();
        Blop2Script = Blop2.GetComponent<Blop2Control>();

        blopOneBody = Blop1.GetComponent<Rigidbody>();
        blopTwoBody = Blop2.GetComponent<Rigidbody>();


    }


    IEnumerator Mergin()
    {
        float timeSinceStart = 1f;

        while (true)
        {
            timeSinceStart += Time.deltaTime;

            // check if Blops are existing and they have an attachment
            if (Blop1 != null && Blop2 != null && Blop1Script.HasAttachedObject() == true && Blop2Script.HasAttachedObject() == true)
            {
                
                Vector3 dir = Blop1.transform.position - Blop2.transform.position;

                // Raycast hit may be deletet if no use
                RaycastHit hit;
                // Raycast to catch if there is a wall between the cubes
                if (Physics.Raycast(Blop1.transform.position, -dir.normalized,out hit,dir.magnitude,mask.value))
                {
                    // Work here if there is a wall between
                    Debug.Log("Hit something");

                }

                // normalize the direction-vector to have it the force independent from the distance of the Blops
                dir = dir.normalized;

                // may remove the time since start component to have a more constant force
                //blopOneBody.isKinematic = false;
                //blopTwoBody.isKinematic = false;
                blopOneBody.AddForce(-dir * fMerginForceMultiplier); 
                blopTwoBody.AddForce(dir * fMerginForceMultiplier);
            }

            yield return new WaitForSeconds(0.1f);
        }
    }


    /// <summary>
    /// stop the mergin process
    /// </summary>
    public void StopMergin()
    {
        bMergeEnabled = false;

        if (merginCoroutine != null)
        {
            StopCoroutine(merginCoroutine);
        }

        if (Blop1Script.GetAttachedObject().GetComponent<CubeControl>().blocktype == CubeControl.BlockType.NPCAStar)
        {
            Blop1Script.GetAttachedObject().GetComponent<FindTestPath>().StartFalling();
            //Blop1Script.GetAttachedObject().GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
        if (Blop2Script.GetAttachedObject().GetComponent<CubeControl>().blocktype == CubeControl.BlockType.NPCAStar)
        {
            Blop2Script.GetAttachedObject().GetComponent<FindTestPath>().StartFalling();
            //Blop1Script.GetAttachedObject().GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }

        merginCoroutine = null;
    }
}

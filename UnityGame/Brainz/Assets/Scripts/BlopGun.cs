using UnityEngine;
using System.Collections;

public class BlopGun : MonoBehaviour {

	public GameObject Blop1Prefab;
	public GameObject Blop2Prefab;

    public GameObject BlopGunSymbol;

    private bool bIsCharged;
    /// <summary>
    /// time since last shot
    /// </summary>
    private float fRechargeTimer;
    /// <summary>
    /// the time needed to recharge the weapon
    /// </summary>
    public float fRechargeTime;

    private Renderer renCurrent;
    public Color colorBlopOneShot;
    public Color colorBlopTwoShot;
    public Color colorSynapsing;



    public static BlopGun BlopGunSingelton;





    private Animator playerAnim;

    // Use this for initialization
    void Start () {
        playerAnim = GetComponentInParent<Animator>();

        BlopGunSingelton = this;
        bIsCharged = true;
        fRechargeTimer = 0.0f;
        renCurrent = BlopGunSymbol.GetComponent<Renderer>();
        

	}
	
	// Update is called once per frame
	void Update ()
    {
        


        if (bIsCharged==false)
        {

            if(fRechargeTimer > fRechargeTime)
            {
                fRechargeTimer = 0.0f;
                bIsCharged = true;

                

            }
            else
            {

                fRechargeTimer += Time.deltaTime;
            }


        }

        // Shot the first Blop
		if(Input.GetButtonDown("Fire1"))
		{
            if (bIsCharged == true)
            {
                Instantiate(Blop1Prefab, transform.position + AimingControl.aimingControlSingleton.GetSpawnPosition(), Quaternion.identity);
                bIsCharged = false;
                Debug.DrawLine(Vector3.zero, AimingControl.aimingControlSingleton.gameObject.transform.position,Color.magenta,5);
                Debug.DrawLine(Vector3.zero, transform.position, Color.cyan, 5);
                Debug.DrawLine(Vector3.zero, transform.parent.root.transform.position, Color.red, 5);


                ChangeTexture(colorBlopOneShot);


                playerAnim.SetTrigger("Shoot");
                StartCoroutine(StartIsShooting());
            }


        }


        // Shot the second Blop
		if(Input.GetButtonDown("Fire2"))
		   {
            if (bIsCharged == true)
            {
                Instantiate(Blop2Prefab, transform.position + AimingControl.aimingControlSingleton.GetSpawnPosition(), Quaternion.identity);
                bIsCharged = false;
                ChangeTexture(colorBlopTwoShot);


                playerAnim.SetTrigger("Shoot");
            }
        }
	}

    public void ChangeTexture(Color col)
    {
        renCurrent.material.color = col;
    }

    IEnumerator StartIsShooting()
    {
        yield return new WaitForEndOfFrame();
        playerAnim.SetBool("IsShooting", true); 
    }
}

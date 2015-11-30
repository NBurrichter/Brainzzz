using UnityEngine;
using System.Collections;

public class BlopGun : MonoBehaviour {

	public GameObject Blop1Prefab;
	public GameObject Blop2Prefab;

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
    private Material matCharged;

    public Material matRecharging;
    public Material matBlop1;
    public Material matBlop2;

    private ParticleSystem psGun;
    private float fXRotation = 90f;


    /// <summary>
    /// the factor the weapon should move while reloading
    /// </summary>
    public float fMovingFactor;

    /// <summary>
    /// the factor how far the object should rotate
    /// </summary>
    public float fRotationFactor;

    private Vector3 vOldPosition;




	// Use this for initialization
	void Start () {
        bIsCharged = true;
        fRechargeTimer = 0.0f;
        renCurrent = GetComponent<Renderer>();
        psGun = GetComponentInChildren<ParticleSystem>();
        

	}
	
	// Update is called once per frame
	void Update ()
    {
        
        transform.LookAt(AimingControl.aimingControlSingleton.GetHitPointPosition());
        Quaternion qRotation; 
        transform.Rotate(new Vector3(90, 0, 0));
        fXRotation = transform.localRotation.eulerAngles.x;
        Debug.Log(fXRotation);

        if (bIsCharged==false)
        {


            if(fRechargeTimer >=fRechargeTime)
            {
                fXRotation = transform.rotation.eulerAngles.x;
                fRechargeTimer = 0.0f;
                bIsCharged = true;
                psGun.Stop();
                //renCurrent.material = matCharged;

            }
            else
            {
                //move weapon while recharging
                if (fRechargeTimer < fRechargeTime / 2)
                {
                    transform.position -= (transform.up * Time.deltaTime) * fMovingFactor;
                    fXRotation -= Time.deltaTime * fRotationFactor;
                }
                else
                {
                    transform.position += (transform.up * Time.deltaTime) * fMovingFactor;
                    fXRotation += Time.deltaTime * fRotationFactor;
                }

                fRechargeTimer += Time.deltaTime;
            }

            qRotation = Quaternion.Euler(fXRotation, 0, 0);
            transform.localRotation = qRotation;

        }

        // Shot the first Blop
		if(Input.GetButtonDown("Fire1"))
		{

            Instantiate(Blop1Prefab, AimingControl.aimingControlSingleton.gameObject.transform.position + AimingControl.aimingControlSingleton.GetSpawnPosition(),Quaternion.identity);
            bIsCharged = false;
            //renCurrent.material = matRecharging;
            matCharged = matBlop1;
            renCurrent.material = matCharged;
            psGun.Play();


        }


        // Shot the second Blop
		if(Input.GetButtonDown("Fire2"))
		   {

			Instantiate(Blop2Prefab, AimingControl.aimingControlSingleton.gameObject.transform.position + AimingControl.aimingControlSingleton.GetSpawnPosition(), Quaternion.identity);
            bIsCharged = false;
            //renCurrent.material = matRecharging;
            matCharged = matBlop2;
            renCurrent.material = matCharged;
            psGun.Play();
        }
	}


}

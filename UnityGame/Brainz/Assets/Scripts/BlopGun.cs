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

    private ParticleSystem psGun;
    private float fXRotation = 90f;
    private float fYRotation;
    private float fZRotation;

    public static BlopGun BlopGunSingelton;


    /// <summary>
    /// the factor the weapon should move while reloading
    /// </summary>
    public float fMovingFactor;

    /// <summary>
    /// the factor how far the object should rotate
    /// </summary>
    public float fRotationFactor;

    private Vector3 vOldPosition;


    private Vector3 localPos;

    // Use this for initialization
    void Start () {
        BlopGunSingelton = this;
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
        fYRotation = transform.localRotation.eulerAngles.y;
        fZRotation = transform.localRotation.eulerAngles.z;

        if (bIsCharged==false)
        {

            if(fRechargeTimer > fRechargeTime)
            {
                fRechargeTimer = 0.0f;
                bIsCharged = true;
                psGun.Stop();
                transform.localPosition = localPos;
                //renCurrent.material = matCharged;

            }
            else
            {
                //move weapon while recharging
                if (fRechargeTimer < fRechargeTime / 2)
                {
                    transform.position -= (transform.up * Time.smoothDeltaTime) * fMovingFactor;
                    fXRotation -= Time.deltaTime * fRotationFactor;

                }
                else
                {
                    transform.position += (transform.up * Time.smoothDeltaTime) * fMovingFactor;
                    fXRotation += Time.deltaTime * fRotationFactor;
                }

                fRechargeTimer += Time.deltaTime;
            }

            qRotation = Quaternion.Euler(fXRotation, fYRotation, fZRotation);
            transform.localRotation = qRotation;

        }

        // Shot the first Blop
		if(Input.GetButtonDown("Fire1"))
		{
            if (bIsCharged == true)
            {
                Instantiate(Blop1Prefab, AimingControl.aimingControlSingleton.gameObject.transform.position + AimingControl.aimingControlSingleton.GetSpawnPosition(), Quaternion.identity);
                bIsCharged = false;
                //renCurrent.material = matRecharging;
                psGun.Play();
                localPos = transform.localPosition;
            }


        }


        // Shot the second Blop
		if(Input.GetButtonDown("Fire2"))
		   {
            if (bIsCharged == true)
            {
                Instantiate(Blop2Prefab, AimingControl.aimingControlSingleton.gameObject.transform.position + AimingControl.aimingControlSingleton.GetSpawnPosition(), Quaternion.identity);
                bIsCharged = false;
                //renCurrent.material = matRecharging;
                psGun.Play();
                localPos = transform.localPosition;
            }
        }
	}

    public void ChangeTexture(Material mat)
    {
        renCurrent.material = mat;
    }
}

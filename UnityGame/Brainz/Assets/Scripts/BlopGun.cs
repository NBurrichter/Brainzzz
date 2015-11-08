using UnityEngine;
using System.Collections;

public class BlopGun : MonoBehaviour
{

    public GameObject Blop1Prefab;
    public GameObject Blop2Prefab;

    private int iBloptype = 1;


    // Use this for initialization
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {

        // Shot the first Blop
        if (Input.GetButtonDown("Fire1"))
        {
            if (iBloptype == 1)
            {
                Instantiate(Blop1Prefab, AimingControl.aimingControlSingleton.gameObject.transform.position + AimingControl.aimingControlSingleton.GetSpawnPosition(), Quaternion.identity);
                iBloptype = 2;
            }
            else
            {
                Instantiate(Blop2Prefab, AimingControl.aimingControlSingleton.gameObject.transform.position + AimingControl.aimingControlSingleton.GetSpawnPosition(), Quaternion.identity);
                iBloptype = 1;
            }
        }
    }
}

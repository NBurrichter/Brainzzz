using UnityEngine;
using System.Collections;

public class AimingControl : MonoBehaviour
{


    RaycastHit hit;
    public static AimingControl aimingControlSingleton;
    public float fShotSpeed;

    // Use this for initialization
    void Start()
    {

        aimingControlSingleton = this;
    }


    // Update is called once per frame
    void Update()
    {

        // Get the point the Player is aiming for
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
        {

        }
        Debug.DrawLine(Camera.main.transform.position, hit.point, Color.green);

    }


    /// <summary>
    /// returns the Direction the shot should be flying, multiplied with a speed value
    /// </summary>
    /// <returns> Vector3</returns>
    public Vector3 GetHitDirection()
    {
        Vector3 vHitDirection = hit.point - BlopGun.BlopGunSingelton.transform.position;
        vHitDirection = vHitDirection.normalized;
        vHitDirection = vHitDirection * fShotSpeed;
        
        return vHitDirection;
    }

    /// <summary>
    /// return the point the Blop should spawn
    /// </summary>
    /// <returns> Vector3</returns>

    public Vector3 GetSpawnPosition()
    {
        Vector3 vHitDirection = hit.point - BlopGun.BlopGunSingelton.transform.position;
        vHitDirection = vHitDirection.normalized;
        

        return vHitDirection;
    }

    public Vector3 GetHitPointPosition()
    {
        return hit.point;
    }

    
}

using UnityEngine;
using System.Collections;

public class DestroyObjectOnCollision : MonoBehaviour {

    void OnCollisionEnter(Collision col)
    {
        Destroy(col.gameObject);
    }

}

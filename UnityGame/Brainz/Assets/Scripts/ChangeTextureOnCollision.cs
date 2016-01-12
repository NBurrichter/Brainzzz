using UnityEngine;
using System.Collections;

public class ChangeTextureOnCollision : MonoBehaviour {

    public GameObject colObject;
    public Texture texture;
    public GameObject matObject;

    void OnCollisionStay(Collision col)
    {
        Debug.Log("Paper collision: " + col.gameObject.name);
        if (col.gameObject == colObject)
        {
            matObject.GetComponent<Renderer>().material.SetTexture("_MainTex", texture);

        }
    }

}

using UnityEngine;
using System.Collections;

public class LoadLevelWhenSonEnters : MonoBehaviour {

    public string levelName;

    public void OnTriggerEnter(Collider col)
    {
        Debug.Log(col.gameObject.name);
        if (col.gameObject.name == "Son")
        {
            Application.LoadLevel(levelName);
        }
    }

}

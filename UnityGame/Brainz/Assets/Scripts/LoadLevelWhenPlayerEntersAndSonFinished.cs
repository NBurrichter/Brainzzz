using UnityEngine;
using System.Collections;

public class LoadLevelWhenPlayerEntersAndSonFinished : MonoBehaviour {

    public string levelName;
    public GameObject son;

    public void OnTriggerStay(Collider col)
    {
        Debug.Log(col.gameObject.name);
        if (col.gameObject.name == "Player")
        {
            if (son.GetComponent<FindTestPath>().finished)
            {
                Application.LoadLevel(levelName);
            }
        }
    }
}

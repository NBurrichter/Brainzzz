using UnityEngine;
using System.Collections;

public class SkipMovie : MonoBehaviour {

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F12))
        {
            Application.LoadLevel("IntroLevel");
        }
    }

}

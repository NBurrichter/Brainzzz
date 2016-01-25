using UnityEngine;
using System.Collections;

public class StartScreenButtonControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void StartButtonpressed()
    {
       Application.LoadLevelAsync(2);
    } 

    public void QuitButtonPressed()
    {
        Application.Quit();
    }
}

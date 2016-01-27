using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class StartScreenButtonControl : MonoBehaviour {

    private bool bStartGameEnabled = true;

	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {




        if (Input.anyKeyDown && bStartGameEnabled == true)
            Application.LoadLevel(2);
	}


    public void StartButtonpressed()
    {
       Application.LoadLevel(2);
    } 

    public void QuitButtonPressed()
    {
        Application.Quit();
    }

    public void MouseEnterQuitButton()
    {
        bStartGameEnabled = false;
    }

    public void MouseLeaveQuitButton()
    {
        bStartGameEnabled = true;
    }
}

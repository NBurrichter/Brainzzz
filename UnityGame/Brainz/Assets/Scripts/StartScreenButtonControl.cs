using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class StartScreenButtonControl : MonoBehaviour {

    private bool bStartGameEnabled = true;
    public Texture2D t2DCrosshair;
    private Vector2 vTextureOffset = new Vector2(30, 30);

    // Use this for initialization
    void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {




        if (Input.anyKeyDown && bStartGameEnabled == true)
        {
            Cursor.SetCursor(t2DCrosshair, vTextureOffset, CursorMode.Auto);
            Application.LoadLevel(2);
        }
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

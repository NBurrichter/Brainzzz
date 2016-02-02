using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class StartScreenButtonControl : MonoBehaviour {

    private bool bStartGameEnabled = true;
    public Texture2D t2DCrosshair;
    private Vector2 vTextureOffset = new Vector2(30, 30);
    private MovieTexture moviePlayed;

    // Use this for initialization
    void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {

        moviePlayed = StartScreenBackgroundVideo.Singleton.GetVideoPlayed();
        if (moviePlayed.isPlaying==false)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.SetCursor(t2DCrosshair, vTextureOffset, CursorMode.Auto);
            Application.LoadLevel(1);
        }

        if (Input.anyKeyDown && bStartGameEnabled == true)
        {
            StartScreenBackgroundVideo.Singleton.PlayIntroVideo();
            Camera.main.GetComponent<AudioSource>().mute = true;

        }
	}


    public void StartButtonpressed()
    {
       Application.LoadLevel(1);
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

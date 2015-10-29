using UnityEngine;
using System.Collections;

public class CursorControl : MonoBehaviour {


    private bool bIsCursorLocked=false;
    private CursorLockMode wantedMode;
    public Texture2D t2DCrosshair;
    private Vector2 vTextureOffset = new Vector2(30, 30);
    private bool bIsCrosshair = false;
    // Use this for initialization
    void Start () {
        wantedMode = CursorLockMode.Locked;
        
        
	}

    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("Fire3"))
        {
            Debug.Log("´change Cursor mode");
            if (bIsCursorLocked == false)
            {
                Debug.Log("Cursor now locked");
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Locked;

                bIsCursorLocked = true;
            }
            else
            {
                wantedMode = CursorLockMode.None;
                Debug.Log("Cursor now visible");
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                bIsCursorLocked = false;
            }
        }
             
                if(Input.GetButtonDown("Camera"))
                {
                Debug.Log("Change Cursor Texture");
                if (!bIsCrosshair)
                {
                    Cursor.SetCursor(t2DCrosshair, vTextureOffset, CursorMode.Auto);
                    bIsCrosshair = true;
                }
                else
                {
                    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                    bIsCrosshair = false;
                }
            

        }
	}
}

﻿using UnityEngine;
using System.Collections;

public class CursorControl : MonoBehaviour
{


    private bool bIsCursorLocked = false;
    public Texture2D t2DCrosshair;
    private Vector2 vTextureOffset = new Vector2(30, 30);
    private bool bIsCrosshair = false;
    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            LockCursor();
        }

        // change the texture of the cursor
        if (Input.GetButtonDown("Camera"))
        {
            Debug.Log("Change Cursor Texture");
            ChangeCrosshairTexture();

        }
    }


    /// <summary>
    /// changes the texture of the crosshair
    /// </summary>
    void ChangeCrosshairTexture()
    {
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


    /// <summary>
    /// locks the cursor at the middle of the screen
    /// </summary>
    void LockCursor()
    {
        if (bIsCursorLocked == false)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Locked;

            bIsCursorLocked = true;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            bIsCursorLocked = false;
        }
    }
}

using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    private float fXRotation;
    private float fYRotation;
    public float fRotationSpeed;

    private Quaternion qRotation;
    private Vector3 startPos;

	void Start ()
	{
        startPos = transform.localPosition;
	}

    void Update()
    {
        // check if mouse is out of screen
        // fXRotation += Input.GetAxis("Mouse X") * fRotationSpeed * Time.deltaTime;
        fYRotation += Input.GetAxis("Mouse Y") * fRotationSpeed * Time.deltaTime * -1.0f;
        fYRotation += Input.GetAxis("Joystick Camera Y") * fRotationSpeed * Time.deltaTime * -1.0f;

        //Clamp rotation
        fYRotation = Mathf.Clamp(fYRotation,-90,90);

        //Mouse rotation moveement
        qRotation = Quaternion.Euler(fYRotation, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        
        transform.rotation = qRotation;

        //move Cam up down
        float movedist = fYRotation / 90;
        if (movedist > 0)
        {
            transform.localPosition = Vector3.Lerp(startPos, startPos + Vector3.forward * 3 + Vector3.up, movedist);
        }
        else
        {
            movedist = Mathf.Abs(movedist);
            transform.localPosition = Vector3.Lerp(startPos, startPos + Vector3.forward * 2 + Vector3.down, movedist);
        }
    }


}

using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    // Variables used for Rotation
    private float fXRotation;
    private float fYRotation;
    public float fRotationSpeed;

    public GameObject focusPoint;
    public GameObject cameraPositionPoint;

    public Vector3 cameraOffset;

    private Quaternion qRotation;
    private Vector3 startPos;

    private float maxCameraDistance;

    //Rigidbody of player
    private Rigidbody rb;

	void Start ()
	{
        rb = GetComponentInParent<Rigidbody>();

        startPos = focusPoint.transform.localPosition + cameraOffset;
        transform.localPosition = startPos;
        transform.position = cameraPositionPoint.transform.position;
	}

    void Update()
    {
        //--Position---//
        //Camera moveing back with move speed
        //transform.localPosition = startPos - new Vector3(0,0,Mathf.Clamp(rb.velocity.magnitude,0,2));
        transform.position = cameraPositionPoint.transform.position;

        //---Rotation--//
        
        // check if mouse is out of screen
        // fXRotation += Input.GetAxis("Mouse X") * fRotationSpeed * Time.deltaTime;
        fYRotation += Input.GetAxis("Mouse Y") * fRotationSpeed * Time.deltaTime * -1.0f;
        fYRotation += Input.GetAxis("Joystick Camera Y") * fRotationSpeed * Time.deltaTime * -1.0f;

        transform.LookAt(focusPoint.transform.position);

        /*
        //Clamp rotation
        fYRotation = Mathf.Clamp(fYRotation,-90,90);

        //Mouse rotation moveement
        qRotation = Quaternion.Euler(fYRotation, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        
        transform.rotation = qRotation;
        */


        //move Cam up down
        /*
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
        */

        //--Test if camera is occupied--//
        RaycastHit cameraHit;
        maxCameraDistance = Vector3.Distance(transform.position, focusPoint.transform.position);
        if (Physics.Raycast(focusPoint.transform.position,transform.position - focusPoint.transform.position, out cameraHit, maxCameraDistance))
        {
            Debug.DrawLine(focusPoint.transform.position,cameraHit.point);
            Vector3 dist = (cameraHit.point - focusPoint.transform.position) * 0.9f;
            transform.position = dist + focusPoint.transform.position;
        }

    }   


}

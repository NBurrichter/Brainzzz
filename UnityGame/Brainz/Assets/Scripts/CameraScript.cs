using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    private float fXRotation;
    private float fYRotation;
    public float fRotationSpeed;

    private Quaternion qRotation;

	void Start ()
	{

	}

    void Update()
    {
        // check if mouse is out of screen
        fXRotation += Input.GetAxis("Mouse X") * fRotationSpeed * Time.deltaTime;
        fYRotation += Input.GetAxis("Mouse Y") * fRotationSpeed * Time.deltaTime * -1.0f;

        //Mouse rotation moveement
        qRotation = Quaternion.Euler(fYRotation, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        transform.rotation = qRotation;
    }


}

using UnityEngine;
using System.Collections;

public class CameraPositionObject : MonoBehaviour {

    public GameObject focusPoint;
    public Vector3 offset;
    public float rotationSpeed;
    public float speedOffsetAmplification;

    private float rotation;
    private Rigidbody playerRB;

    void Start ()
    {
        playerRB = GetComponentInParent<Rigidbody>();
        transform.localPosition = offset;
    }

    void Update ()
    {
        //Speed cameraoffset
        float speedOffset = playerRB.velocity.magnitude *speedOffsetAmplification;

        transform.localPosition = offset - new Vector3(0,0,speedOffset);

        // transform.localPosition = focusPoint.transform.position + offset;
        rotation += Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime * -1.0f;
        rotation += Input.GetAxis("Joystick Camera Y") * rotationSpeed * Time.deltaTime * -1.0f;

        transform.LookAt(focusPoint.transform.position);

        //Clamp rotation
        rotation = Mathf.Clamp(rotation, -89, 89);

        //Mouse rotation moveement
        focusPoint.transform.rotation = Quaternion.Euler(rotation, focusPoint.transform.rotation.eulerAngles.y, focusPoint.transform.rotation.eulerAngles.z);

    }
}

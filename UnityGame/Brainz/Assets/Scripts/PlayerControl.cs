using UnityEngine;
using System.Collections;

//Requirements
[RequireComponent(typeof(Rigidbody))]

//----http://wiki.unity3d.com/index.php?title=RigidbodyFPSWalker---
public class PlayerControl : MonoBehaviour {

    //momentarily public, later may be calculated automaticaly
    public float playerHeight;

	public float fRotationSpeed = 40.0f;
	private float fXRotation;
	private float fYRotation;
	private Quaternion qRotation;
	private GameObject player;

    private bool isGrappling;

    //Player Rigidbody
    private Rigidbody rb;

    //Rigidbody movement
    private Vector3 force;

    public float speed = 10.0f;
    public float gravity = 10.0f;
    public float maxVelocityChange = 10.0f;
    public bool canJump = true;
    public float jumpHeight = 2.0f;
    private bool grounded = false;

    //Character Controller vars
    /*
    float speed = 5; // units per second
    float turnSpeed = 90; // degrees per second
    float jumpSpeed = 8;
    float gravity = 9.8f;
    private float vSpeed = 0; // current vertical velocity
    */

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.useGravity = false;
    }

	void Start ()
    {
		player = this.gameObject;
		qRotation = player.transform.rotation;
		isGrappling=false;
	}
	
	void Update ()
    {

		// check if mouse is out of screen
		fXRotation += Input.GetAxis("Mouse X") * fRotationSpeed * Time.deltaTime;
		fYRotation += Input.GetAxis("Mouse Y") * fRotationSpeed * Time.deltaTime *-1.0f;

        fXRotation += Input.GetAxis("Joystick Camera X") * fRotationSpeed * Time.deltaTime;


        //Mouse rotation moveement
        qRotation = Quaternion.Euler(0,fXRotation,0);

        player.transform.rotation = qRotation;

        if(Input.GetButtonDown("Restart"))
        {
            Application.LoadLevel("NiklasScene");
        }

        //Character Controller Script
        /*
        Vector3 vVel = transform.forward * Input.GetAxis("Vertical") * speed;
        Vector3 hVel = transform.right * Input.GetAxis("Horizontal") * speed;
        var controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            vSpeed = 0; // grounded character has vSpeed = 0...
            if (Input.GetKeyDown("space"))
            { // unless it jumps:
                vSpeed = jumpSpeed;
            }
        }
        // apply gravity acceleration to vertical speed:
        vSpeed -= gravity * Time.deltaTime;
        vVel.y = vSpeed; // include vertical speed in vel
                        // convert vel to displacement and Move the character:
        controller.Move(vVel * Time.deltaTime);
        controller.Move(hVel * Time.deltaTime);
        */

    }

    void FixedUpdate()
    {

        if (isGrappling)
        {
            grounded = false;
            return;
        }

       /* if (Physics.Raycast(transform.position, Vector3.down, playerHeight))
        {
            grounded = true;
        }
        */
        if (grounded)
        {
            // Calculate how fast we should be moving
            Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            targetVelocity = transform.TransformDirection(targetVelocity);
            targetVelocity *= speed;

            // Apply a force that attempts to reach our target velocity
            Vector3 velocity = rb.velocity;
            Vector3 velocityChange = (targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;
            rb.AddForce(velocityChange, ForceMode.VelocityChange);

            // Jumping
            if (canJump && Input.GetButton("Jump"))
            {
                rb.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
            }
        }

        // We apply gravity manually for more tuning control
        if(!isGrappling)
        rb.AddForce(new Vector3(0, -gravity * rb.mass, 0));

        grounded = false;
    }

    void OnCollisionStay()
    {
        grounded = true;
    }

    float CalculateJumpVerticalSpeed()
    {
        // From the jump height and gravity we deduce the upwards speed 
        // for the character to reach at the apex.
        return Mathf.Sqrt(2 * jumpHeight * gravity);
    }

    public void SetGrapple(bool b)
	{
		isGrappling = b;
	}

}

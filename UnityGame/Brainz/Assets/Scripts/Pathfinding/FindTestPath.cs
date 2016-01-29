using UnityEngine;
using System.Collections;
using Pathfinding;

public class FindTestPath : MonoBehaviour
{

    //The point to move to
    public Vector3 targetPosition;
    private Seeker seeker;
    private CharacterController controller;
    //The calculated path
    public Path path;
    //The AI's speed per second
    public float speed = 100;
    //The max distance from the AI to a waypoint for it to continue to the next waypoint
    public float nextWaypointDistance = 3;
    //The waypoint we are currently moving towards
    private int currentWaypoint = 0;

    public GameObject[] waypoints;
    private int activeWaypoint;

    //If the end of the path is reached
    private bool endOfPathReached;

    //Apply the current waypoint effect
    public WaypointType.Types walkState;

    //lookat dummy (should be found automaticaly)
    public GameObject lookAtDummy;

    //Sound source
    private AudioSource source;

    public AudioClip clipSonWalking;

    //bool wait and finished
    public bool wait;
    public bool finished;

    //animation controlls
    private Animator anim;

    public void Awake()
    {
        source = GetComponent<AudioSource>();
        anim = GetComponentInChildren<Animator>();

        activeWaypoint = 0;
        seeker = GetComponent<Seeker>();
        controller = GetComponent<CharacterController>();
        endOfPathReached = false;
        finished = false;
    }

    public void Start()
    {
        //wait = false;
        //Start a new path to the targetPosition, return the result to the OnPathComplete function
        seeker.StartPath(transform.position, waypoints[activeWaypoint].transform.position, OnPathComplete);
        endOfPathReached = false;
    }

    public void OnPathComplete(Path p)
    {
        Debug.Log("Yay, we got a path back. Did it have an error? " + p.error);
        if (!p.error)
        {
            path = p;
            //Reset the waypoint counter
            currentWaypoint = 0;
        }
    }
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            speed += 0.1f;
        }

        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            speed -= 0.1f;
        }

        if (finished)
        {
            return;
        }

        if(wait == false && source.isPlaying == false)
        {
            //Stop if other sound was played
            if (source.clip != clipSonWalking)
                source.Stop();

            // switch to correct sound and play it
            source.clip = clipSonWalking;
            source.Play();
        }
        else if(wait == true)
        {
            source.Stop();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Start();
        }

        if (wait == true)
        {
            //Debug.Log("Wait");
            return;
        }

        //rotate towards lookatdummy
        transform.rotation = Quaternion.Lerp(transform.rotation, lookAtDummy.transform.rotation, 0.45f);

        //seeker.GetNewPath(transform.position,targetPosition);

        //Look if near next waypoint
        Debug.DrawLine(transform.position, waypoints[activeWaypoint].transform.position);

        //Old Waypoint reaching. Lookw for the point instead of trigger box
        if (Vector3.Distance(transform.position, waypoints[activeWaypoint].transform.position) < 2 && activeWaypoint < waypoints.Length - 1)
        {
            //Debug.Log("Reached waypoit");
            /*walkState = waypoints[activeWaypoint].GetComponent<WaypointType>().type;
            activeWaypoint++;
            Start();
            endOfPathReached = false;*/
        }

        if (path == null)
        {
            //We have no path to move after yet
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count && !endOfPathReached)
        {
            Debug.Log("End Of Path Reached");
            endOfPathReached = true;
            return;
        }

        //Move around
        if (!endOfPathReached)
        {
            switch (walkState)
            {
                case WaypointType.Types.walkToNextWaypointOnPathEnd:
                case WaypointType.Types.stayOnPathEnd:
                    Debug.DrawLine(transform.position, path.vectorPath[currentWaypoint],Color.red);
                    //Direction to the next waypoint
                    Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
                    dir *= speed;
                    controller.SimpleMove(dir);
                    Vector3 flatDir = new Vector3(dir.x, 0, dir.z);
                    Debug.DrawLine(transform.position, transform.position + flatDir, Color.yellow);
                    lookAtDummy.transform.LookAt(transform.position + flatDir);
                    break;
                case WaypointType.Types.elevatorEntrance:
                    Vector3 directionToWaypoint = (waypoints[activeWaypoint].transform.position - transform.position).normalized;
                    directionToWaypoint *= speed;
                    controller.SimpleMove(directionToWaypoint);
                    Vector3 flatWaypointDir = new Vector3(directionToWaypoint.x, 0, directionToWaypoint.z);
                    Debug.DrawLine(transform.position, transform.position + flatWaypointDir,Color.yellow);
                    lookAtDummy.transform.LookAt(transform.position + flatWaypointDir);
                    break;
                case WaypointType.Types.elevator:
                    GetComponent<Rigidbody>().isKinematic = false;
                    anim.SetBool("IsIdle", true);
                    controller.enabled = false;

                    break;
                case WaypointType.Types.elevatorExit:
                    GetComponent<Rigidbody>().isKinematic = true;
                    controller.enabled = true;
                    Vector3 elevatorDir = (waypoints[activeWaypoint].transform.position - transform.position).normalized;
                    elevatorDir *= speed;
                    controller.SimpleMove(elevatorDir);
                    Vector3 flatElevatorDir = new Vector3(elevatorDir.x, 0, elevatorDir.z);
                    Debug.DrawLine(transform.position, transform.position + flatElevatorDir, Color.yellow);
                    lookAtDummy.transform.LookAt(transform.position + flatElevatorDir);
                    break;
                case WaypointType.Types.walkDirectPath:
                    Vector3 directDir = (waypoints[activeWaypoint].transform.position - transform.position).normalized;
                    directDir *= speed;
                    controller.SimpleMove(directDir);
                    Vector3 flatDirectDir = new Vector3(directDir.x, 0, directDir.z);
                    Debug.DrawLine(transform.position, transform.position + flatDirectDir, Color.yellow);
                    lookAtDummy.transform.LookAt(transform.position + flatDirectDir);
                    break;
                case WaypointType.Types.endOfPath:
                    finished = true;
                    anim.SetBool("IsIdle", true);
                    break;

            }

        }
        else
        {
            switch (walkState)
            {
                case WaypointType.Types.walkToNextWaypointOnPathEnd:
                    Vector3 dir = (waypoints[activeWaypoint].transform.position - transform.position).normalized;
                    dir *= speed;
                    controller.SimpleMove(dir);
                    Vector3 flatDir = new Vector3(dir.x, 0, dir.z);
                    Debug.DrawLine(transform.position, transform.position + flatDir, Color.yellow);
                    lookAtDummy.transform.LookAt(transform.position + flatDir);
                    break;
                case WaypointType.Types.elevatorEntrance:
                    Vector3 elevatorDir = (waypoints[activeWaypoint].transform.position - transform.position).normalized;
                    elevatorDir *= speed;
                    controller.SimpleMove(elevatorDir);
                    Vector3 flatElevatorDir = new Vector3(elevatorDir.x, 0, elevatorDir.z);
                    Debug.DrawLine(transform.position, transform.position + flatElevatorDir, Color.yellow);
                    lookAtDummy.transform.LookAt(transform.position + flatElevatorDir);
                    break;
                case WaypointType.Types.elevator:
                    GetComponent<Rigidbody>().isKinematic = false;
                    controller.enabled = false;
                    anim.SetBool("IsIdle", true);
                    break;
                case WaypointType.Types.elevatorExit:
                    GetComponent<Rigidbody>().isKinematic = true;
                    controller.enabled = true;
                    Vector3 exitDir = (waypoints[activeWaypoint].transform.position - transform.position).normalized;
                    exitDir *= speed;
                    controller.SimpleMove(exitDir);
                    Vector3 flatExitDir = new Vector3(exitDir.x, 0, exitDir.z);
                    Debug.DrawLine(transform.position, transform.position + flatExitDir, Color.yellow);
                    lookAtDummy.transform.LookAt(transform.position + flatExitDir);
                    break;
                case WaypointType.Types.walkDirectPath:
                    Vector3 directDir = (waypoints[activeWaypoint].transform.position - transform.position).normalized;
                    directDir *= speed;
                    controller.SimpleMove(directDir);
                    Vector3 flatDirectDir = new Vector3(directDir.x, 0, directDir.z);
                    Debug.DrawLine(transform.position, transform.position + flatDirectDir, Color.yellow);
                    lookAtDummy.transform.LookAt(transform.position + flatDirectDir);
                    break;
                case WaypointType.Types.endOfPath:
                    finished = true;
                    anim.SetBool("IsIdle", true);
                    break;
            }
            return;
        }

        //Check if we are close enough to the next waypoint
        //If we are, proceed to follow the next waypoint
        if (Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) < nextWaypointDistance)
        {
            currentWaypoint++;
            return;
        }
    }

    public void StartCouroutineFindPath()
    {
        StartCoroutine(FindNewPath());
    }

    IEnumerator FindNewPath()
    {
        yield return new WaitForSeconds(1);
        //Disabled for now
        //Start();
    }

    IEnumerator WaitAtPoint(float seconds)
    {
        anim.SetBool("IsIdle", true);
        yield return new WaitForSeconds(seconds);
        wait = false;
        if (!finished)
        {
            anim.SetBool("IsIdle", false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Waypoint")
        {
            WaypointType pointType = other.transform.parent.GetComponent<WaypointType>();
            //Debug.Log(activeWaypoint.ToString());
            //Debug.Log(pointType.waypointNumber.ToString());

            if (activeWaypoint == pointType.waypointNumber && activeWaypoint < waypoints.Length - 1)
            {
                Debug.Log("Reached waypoit");
                walkState = waypoints[activeWaypoint].GetComponent<WaypointType>().type;
                activeWaypoint++;
                Start();
                endOfPathReached = false;

                wait = true;
                StartCoroutine(WaitAtPoint(pointType.waitTime));

                if (pointType.sType == WaypointType.SoundType.playOneSound)
                {
                    source.PlayOneShot(pointType.soundClip);
                }
            }
        }
    }

    public void GetBlop()
    {
        anim.SetBool("HasBlop", true);
    }

    public void StartFlying()
    {
        anim.SetBool("IsFlying", true);
    }

    public void StartFalling()
    {
        anim.SetBool("IsFalling", true);
    }

    public void Landed()
    {
        anim.SetBool("HitGround", true);
    }

    public void ResetFallCicle()
    {
        anim.SetBool("HasBlop", false);
        anim.SetBool("IsFlying", false);
        anim.SetBool("IsFalling", false);
        anim.SetBool("HitGround", false);
    }
}

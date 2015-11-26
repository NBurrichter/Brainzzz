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

    public void Awake()
    {
        activeWaypoint = 0;
        seeker = GetComponent<Seeker>();
        controller = GetComponent<CharacterController>();
        endOfPathReached = false;
    }

    public void Start()
    {
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
        if (Input.GetKeyDown(KeyCode.P))
        {
            Start();
        }

        //seeker.GetNewPath(transform.position,targetPosition);

        //Look if near next waypoint
        Debug.DrawLine(transform.position, waypoints[activeWaypoint].transform.position);

        //Old Waypoint reaching. Lookw for the point instead of trigger box
        if (Vector3.Distance(transform.position, waypoints[activeWaypoint].transform.position) < 2 && activeWaypoint < waypoints.Length - 1)
        {
            Debug.Log("Reached waypoit");
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
                    transform.LookAt(transform.position + flatDir);
                    break;
                case WaypointType.Types.elevatorEntrance:
                    Vector3 directionToWaypoint = (waypoints[activeWaypoint].transform.position - transform.position).normalized;
                    directionToWaypoint *= speed;
                    controller.SimpleMove(directionToWaypoint);
                    Vector3 flatWaypointDir = new Vector3(directionToWaypoint.x, 0, directionToWaypoint.z);
                    Debug.DrawLine(transform.position, transform.position + flatWaypointDir,Color.yellow);
                    transform.LookAt(transform.position + flatWaypointDir);
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
                    transform.LookAt(transform.position + dir);
                    break;
                case WaypointType.Types.elevatorEntrance:

                    break;
                case WaypointType.Types.elevator:

                    break;
                case WaypointType.Types.elevatorExit:

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

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Waypoint")
        {
            WaypointType pointType = other.transform.parent.GetComponent<WaypointType>();
            Debug.Log(activeWaypoint.ToString());
            Debug.Log(pointType.waypointNumber.ToString());

            if (activeWaypoint == pointType.waypointNumber && activeWaypoint < waypoints.Length - 1)
            {
                Debug.Log("Reached waypoit");
                walkState = waypoints[activeWaypoint].GetComponent<WaypointType>().type;
                activeWaypoint++;
                Start();
                endOfPathReached = false;
            }
        }
    }
}

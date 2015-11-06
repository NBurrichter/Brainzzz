using UnityEngine;
using System.Collections;

public class NavmeshTestNavigation : MonoBehaviour {

    private NavMeshAgent agent;
    private bool active;
    private int nextWaypointNumber;
    private Rigidbody rb;
    public bool grounded;

    public GameObject visualizer;

    public GameObject[] waypoints;

    public float distanceToWaypoint;
    //public GameObject target;

	void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        active = true;
        nextWaypointNumber = 0;
        rb = GetComponent<Rigidbody>();
        grounded = true;
	}

    void Update()
    {
        //Check for grounded
        if (Physics.Raycast(transform.position, Vector3.down,0.55f))
        {
            grounded = true;
            //agent.enabled = true;
        }
        else
        {
            grounded = false;
            //agent.enabled = false;
        }

        //Look if son is near  the next waypoint  toto proceed to the next
        Debug.DrawLine(transform.position, waypoints[nextWaypointNumber].transform.position);
        if (Vector3.Distance(transform.position, waypoints[nextWaypointNumber].transform.position) < distanceToWaypoint)
        {
            if (nextWaypointNumber != waypoints.Length-1)
            {
                nextWaypointNumber++;
            }
        }

        //Look if son is nearer to a later point and then proceed
        float distancePlayerWaypoint = Vector3.Distance(transform.position, waypoints[nextWaypointNumber].transform.position);
        for (int i = nextWaypointNumber; i < waypoints.Length; i++)
        {
            if (distancePlayerWaypoint > Vector3.Distance(transform.position,waypoints[i].transform.position))
            {
                nextWaypointNumber = i;
            }
        }

        //Only move when active
        if (active)
        {
            //agent.destination = target.transform.position;
            //if (grounded)
            //{
                agent.SetDestination(waypoints[nextWaypointNumber].transform.position);
            //}
        }
        visualizer.transform.LookAt(waypoints[nextWaypointNumber].transform.position);
    }

    public void SetActivationMode(bool status)
    {
        if (status)
        {
            agent.enabled = true;
            active = true;
            rb.isKinematic = true;

        }
        else
        {
            agent.enabled = false;
            active = false;
            rb.isKinematic = false;
        }
    }
}

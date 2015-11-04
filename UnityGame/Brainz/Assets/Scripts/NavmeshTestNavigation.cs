using UnityEngine;
using System.Collections;

public class NavmeshTestNavigation : MonoBehaviour {

    private NavMeshAgent agent;
    private bool active;
    private int nextWaypointNumber;
    private Rigidbody rb;

    public GameObject[] waypoints;
    
    //public GameObject target;

	void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        active = true;
        nextWaypointNumber = 0;
        rb = GetComponent<Rigidbody>();
	}

    void Update()
    {
        if (active)
        {
            //agent.destination = target.transform.position;
            agent.SetDestination(waypoints[nextWaypointNumber].transform.position);
        }
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

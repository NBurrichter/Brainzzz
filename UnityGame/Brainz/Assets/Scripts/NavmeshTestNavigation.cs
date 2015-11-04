using UnityEngine;
using System.Collections;

public class NavmeshTestNavigation : MonoBehaviour {

    private NavMeshAgent agent;

    public GameObject target;

	void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
	}
	
	void Update ()
    {
        agent.destination = target.transform.position;
	}
}

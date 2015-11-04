using UnityEngine;
using System.Collections;

public class NavmeshTestNavigation : MonoBehaviour {

    private NavMeshAgent agent;
    private bool active;

    public GameObject target;

	void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        active = true;
	}

    void Update()
    {
        if (active)
        {
            agent.destination = target.transform.position;
        }
    }

    public void SetActivationMode(bool status)
    {
        if (status)
        {
            agent.enabled = true;
            active = true;
        }
        else
        {
            agent.enabled = false;
            active = false;
        }
    }
}

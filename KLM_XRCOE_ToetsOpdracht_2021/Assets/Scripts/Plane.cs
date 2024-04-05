using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Plane : MonoBehaviour
{
    // reference to the planeData class
    public PlaneData planeData;

    // reference to the navMeshAgent that controlls the object this script is attatched to
    public NavMeshAgent navMeshAgent;

    // range the plane is able to make its next move in
    [SerializeField]
    private float range;

    //currently using a bool, if there needs to be more advanced movement, we can use a state machine
    private bool _isPatrolling = true;

    //Created these in case they ever needed to be referenced by other scripts
    public string brand { get; private set; }
    public string type { get; private set; }

    //reference to the light
    public new Light light;

    // Start is called before the first frame update
    void Start()
    {
        //initialize the components from the scriptable object
        brand = planeData.brand;
        type = planeData.type;

        // in this case we don't a foreach, but if we manage to separate all the meshes from
        // the simpleplane object, this could make it easy to colour the entire plane.
        foreach(Transform child in this.transform)
        {
            child.gameObject.GetComponent<Renderer>().material = planeData.material;
        }

        //gameObject.GetComponent<Renderer>().material = planeData.material;
        navMeshAgent.updateUpAxis = false;

        //default lights off
        light.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // check if still patrolling
        if (_isPatrolling)
        {
            Patrol();
        }
    }

    void Patrol()
    {
        // check if the navmesh has reached it's current distance
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            //determine a new destination using the range and the positition
            Vector3 newDestination = transform.position + Random.insideUnitSphere * range;

            //check if this point is on the navmesh, if so, set it as the new destination
            NavMeshHit navMeshHit;
            if (NavMesh.SamplePosition(newDestination, out navMeshHit, 1.0f, NavMesh.AllAreas))
            {
                navMeshAgent.SetDestination(newDestination);
            }
            // could still improve this by having an alternative solution if the plane can't make the move. For now it just tries again.
        }
    }


    public void Park(Vector3 newDestination)
    {
        _isPatrolling = false; // no longer patrolling
        
        navMeshAgent.SetDestination(newDestination); // set new destination given by gamemanager
    }

    // turn light on
    public void LightsOn()
    {
        light.enabled = true;
    }

    // turn light off
    public void LightsOff()
    {
        light.enabled = false;
    }
}

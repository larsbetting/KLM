using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine;

public class Plane : MonoBehaviour
{
    // reference to the planeData class
    public PlaneData planeData;

    // reference to the navMeshAgent that controlls the object this script is attatched to
    public NavMeshAgent navMeshAgent;

    [SerializeField]
    private float range;

    private bool isPatrolling = true;
    public string brand { get; private set; }
    public string type { get; private set; }

    public new Light light;
    // Start is called before the first frame update
    void Start()
    {
        //initialize the components from the scriptable object
        //ID = planeData.ID;
        brand = planeData.brand;
        type = planeData.type;
        gameObject.GetComponent<Renderer>().material = planeData.material;
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;


        light.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPatrolling)
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            Vector3 newDestination = transform.position + Random.insideUnitSphere * range;
            NavMeshHit navMeshHit;
            if (NavMesh.SamplePosition(newDestination, out navMeshHit, 1.0f, NavMesh.AllAreas))
            {
                navMeshAgent.SetDestination(newDestination);
            }

        }
    }

    public void Park(Vector3 newDestination)
    {
        isPatrolling = false;
        
        navMeshAgent.SetDestination(newDestination);
    }

    public void LightsOn()
    {
        light.enabled = true;
    }

    public void LightsOff()
    {
        light.enabled = false;
    }
}

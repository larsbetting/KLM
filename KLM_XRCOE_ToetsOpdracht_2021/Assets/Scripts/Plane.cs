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

    public string brand { get; private set; }
    public string type { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        //initialize the components from the scriptable object
        //ID = planeData.ID;
        brand = planeData.brand;
        type = planeData.type;
        //IDText.text = ID.ToString(); // convert the ID to a string to be displayed on top of the plane
        gameObject.GetComponent<Renderer>().material = planeData.material;
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        //navMeshAgent.SetDestination(new Vector3(Random.Range(-10.0f, 10.0f), 0.0f, Random.Range(-10.0f, 10.0f)));
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
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
}

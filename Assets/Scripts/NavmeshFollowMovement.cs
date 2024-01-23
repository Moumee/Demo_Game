using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavmeshFollowMovement : MonoBehaviour
{
    public Transform player;
    bool playerInRange;
    public LayerMask playerLayer;
    NavMeshAgent agent;
    float range = 7f;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInRange = Physics.CheckSphere(transform.position, range, playerLayer);
        if (!playerInRange)
            Patrol();
        else
        {
            Chase();
            if (agent.remainingDistance > range)
            {
                Patrol();
            }
        }
        

    }

    private void Patrol()
    {
        if (agent.remainingDistance <= 1f) //done with path
        {
            Vector3 point;
            if (RandomPoint(transform.position, range, out point)) //pass in our centre point and radius of area
            {
                /*Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f)*/ //so you can see with gizmos
                agent.SetDestination(point);
            }
        }
    }


    void Chase()
    {
        agent.SetDestination(player.position);
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        {
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}

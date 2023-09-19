using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveAI : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;

    [SerializeField] List<Transform> waypoints;
    private int currentWaypoint;

    private AIState currentState;
    private Transform hostileSpotted = null;

    public enum AIState
    {
        Patrol,
        Chase,
    }

    private void Start()
    {
        agent.SetDestination(waypoints[currentWaypoint].position);

        currentState = AIState.Patrol;
    }

    private void Update()
    {

        if (currentState == AIState.Patrol)
        {
            if (agent.remainingDistance <= 0)
            {
                currentWaypoint = (currentWaypoint + 1) % waypoints.Count;
            }

            agent.SetDestination(waypoints[currentWaypoint].position);
        }
       else if (currentState == AIState.Chase)
        {
            agent.SetDestination(hostileSpotted.position);
            if (Vector3.Distance(transform.position, hostileSpotted.position) >= 10)
            {
                hostileSpotted = null;
                currentState = AIState.Patrol;
            }
        }
    }

    public void setHostile(Transform hostile)
    {
        hostileSpotted = hostile;
        currentState = AIState.Chase;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderingAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 target;

    [SerializeField] private float agentRandomInterval = 5;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = new Vector3(transform.position.x + Random.Range(-5, 5), transform.position.y, transform.position.z + Random.Range(-5, 5));
        agent.SetDestination(target);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= agentRandomInterval)
        {
            target = new Vector3(transform.position.x + Random.Range(-10, 10), transform.position.y, transform.position.z + Random.Range(-10, 10));
            agent.SetDestination(target);
            timer = 0;
        }
        
    }
}

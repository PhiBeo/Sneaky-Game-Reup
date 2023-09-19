using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChasingAi : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] private NavMeshAgent agent;

    private bool _isChase;
    void Start()
    {
        _isChase = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(_isChase)
        {
            agent.SetDestination(player.position);
        }
    }

    public void setChase(bool isChase)
    {
        _isChase = isChase;
    }
}

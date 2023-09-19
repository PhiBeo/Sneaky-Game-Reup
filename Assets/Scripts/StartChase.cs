using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartChase : MonoBehaviour
{
    [SerializeField] private ChasingAi Agent;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Agent.setChase(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VisionBox : MonoBehaviour
{
    [SerializeField] private MoveAI movingAgent;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //linecast check
            RaycastHit hit;
            if(Physics.Linecast(movingAgent.transform.position, other.transform.position, out hit))
            {
                //debug draw a line between capsule and hostile
                //Debug.DrawLine(movingAgent.transform.position, hit.point, Color.red, 5);
                if (hit.transform.tag == "Player")
                {
                    //set the hostile as target
                    movingAgent.setHostile(other.transform);

                }
            }
        }
    }
}

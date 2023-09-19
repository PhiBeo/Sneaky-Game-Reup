using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckKey : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(GameManager.Instant.hadKey())
            {
                GameManager.Instant.useKey();
                gameObject.SetActive(false);
            }
        }
    }
}

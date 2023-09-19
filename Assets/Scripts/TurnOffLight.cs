using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffLight : MonoBehaviour
{
    [SerializeField] Light switchLight;
    void Update()
    {
        switchLight.enabled = !GameManager.Instant.isTurnOffLight();
    }
}

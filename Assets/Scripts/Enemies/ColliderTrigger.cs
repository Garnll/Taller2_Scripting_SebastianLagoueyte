using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ColliderTrigger : MonoBehaviour {

    [HideInInspector]
    public bool playerDetected = false;
    [HideInInspector]
    public Collider player = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerDetected = true;
            player = other;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerDetected = false;
            player = null;
        }
    }
}

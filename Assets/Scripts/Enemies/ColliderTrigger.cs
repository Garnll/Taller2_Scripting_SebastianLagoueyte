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
            if (!other.GetComponent<PlayerMovement>().EnSigilo)
            {
                playerDetected = true;
                player = other;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if ((other.transform.position - GetComponentInParent<EnemyBaseMovement>().gameObject.transform.position).magnitude < 2 || !other.GetComponent<PlayerMovement>().EnSigilo)
            {
                playerDetected = true;
                player = other;
            }
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

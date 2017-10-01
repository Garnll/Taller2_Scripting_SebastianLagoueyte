using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Rigidbody))]
public class EnemyBaseMovement : MonoBehaviour {

    [SerializeField]
    protected ColliderTrigger areaOfView = null;
    [SerializeField]
    protected Transform[] targets;

    protected NavMeshAgent agent;

    private Vector3 originPosition;

    protected string estadoActual;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        if (areaOfView == null)
        {
            try
            {
                areaOfView = GetComponentInChildren<ColliderTrigger>();
            }
            catch
            {
                Debug.LogError("No hay ColliderTrigger en un hijo del enemigo");
            }
        }
        originPosition = transform.position;

        GameController.EnReinicio += Reinicio;

        estadoActual = "Cambiar Objetivo";
	}

    protected virtual void Update()
    {
        if (areaOfView.playerDetected)
        {
            estadoActual = "Buscar Jugador";
        }
        else
        {
            if (estadoActual != "Moviendo")
            {
                estadoActual = "Cambiar Objetivo";
            }

            if (agent.remainingDistance < agent.stoppingDistance)
            {

            }
        }


        switch (estadoActual)
        {
            case "Moviendo":
                Move(agent.destination);
                break;

            case "Buscar Jugador":
                    PlayerSpotted(areaOfView.player);
                break;

            case "Cambiar Objetivo":
                Move(ChangeTarget(0));
                break;

            default:
                Move(agent.destination);
                break;
        }


    }

    protected virtual void Move(Vector3 currentTarget)
    {
        agent.SetDestination(currentTarget);
    }

    protected virtual Vector3 ChangeTarget(int whichOne)
    {
        agent.speed = 3;
        agent.angularSpeed = 90;
        agent.acceleration = 1;

        estadoActual = "Moviendo";
        return (targets[whichOne].position);
    }

    protected virtual void PlayerSpotted(Collider other)
    {
        agent.speed = 5;
        agent.angularSpeed = 200;
        agent.acceleration = 5;

        agent.SetDestination(other.transform.position);
    }

    protected virtual void AtackPlayer(Collider other)
    {

    }

    private void Reinicio()
    {
        gameObject.SetActive(true);
        agent.Warp(originPosition);
    }

    public void Calling(Collider other)
    {
        agent.speed = 5;
        agent.angularSpeed = 200;
        agent.acceleration = 5;

        agent.SetDestination(other.transform.position);
    }


    private void OnDestroy()
    {
        GameController.EnReinicio -= Reinicio;
    }
}

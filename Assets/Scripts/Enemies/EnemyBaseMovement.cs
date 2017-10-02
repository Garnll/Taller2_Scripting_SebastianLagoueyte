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
    [SerializeField]
    protected float tiempoDeReposo = 3;
    [SerializeField]
    protected int daño = 25;

    protected NavMeshAgent agent;
    protected float tiempo = 0;

    private Vector3 originPosition;
    private Quaternion originRotation;
    protected Vector3 playerLastPosition;

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
        originRotation = transform.rotation;

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
        }


        switch (estadoActual)
        {
            case "Moviendo":
                Move(agent.destination);
                break;

            case "Buscar Jugador":
                PlayerSpotted(areaOfView.player);
                break;

            case "Jugador Perdido":
                SearchForPlayer(playerLastPosition);
                break;

            case "Cambiar Objetivo":
                Move(ChangeTarget(0));
                break;

            default:
                Move(agent.destination);
                break;
        }


    }

    protected void LateUpdate()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            ChangeRotation();
        }
    }

    protected virtual void ChangeRotation()
    {
        transform.localRotation = Quaternion.Lerp(transform.rotation, originRotation, Time.deltaTime / 0.5f);
    }

    protected virtual void Move(Vector3 currentTarget)
    {
        agent.SetDestination(currentTarget);
    }

    protected virtual Vector3 ChangeTarget(int whichOne)
    {
        agent.speed = 3;
        agent.angularSpeed = 120;
        agent.acceleration = 5;

        estadoActual = "Moviendo";
        return (targets[whichOne].position);
    }


    protected virtual void SearchForPlayer(Vector3 lastKnownPosition)
    {
        agent.SetDestination(lastKnownPosition);
        int rotar = Random.Range(1, 100);
        if (rotar == 1)
        {
            float angulo = Random.Range(0, 270);
            transform.Rotate(Vector3.up, angulo * Time.deltaTime);
        }
    }

    protected virtual void PlayerSpotted(Collider other)
    {
        agent.speed = 5;
        agent.angularSpeed = 200;
        agent.acceleration = 5;

        playerLastPosition = other.transform.position;

        agent.SetDestination(other.transform.position);
    }

    protected virtual void AtackPlayer(Collider other)
    {
        other.GetComponent<PlayerStats>().RecibirDaño(daño);
    }


    public virtual void Morir()
    {
        StopAllCoroutines();
        gameObject.SetActive(false);
    }


    private void Reinicio()
    {
        gameObject.SetActive(true);
        agent.Warp(originPosition);
    }

    public void Calling(Vector3 other)
    {
        agent.speed = 5;
        agent.angularSpeed = 200;
        agent.acceleration = 5;

        agent.SetDestination(other);
    }

    private void OnDestroy()
    {
        GameController.EnReinicio -= Reinicio;
    }
}

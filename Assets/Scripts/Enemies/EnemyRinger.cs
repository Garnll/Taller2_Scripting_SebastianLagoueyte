using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRinger : EnemyBaseMovement
{
    [SerializeField]
    private float distanciaMaxima = 10;

    protected override void Update()
    {
        if (areaOfView.playerDetected)
        {
            estadoActual = "Alertar";
        }
        else
        {
            if (estadoActual != "Volviendo a puesto")
            {
                estadoActual = "Cambiar Objetivo";
            }
        }

        switch (estadoActual)
        {
            case "Volviendo a puesto":
                Move(agent.destination);
                break;

            case "Alertar":
                PlayerSpotted(areaOfView.player);
                break;

            case "Cambiar Objetivo":
                Move(ChangeTarget(0));
                estadoActual = "Volviendo a puesto";
                break;

            default:
                Move(agent.destination);
                break;
        }
    }

    protected override void PlayerSpotted(Collider player)
    {
        agent.speed = 7.8f;
        agent.angularSpeed = 200;
        agent.acceleration = 10;
        if (agent.remainingDistance <= agent.stoppingDistance)
        {

            agent.SetDestination(ChangeTarget(Random.Range(0, targets.Length)));
        }

        int layerMasker = 1 << LayerMask.NameToLayer("Enemy");

        Collider[] otherEnemies = Physics.OverlapSphere(transform.position, distanciaMaxima, layerMasker);

        foreach (Collider myEnemies in otherEnemies)
        {
            Debug.Log(myEnemies);
            try
            {
                myEnemies.GetComponent<EnemyGuard>().Calling(player);
            }
            catch (System.NullReferenceException)
            {
                try
                {
                    myEnemies.GetComponent<EnemyPatrol>().Calling(player);
                }
                catch
                {
                    Debug.Log("Lamando a otros");
                }
            }
        }
    }

}

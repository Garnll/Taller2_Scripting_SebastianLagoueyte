using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGuard : EnemyBaseMovement
{
    [SerializeField]
    private float distanciaMaxima = 10;

    protected override void Update()
    {
        if (areaOfView.playerDetected)
        {
            estadoActual = "Seguir Jugador";
        }
        else
        {
            if (estadoActual != "Volviendo a puesto")
            {
                estadoActual = "Cambiar Objetivo";
            }
        }

        if ((agent.transform.position - targets[0].position).magnitude > distanciaMaxima)
        {
            if (estadoActual != "Volviendo a puesto")
            {
                estadoActual = "Cambiar Objetivo";
            }
        }

        if (agent.remainingDistance <= agent.stoppingDistance && agent.destination != targets[0].position)
        {
            estadoActual = "Cambiar Objetivo";
        }

        switch (estadoActual)
        {
            case "Volviendo a puesto":
                Move(agent.destination);
                break;

            case "Seguir Jugador":
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
}

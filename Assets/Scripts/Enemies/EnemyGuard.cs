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
            if (estadoActual != "Atacar Jugador")
            {
                estadoActual = "Seguir Jugador";
            }
            tiempo = 0;
        }
        else if (estadoActual == "Seguir Jugador" || estadoActual == "Buscar Jugador")
        {
            tiempo += Time.deltaTime;
            estadoActual = "Buscar Jugador";
            if (tiempo >= tiempoDeReposo)
            {
                tiempo = 0;
                estadoActual = "Cambiar Objetivo";
            }
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



        switch (estadoActual)
        {
            case "Volviendo a puesto":
                Move(agent.destination);
                break;

            case "Seguir Jugador":
                PlayerSpotted(areaOfView.player);
                break;

            case "Atacar Jugador":
                AtackPlayer(areaOfView.player);
                break;

            case "Cambiar Objetivo":
                Move(ChangeTarget(0));
                estadoActual = "Volviendo a puesto";
                break;

            case "Buscar Jugador":
                SearchForPlayer(playerLastPosition);
                break;

            default:
                Move(agent.destination);
                break;
        }

        if ((estadoActual == "Seguir Jugador" || estadoActual == "Atacar Jugador") && agent.remainingDistance < agent.stoppingDistance)
        {
            estadoActual = "Atacar Jugador";
            AtackPlayer(areaOfView.player);
        }
        else if (areaOfView.playerDetected)
        {
            estadoActual = "Seguir Jugador";
        }

        if ((estadoActual != "Seguir Jugador" && estadoActual != "Buscar Jugador") && agent.remainingDistance <= agent.stoppingDistance)
        {
            estadoActual = "Cambiar Objetivo";
        }

    }
}

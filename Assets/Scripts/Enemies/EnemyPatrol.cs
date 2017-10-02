using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : EnemyBaseMovement
{
    [SerializeField]
    private float distanciaMaxima = 10;


    private Vector3 ultimaPosicion;
    private int indexLugar = 0;

    protected override void Update()
    {
        if (areaOfView.playerDetected)
        {
            if (ultimaPosicion == Vector3.zero)
            {
                ultimaPosicion = transform.position;
            }
            if (estadoActual != "Atacar Jugador")
            {
                estadoActual = "Buscar Jugador";
            }
            tiempo = 0;
        }
        else if (estadoActual == "Buscar Jugador" || estadoActual == "Perder Jugador")
        {
            tiempo += Time.deltaTime;
            estadoActual = "Perder Jugador";
            if (tiempo >= tiempoDeReposo)
            {
                tiempo = 0;
                estadoActual = "Cambiar Objetivo";
            }
        }
        else
        {
            if (ultimaPosicion != Vector3.zero)
            {
                ultimaPosicion = Vector3.zero;
            }

            if (estadoActual != "Patrullando")
            {
                estadoActual = "Cambiar Objetivo";
            }
        }


        if (ultimaPosicion != Vector3.zero && (transform.position - ultimaPosicion).magnitude > distanciaMaxima)
        {
            if (estadoActual != "Patrullando")
            {
                estadoActual = "Patrullando";
                VolverAUltimaPosicion();
            }
        }

        if (agent.remainingDistance <= agent.stoppingDistance && estadoActual != "Buscar Jugador" && estadoActual != "Perder Jugador" && estadoActual != "Atacar Jugador")
        {
            estadoActual = "Cambiar Objetivo";
            if (ultimaPosicion != Vector3.zero)
            {
                ultimaPosicion = Vector3.zero;
            }
        }


        switch (estadoActual)
        {
            case "Patrullando":
                Move(agent.destination);
                break;

            case "Buscar Jugador":
                PlayerSpotted(areaOfView.player);
                break;

            case "Atacar Jugador":
                AtackPlayer(areaOfView.player);
                break;

            case "Perder Jugador":
                SearchForPlayer(playerLastPosition);
                break;

            case "Cambiar Objetivo":
                Move(ChangeTarget(indexLugar));
                estadoActual = "Patrullando";
                indexLugar++;
                if (indexLugar >= targets.Length)
                {
                    indexLugar = 0;
                }
                break;

            default:
                Move(agent.destination);
                break;
        }

        if ((estadoActual == "Buscar Jugador" || estadoActual == "Atacar Jugador") && (areaOfView.player.transform.position - transform.position).magnitude < 1)
        {
            estadoActual = "Atacar Jugador";
            AtackPlayer(areaOfView.player);
        }
        else if (areaOfView.playerDetected)
        {
            estadoActual = "Seguir Jugador";
        }
    }

    private void VolverAUltimaPosicion()
    {
        Move(ultimaPosicion);
    }
}

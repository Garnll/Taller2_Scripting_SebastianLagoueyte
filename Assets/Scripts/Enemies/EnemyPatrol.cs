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
            estadoActual = "Buscar Jugador";
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

        if (agent.remainingDistance <= agent.stoppingDistance && estadoActual != "Buscar Jugador")
        {
            estadoActual = "Cambiar Objetivo";
            if (ultimaPosicion != Vector3.zero)
            {
                ultimaPosicion = Vector3.zero;
            }
        }

        Debug.Log(estadoActual);

        switch (estadoActual)
        {
            case "Patrullando":
                Move(agent.destination);
                break;

            case "Buscar Jugador":
                PlayerSpotted(areaOfView.player);
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
    }

    private void VolverAUltimaPosicion()
    {
        Move(ultimaPosicion);
    }
}

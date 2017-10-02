using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRinger : EnemyBaseMovement
{
    [SerializeField]
    private float distanciaMaxima = 100;

    private bool llamando = false;
    private int jugadorVisto = 0;

    protected override void Update()
    {
        if (areaOfView.playerDetected)
        {
            estadoActual = "Alertar";
            tiempo = 0;
            jugadorVisto++;
        }
        else if (estadoActual == "Alertar")
        {
            tiempo += Time.deltaTime;
            if (tiempo >= tiempoDeReposo)
            {
                tiempo = 0;
                estadoActual = "Cambiar Objetivo";
                jugadorVisto = 0;
            }
        }
        else
        {
            if (estadoActual != "Volviendo a puesto")
            {
                jugadorVisto = 0;
                estadoActual = "Cambiar Objetivo";
            }
        }

        if (agent.remainingDistance <= agent.stoppingDistance && estadoActual == "Alertar")
        {
            agent.SetDestination(ChangeTarget(Random.Range(0, targets.Length)));
        }


        switch (estadoActual)
        {
            case "Volviendo a puesto":
                Move(agent.destination);

                break;

            case "Alertar":
                agent.speed = 7.8f;
                agent.angularSpeed = 200;
                agent.acceleration = 10;
                if (jugadorVisto <= 1)
                {
                    agent.SetDestination(ChangeTarget(Random.Range(0, targets.Length)));
                }
                if (!llamando)
                {
                    llamando = true;
                    StartCoroutine(LlamarAOtros());
                }
                break;

            case "Cambiar Objetivo":
                llamando = false;
                StopAllCoroutines();
                Move(ChangeTarget(0));
                estadoActual = "Volviendo a puesto";
                break;

            default:
                Move(agent.destination);
                break;
        }
    }


    private IEnumerator LlamarAOtros()
    {
        yield return new WaitForSeconds(2);
        PlayerSpotted(areaOfView.player);
        llamando = false;
    }

    protected override void PlayerSpotted(Collider player)
    {
        if (player != null)
        {
            playerLastPosition = player.transform.position;
        }

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.SetDestination(ChangeTarget(Random.Range(0, targets.Length)));
        }

        int layerMasker = 1 << LayerMask.NameToLayer("Enemy");

        distanciaMaxima = 100;
        Collider[] otherEnemies = Physics.OverlapSphere(transform.position, distanciaMaxima, layerMasker);

        foreach (Collider myEnemies in otherEnemies)
        {
            try
            {
                myEnemies.GetComponent<EnemyGuard>().Calling(playerLastPosition);
            }
            catch (System.NullReferenceException)
            {
                try
                {
                    myEnemies.GetComponent<EnemyPatrol>().Calling(playerLastPosition);
                }
                catch
                {
                    Debug.Log("Lamando a otros");
                }
            }
        }
    }

}

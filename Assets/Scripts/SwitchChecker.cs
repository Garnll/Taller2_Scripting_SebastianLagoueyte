using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SwitchChecker : MonoBehaviour {

    private bool correcto = false;
    private GameController gm = null;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerMovement>().EnUsarBoton += CheckSwitch;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerMovement>().EnUsarBoton -= CheckSwitch;
        }
    }

    private void CheckSwitch()
    {
        if (gm == null)
        {
            gm = GameController.Instance;
            GameController.EnReinicio += Reiniciar;
        }


        if (gm.UsadosLength() < gm.SwitchesDelJuego)
        {
            Debug.Log("No se han presionado todos");
            return;
        }

        for (int i = 0; i < gm.UsadosLength(); i++)
        {
            correcto = gm.CheckSwitch();
            gm.switchesActivados = correcto;

            if (!correcto)
            {
                break;
            }
        }


        if (correcto)
        {
            gm.OpenUp();
            //abrir puertas
        }
    }

    private void Reiniciar()
    {
        correcto = false;

    }

    private void OnDestroy()
    {
        GameController.EnReinicio -= Reiniciar;
    }
}

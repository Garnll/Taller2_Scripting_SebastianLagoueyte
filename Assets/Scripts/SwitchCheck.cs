using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCheck : MonoBehaviour {

    private bool correcto = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerMovement>().EnUsarBoton += CheckSwitch;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerMovement>().EnUsarBoton -= CheckSwitch;
        }
    }

    private void CheckSwitch()
    {
        if (GameController.Instance.UsadosLength() < 3)
        {
            return;
        }

        for (int i = 0; i < GameController.Instance.UsadosLength(); i++)
        {
            correcto = GameController.Instance.CheckSwitch();

            if (!correcto)
            {
                break;
            }
        }


        if (correcto)
            {
                Debug.Log("You did it");
                //abrir puertas
            }
            else
            {
                Debug.Log("You did it not");
                //llamar guardias
            }
    }
}

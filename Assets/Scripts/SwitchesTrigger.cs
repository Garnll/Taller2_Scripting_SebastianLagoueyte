using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SwitchesTrigger : MonoBehaviour {

    private bool encendido;
    private bool activado;

    [SerializeField]
    private int id = 0;
    [SerializeField]
    private Material[] material = new Material[2];

    // Use this for initialization
    void Start ()
    {
        encendido = false;
        activado = false;
        GetComponent<Renderer>().material = material[0];

        GameController.EnDesorden += Reiniciar;
        GameController.EnReinicio += ReiniciarTotal;
	}


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player") && !encendido)
        {
            encendido = true;
            collision.GetComponent<PlayerMovement>().EnUsarBoton += ActivarSwitch;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player") && encendido)
        {
            encendido = false;
            collision.GetComponent<PlayerMovement>().EnUsarBoton -= ActivarSwitch;
        }
    }

    private void ActivarSwitch()
    {
        if (!activado && encendido)
        {
            activado = true;
            UIController.instance.SetSwitch(id, Color.yellow);
            GetComponent<Renderer>().material = material[1];
            GameController.Instance.ActivatedSwitch(this);
        }
    }

    private void Reiniciar()
    {
        activado = false;
        encendido = false;
        GetComponent<Renderer>().material = material[0];
        UIController.instance.SetSwitch(id, Color.white);
    }


    private void ReiniciarTotal()
    {
        activado = false;
        encendido = false;
        UIController.instance.SetSwitch(id, Color.white);
        GetComponent<Renderer>().material = material[0];
    }

    private void OnDestroy()
    {
        GameController.EnDesorden -= Reiniciar;
        GameController.EnReinicio -= ReiniciarTotal;
    }

    public int MyID()
    {
        return id;
    }
}

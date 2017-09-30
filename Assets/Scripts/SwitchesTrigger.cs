using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SwitchesTrigger : MonoBehaviour {

    private bool encendido;
    private bool activado;

    [SerializeField]
    private int id = 0;

	// Use this for initialization
	void Start ()
    {
        encendido = false;
        activado = false;

        GameController.EnDesorden += Reiniciar;
	}



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !encendido)
        {
            encendido = true;
            collision.GetComponent<PlayerMovement>().EnUsarBoton += ActivarSwitch;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
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
            GameController.Instance.ActivatedSwitch(this);
        }
    }

    private void Reiniciar()
    {
        activado = false;
        encendido = false;
    }

    private void OnDestroy()
    {
        GameController.EnDesorden -= Reiniciar;
    }

    public int MyID()
    {
        return id;
    }
}

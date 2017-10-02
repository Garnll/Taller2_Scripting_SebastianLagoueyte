using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class ObjetosBase : MonoBehaviour {

    protected Collider player = null;
    protected string tipo = "ninguno";
    protected string tiempo = "";

    public abstract void Activar();

    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other;
            if (other.GetComponent<PlayerMovement>().CheckDisponibility())
            {
                GameController.EnReinicio += Reinicio;
                other.GetComponent<PlayerMovement>().AddObject(this.gameObject);
                UIController.instance.SetObject(tipo, tipo, tiempo);
                Deactivate();
            }
        }
    }

    protected void Deactivate()
    {
        this.gameObject.SetActive(false);
    }

    protected virtual void Reinicio()
    {
        this.gameObject.SetActive(true);
        player.GetComponent<PlayerMovement>().RemoveObject();
        GameController.EnReinicio -= Reinicio;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RalentizarObjeto : ObjetosBase
{
    [SerializeField]
    private float duracion = 5;

    private float tiempoDeDuracion;

    private void Start()
    {
        tipo = "ralentizador";
        tiempoDeDuracion = duracion;
        tiempo = string.Format("{0} segundos", (int)tiempoDeDuracion);
    }

    public override void Activar()
    {
        Time.timeScale = 0.5f;
        tiempo = string.Format("{0} segundos", (int)tiempoDeDuracion);

        player.GetComponent<PlayerMovement>().multiplyVelocity = 1.5f;

        InvokeRepeating("PonerTexto", 0.1f, Time.deltaTime);
        Invoke("Quitar", duracion);
    }

    private void PonerTexto()
    {
        if (!(tiempoDeDuracion <= 0f))
        {
            tiempoDeDuracion -= Time.unscaledDeltaTime;
            tiempo = string.Format("{0} segundos", (int)tiempoDeDuracion + 1);
            UIController.instance.SetObject(tipo, tipo, tiempo);
        }
        else
        {
            CancelInvoke("PonerTexto");
            tiempoDeDuracion = duracion;
            UIController.instance.SetObject("ninguno", "ninguno");
        }
    }

    private void Quitar()
    {
        CancelInvoke("PonerTexto");
        tiempoDeDuracion = duracion;
        Time.timeScale = 1f;
        player.GetComponent<PlayerMovement>().multiplyVelocity = 1;
        player.GetComponent<PlayerMovement>().RemoveObject();
    }

    protected override void Reinicio()
    {
        base.Reinicio();
        tiempoDeDuracion = duracion;
        Time.timeScale = 1f;
        player.GetComponent<PlayerMovement>().multiplyVelocity = 1;
    }
}

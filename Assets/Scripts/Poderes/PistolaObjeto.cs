using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolaObjeto : ObjetosBase
{
    [SerializeField]
    private int distancia = 50;


    private int balas = 3;

    private void Start()
    {
        tipo = "arma";
        tiempo = string.Format("{0} disparos", balas);
    }

    public override void Activar()
    {
        balas--;

        player.GetComponentInChildren<ParticleSystem>().Play();

        RaycastHit hit;

        int layer = 1 << LayerMask.NameToLayer("Enemy");

        if (Physics.Raycast (player.transform.position, player.transform.forward, out hit, distancia, layer))
        {
            Debug.Log(hit.collider);
            if (hit.collider.CompareTag("Enemy"))
            {
  
                hit.collider.GetComponent<EnemyBaseMovement>().Morir();
            }
        }
        tiempo = string.Format("{0} disparos", balas.ToString());
        UIController.instance.SetObject(tipo, tipo, tiempo);

        if (balas <= 0)
        {
            player.GetComponent<PlayerMovement>().RemoveObject();
        }
    }

    protected override void Reinicio()
    {
        balas = 3;
        tiempo = string.Format("{0} disparos", balas.ToString());
        base.Reinicio();
    }
}

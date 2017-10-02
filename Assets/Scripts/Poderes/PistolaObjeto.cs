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
        tiempo = string.Format("{0} disparos", balas);

        RaycastHit hit;

        int layer =~ 1 << LayerMask.NameToLayer("Default");

        if (Physics.Raycast (player.transform.position, player.transform.forward, out hit, 50, layer))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<EnemyBaseMovement>().Morir();
            }
        }
        if (balas < 0)
        {
            player.GetComponent<PlayerMovement>().RemoveObject();
        }
    }

    protected override void Reinicio()
    {
        base.Reinicio();
        balas = 3;
    }
}

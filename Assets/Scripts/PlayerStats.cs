using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    [SerializeField]
    private int hp = 100;
    [SerializeField]
    private int invincibleFrames = 2;

    private bool invincible = false;
    public static bool muerto = false;

    // Use this for initialization
    void Start () {
        UIController.instance.SetHP(hp);
        GameController.EnReinicio += Reiniciar;
	}

    private void Reiniciar()
    {
        invincible = false;
        muerto = false;
        hp = 100;
        UIController.instance.SetHP(hp);
    }

    public void RecibirDaño(int daño)
    {
        if (!invincible && hp > 0)
        {
            invincible = true;
            hp -= daño;
            UIController.instance.SetHP(hp);
            Invoke("QuitarInvensibilidad", invincibleFrames);

            if (hp <= 0)
            {
                Morir();
            }
        }
    }

    private void Morir()
    {
        Debug.Log("He muerto");
        CancelInvoke("QuitarInvensibilidad");
        muerto = true;
        GameController.Instance.Loser();
    }

    private void QuitarInvensibilidad()
    {
        invincible = false;
    }
}

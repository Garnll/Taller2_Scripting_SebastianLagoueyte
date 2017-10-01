using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController 
{
    private static GameController instance = null;

    public static GameController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameController();
            }
            return instance;
        }
    }


    public delegate void Reiniciar();
    public static event Reiniciar EnDesorden;
    public static event Reiniciar EnReinicio;

    public delegate void AbrirPuertas();
    public static event AbrirPuertas EnAbrir;
    public static event AbrirPuertas EnFinal;

    private Stack<SwitchesTrigger> switchesUsados = new Stack<SwitchesTrigger>(5);
    private Stack<SwitchesTrigger> switchesCheckeados = new Stack<SwitchesTrigger>(5);

    private GameObject[] switchesDelJuego;

    private int ordenDelCheck;

    public bool switchesActivados = false;

    public void SetGameObject(GameObject[] switches)
    {
        switchesDelJuego = switches;
        ordenDelCheck = switchesDelJuego.Length;
    }

    public int SwitchesDelJuego
    {
        get
        {
            return switchesDelJuego.Length;
        }
    }

    public void ActivatedSwitch(SwitchesTrigger switchYo)
    {
        if (!switchesUsados.Contains(switchYo))
        {
            switchesUsados.Push(switchYo);
        }
    }

    public int UsadosLength()
    {
        return switchesUsados.Count;
    }

    public bool CheckSwitch()
    {
        switchesCheckeados.Push(switchesUsados.Pop());

        if (switchesCheckeados.Peek().MyID() == ordenDelCheck)
        {
            ordenDelCheck--;
            return true;
        }
        else
        {
            ordenDelCheck = switchesDelJuego.Length;
            switchesUsados.Clear();
            switchesCheckeados.Clear();

            if (EnDesorden != null)
            {
                EnDesorden();
            }
            else
            {
                Debug.LogError("No hay quien reciba Desorden");
            }

            return false;
        }
    }

    public void OpenUp()
    {
        EnAbrir();
    }

    public void Winner()
    {
        if (switchesActivados)
        {
            EnFinal();
            Debug.Log("You are winner");
        }
        else
        {
            Debug.Log("Nice try, go press the switches");
        }
    }

    public void ReinicioCompleto()
    {
        switchesActivados = false;
        ordenDelCheck = switchesDelJuego.Length;
        switchesUsados.Clear();
        switchesCheckeados.Clear();
        EnReinicio();
    }
}

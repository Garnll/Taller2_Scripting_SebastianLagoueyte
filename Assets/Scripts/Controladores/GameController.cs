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
    public static event Reiniciar EnMuerte;

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
        UIController.instance.SetWinLose("");
        UIController.instance.SetDoor(false, true);
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
            UIController.instance.SetSwitch(ordenDelCheck, Color.green);
            ordenDelCheck--;
            return true;
        }
        else
        {
            for (int i = 0; i < switchesDelJuego.Length; i++)
            {
                UIController.instance.SetSwitch(i + 1, Color.red);
            }
            ordenDelCheck = switchesDelJuego.Length;
            switchesUsados.Clear();
            switchesCheckeados.Clear();

            if (EnDesorden != null)
            {
                EnDesorden();
                UIController.instance.SetDoor(false);
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
        for (int i = 0; i < switchesDelJuego.Length; i++)
        {
            UIController.instance.SetSwitch(i + 1, Color.green);
        }
        UIController.instance.SetDoor(true);
        EnAbrir();
    }

    public void Winner()
    {
        if (switchesActivados)
        {
            EnFinal();
            UIController.instance.SetWinLose("You are winner");
            Debug.Log("You are winner");
        }
        else
        {
            Debug.Log("Nice try, go press the switches");
        }
    }

    public void Loser()
    {
        UIController.instance.SetWinLose("You have died");
        EnMuerte();
    }

    public void ReinicioCompleto()
    {
        UIController.instance.SetWinLose("");
        UIController.instance.SetDoor(false, true);
        switchesActivados = false;
        ordenDelCheck = switchesDelJuego.Length;
        switchesUsados.Clear();
        switchesCheckeados.Clear();
        EnReinicio();
    }
}

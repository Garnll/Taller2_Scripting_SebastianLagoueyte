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

    private Stack<SwitchesTrigger> switchesUsados = new Stack<SwitchesTrigger>(5);
    private Stack<SwitchesTrigger> switchesCheckeados = new Stack<SwitchesTrigger>(5);

    private GameObject[] switchesDelJuego;

    private int ordenDelCheck = 1;

    public void SetGameObject(GameObject[] switches)
    {
        switchesDelJuego = switches;
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
            ordenDelCheck++;
            return true;
        }
        else
        {
            ordenDelCheck = 1;
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

}

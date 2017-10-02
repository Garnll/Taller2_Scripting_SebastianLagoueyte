using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReceiver : MonoBehaviour {

    [SerializeField]
    private GameObject[] switches;

	// Use this for initialization
	void Start ()
    {
        GameController.Instance.SetGameObject(switches);
        GameController.EnFinal += Reiniciar;
        GameController.EnMuerte += Reiniciar;
    }


    private void Reiniciar()
    {
        Invoke("ReiniciarTodo", 5);
    }

    private void ReiniciarTodo()
    {
        GameController.Instance.ReinicioCompleto();
    }

    private void OnDestroy()
    {
        GameController.EnFinal -= Reiniciar;
    }

}

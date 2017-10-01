using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBlocker : MonoBehaviour {



	// Use this for initialization
	void Start () {
        GameController.EnAbrir += Desaparecer;
        GameController.EnReinicio += Reiniciar;
	}

    private void Desaparecer()
    {
        gameObject.SetActive(false);
    }

    private void Reiniciar()
    {
        gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        GameController.EnReinicio -= Reiniciar;
        GameController.EnAbrir -= Desaparecer;
    }

}

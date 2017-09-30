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
	}
	
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraMovement : MonoBehaviour {

    [SerializeField]
    private Transform jugador;
    [SerializeField]
    private float tiempo = 2;

    private float offset;

	// Use this for initialization
	void Start () {
        offset = jugador.position.y - transform.position.y;
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        Vector3 newPosition = new Vector3(jugador.position.x, jugador.position.y - offset, jugador.position.z);
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime / tiempo);
	}
}

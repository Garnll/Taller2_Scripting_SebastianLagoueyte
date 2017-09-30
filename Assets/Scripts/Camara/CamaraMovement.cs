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
        offset = Mathf.Abs(jugador.position.z - transform.position.z);
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        Vector3 newPosition = new Vector3(jugador.position.x, jugador.position.y, jugador.position.z - offset);
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime / tiempo);
	}
}

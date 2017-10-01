using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    private int maxVelocity = 8;
    [SerializeField]
    private float aceleration = 0.1f;
    [SerializeField]
    private float speedDown = 2;
    
    private Rigidbody rb;
    private Vector3 originalPosition;

    private bool sigilo;
    

    public delegate void Activar();
    public event Activar EnUsarBoton;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        originalPosition = transform.position;
        sigilo = false;
        if (rb.useGravity)
        {
            rb.useGravity = false;
        }

        GameController.EnReinicio += Reiniciar;
	}

    // Update is called once per frame
    void Update ()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            sigilo = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            sigilo = false;
        }

        if (h != 0 || v != 0)
        {
            Mover(new Vector3(h, 0, v));
        }
        else
        {
            Detener();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Usar();
        }
    }



    private void Usar()
    {
        if (EnUsarBoton != null)
        {
            EnUsarBoton();
        }
    }

    private void Detener()
    {
        rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, Time.deltaTime / aceleration * 5);
    }

    private void Mover(Vector3 movimiento)
    {
        movimiento = movimiento.normalized;

        if (!sigilo)
        {
            rb.velocity = Vector3.Lerp(rb.velocity, movimiento * maxVelocity, Time.deltaTime / aceleration);
        }
        else
        {
            rb.velocity = Vector3.Lerp(rb.velocity, movimiento * (maxVelocity/speedDown), Time.deltaTime / aceleration);
        }
    }



    private void Reiniciar()
    {
        transform.position = originalPosition;
    }

    private void OnDestroy()
    {
        GameController.EnReinicio -= Reiniciar;
    }
}

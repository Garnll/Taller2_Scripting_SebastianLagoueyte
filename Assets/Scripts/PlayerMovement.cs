using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    private int maxVelocity = 500;
    [SerializeField]
    private float aceleration = 5;

    private Rigidbody2D rb2d;

    public delegate void Activar();
    public event Activar EnUsarBoton;

	// Use this for initialization
	void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
        if (rb2d.gravityScale > 0)
        {
            rb2d.gravityScale = 0;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (h != 0 || v != 0)
        {
            Mover(new Vector2(h, v));
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
        rb2d.velocity = Vector2.Lerp(rb2d.velocity, Vector2.zero, Time.deltaTime / aceleration * 5);
    }

    private void Mover(Vector2 movimiento)
    {
        movimiento = movimiento.normalized;

        rb2d.velocity = Vector2.Lerp(rb2d.velocity, movimiento * maxVelocity, Time.deltaTime / aceleration);
    }
}

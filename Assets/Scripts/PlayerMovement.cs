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

    [HideInInspector]
    public float multiplyVelocity;

    private GameObject objetoAUsar;

    //Codigo desde el tutorial de unity de Survival Shooter debido a que la rotación del objeto en 3d se me hacía imposible desde el primer ejercicio de la clase
    int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
    float camRayLength = 100f;          // The length of the ray from the camera into the scene. 

    public delegate void Activar();
    public event Activar EnUsarBoton;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        floorMask = LayerMask.GetMask("Floor");
        originalPosition = transform.position;
        multiplyVelocity = 1;
        sigilo = false;
        if (rb.useGravity)
        {
            rb.useGravity = false;
        }


        UIController.instance.SetSigilo(sigilo);
        UIController.instance.SetObject("ninguno", "ninguno");

        GameController.EnReinicio += Reiniciar;
    }

    // Update is called once per frame
    void Update ()
    {
        if (PlayerStats.muerto == false)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                sigilo = true;
                UIController.instance.SetSigilo(sigilo);
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                sigilo = false;
                UIController.instance.SetSigilo(sigilo);
            }

            if (h != 0 || v != 0)
            {
                Mover(new Vector3(h, 0, v));
            }
            else
            {
                Detener();
            }

            Turning();

            if (Input.GetButtonDown("Fire1"))
            {
                UsarPoder();
            }

            if (Input.GetButtonDown("Fire2"))
            {
                Usar();
            }
        }
    }

    private void UsarPoder()
    {
        if (objetoAUsar != null)
        {
            objetoAUsar.GetComponent<ObjetosBase>().Activar();
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
            rb.velocity = Vector3.Lerp(rb.velocity, movimiento * (maxVelocity * multiplyVelocity), Time.deltaTime / aceleration);
        }
        else
        {
            rb.velocity = Vector3.Lerp(rb.velocity, movimiento * (maxVelocity/speedDown), Time.deltaTime / aceleration);
        }
    }

    void Turning() //Codigo de tutorial de unity
    {
        // Create a ray from the mouse cursor on screen in the direction of the camera.
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit floorHit;

        // Perform the raycast and if it hits something on the floor layer...
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            Vector3 playerToMouse = floorHit.point - transform.position;

            // Ensure the vector is entirely along the floor plane.
            playerToMouse.y = 0f;

            // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            // Set the player's rotation to this new rotation.
            rb.MoveRotation(newRotation);
        }
    }

    public bool CheckDisponibility()
    {
        return objetoAUsar == null;
    }

    public void AddObject(GameObject objeto)
    {
        objetoAUsar = objeto;
    }

    public void RemoveObject()
    {
        Debug.Log("hum");
        objetoAUsar = null;
        UIController.instance.SetObject("ninguno", "ninguno");
    }

    public bool EnSigilo
    {
        get
        {
            return sigilo;
        }
    }


    private void Reiniciar()
    {
        UIController.instance.SetSigilo(sigilo);
        UIController.instance.SetObject("ninguno", "ninguno");
        multiplyVelocity = 1;
        transform.position = originalPosition;
        RemoveObject();
    }

    private void OnDestroy()
    {
        GameController.EnReinicio -= Reiniciar;
    }
}

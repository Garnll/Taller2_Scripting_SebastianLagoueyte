using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public static UIController instance = null;

    [SerializeField]
    private Image[] switchImagenes;
    [SerializeField]
    private Text hp;
    [SerializeField]
    private Text[] objects;
    [SerializeField]
    private Image objectI;
    [SerializeField]
    private Text winLose;
    [SerializeField]
    private Text door;
    [SerializeField]
    private Image sigilo;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void SetHP(int vida)
    {
        hp.text = string.Format("HP: {0}", vida);
    }

    public void SetSwitch(int switchAEncender, Color color)
    {
        switchImagenes[switchAEncender - 1].color = color;
    }

    public void SetObject(string objetoEnImagen, string objetoEnTexto, string objetoUsos = "")
    {
        objectI.GetComponent<ImageChooser>().GenerarImagen(objetoEnImagen);
        objects[0].text = string.Format("Usando {0}", objetoEnTexto);
        objects[1].text = objetoUsos;
    }

    public void SetWinLose(string texto)
    {
        winLose.text = texto;
    }

    public void SetDoor(bool ponerTexto, bool primeraVez = false)
    {
        if (!primeraVez)
        {
            if (ponerTexto)
            {
                door.text = "Door Opened";
            }
            else
            {
                door.text = "Wrong Order";
                StartCoroutine(ResetDoor());

            }
        }
        else
        {
            door.text = "";
        }
    }

    private IEnumerator ResetDoor()
    {
        yield return new WaitForSeconds(5);
        door.text = "";
    }

    public void SetSigilo(bool sigiloso)
    {
        sigilo.GetComponent<SigiloImageChooser>().AmIVisible(sigiloso);
    }
}

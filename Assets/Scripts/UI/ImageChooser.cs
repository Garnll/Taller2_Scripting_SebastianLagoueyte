using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageChooser : MonoBehaviour {

    [SerializeField]
    private Sprite ninguno;
    [SerializeField]
    private Sprite arma;
    [SerializeField]
    private Sprite ralentizador;

    private Dictionary<string, Sprite> imagen = new Dictionary<string, Sprite>();

    private void Start()
    {
        imagen.Add("ninguno", ninguno);
        imagen.Add("arma", arma);
        imagen.Add("ralentizador", ralentizador);
    }

    public void GenerarImagen(string llave)
    {
        this.GetComponent<Image>().sprite = imagen[llave];
    }
}

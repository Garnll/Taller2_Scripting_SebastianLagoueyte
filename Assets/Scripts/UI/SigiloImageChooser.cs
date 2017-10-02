using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SigiloImageChooser : MonoBehaviour {

    [SerializeField]
    private Sprite visible;
    [SerializeField]
    private Sprite invisible;

    public void AmIVisible(bool invisibility)
    {
        if (invisibility)
        {
            GetComponent<Image>().sprite = invisible;
        }
        else
        {
            GetComponent<Image>().sprite = visible;
        }
    }
}

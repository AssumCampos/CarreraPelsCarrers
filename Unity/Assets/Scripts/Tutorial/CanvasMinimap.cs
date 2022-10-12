using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMinimap : MonoBehaviour
{
    //public GameObject botonContinua;
    public GameObject canvasAnterior;

    public void continueButon()
    {
        canvasAnterior.SetActive(false);
    }
}

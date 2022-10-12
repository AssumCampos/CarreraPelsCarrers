using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasTecles : MonoBehaviour
{
    public GameObject canvasMinimap;

    public void desvisualizarEsteCanvas()
    {
        canvasMinimap.SetActive(false);
    }
}

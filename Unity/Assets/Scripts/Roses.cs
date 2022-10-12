using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
public class Roses : MonoBehaviour
{
    public ThirdPersonController tpc;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        var emission = tpc.speedlines.emission; // Se guarda el módulo en una variable para poder variarlo
        emission.enabled = false; 
        
        tpc.MoveSpeed = 3;                      // Se modifica la velocidad a la mitad de la normal
    }

    private void OnTriggerExit(Collider other)
    {
        var emission = tpc.speedlines.emission; // Se guarda el módulo en una variable para poder variarlo
        emission.enabled = true ; 
        
        // Al terminar, devuelve la velocidad correspondiente 
        if (tpc.isPowerUpOn)
        {
            tpc.MoveSpeed = 12;
        }
        else
        {
            tpc.MoveSpeed = 6;
        }
        
    }
}

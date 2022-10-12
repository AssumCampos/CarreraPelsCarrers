using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Bloqueamos el ratón
 */
public class UnlockMouse : MonoBehaviour
{
    // Confimanos el ratón para que no pueda salir de la pantalla
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }
}

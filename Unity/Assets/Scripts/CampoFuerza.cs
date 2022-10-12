using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CampoFuerza : MonoBehaviour
{
    public int num_energias;
    private int numVisitPoints, numVisitPointsInicio;
    private GameObject hijo;
    private string tamEscena;
    
    void Start(){
        numVisitPointsInicio = PlayerPrefs.GetInt("NumVisitPoints");
        tamEscena = PlayerPrefs.GetString("tamEscena");
        if (tamEscena == "Pequeña")
            num_energias = 2;
        else if (tamEscena == "Mediana")
            num_energias = 3;
        else if (tamEscena == "Grande")
            num_energias = 4;
        
    }
    void OnTriggerEnter(Collider other)
    {
        numVisitPoints = PlayerPrefs.GetInt("NumVisitPoints");
        
        if ((numVisitPoints - numVisitPointsInicio) == num_energias)
        {
            hijo = transform.GetChild(0).gameObject;
            hijo.GetComponent<Collider>().enabled = false;
            GetComponent<Collider>().enabled = false;
        }
    }
}


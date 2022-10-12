using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Inventario;
using UnityEngine.UI;
/**
 * Gestión del Item del inventario
 */
public class Item : MonoBehaviour
{
    public InventoryItemDescription description;
    public GameObject visitPointsUI;
    public string nombre;
    public GameObject spriteNave;
    private int num_energias;
    private float tiempoEliminacion = 4.5f;
    private int numVisitPoints = 0;
    private string tamEscena;
    private GameObject slider;
    private Slider slider_energia;
    

    public void Start(){
        tamEscena = PlayerPrefs.GetString("tamEscena");
        if (tamEscena == "Pequeña")
            num_energias = 2;
        else if (tamEscena == "Mediana")
            num_energias = 3;
        else if (tamEscena == "Grande")
            num_energias = 4;
        slider = GameObject.Find("/GUI/SliderEnergia");
        slider_energia = slider.GetComponent<Slider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Inventory.instance.AddItem(this);
        if (GetComponent<AudioSource>() != null)
            GetComponent<AudioSource>().PlayOneShot(description.sound);

        GetComponent<Collider>().enabled = false;
        if(description.type != Type.MONUMENTS)
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
        else 
        {
            Invoke("ShowPhoto", 1.3f);
            // Creamos el player pref de num visit points y añadimos
            if (PlayerPrefs.HasKey("NumVisitPoints"))
                numVisitPoints = PlayerPrefs.GetInt("NumVisitPoints");
            PlayerPrefs.SetInt("NumVisitPoints", numVisitPoints + 1);

            // Aumentamos la barra de las energías
            slider_energia.maxValue = num_energias;
            slider_energia.minValue = 0;
            slider_energia.value = PlayerPrefs.GetInt("NumVisitPoints");
            Invoke("DeleteImage", tiempoEliminacion-0.1f);

            numVisitPoints = PlayerPrefs.GetInt("NumVisitPoints");
        
            if (numVisitPoints == num_energias)
            {
                spriteNave.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        Destroy(transform.gameObject, tiempoEliminacion);
    }

    public void ShowPhoto()
    {
        visitPointsUI.SetActive(true);
        visitPointsUI.GetComponent<Image>().sprite = Resources.Load<Sprite>(nombre);
    }
    
    public void DeleteImage()
    {
        visitPointsUI.SetActive(false);
    }
}
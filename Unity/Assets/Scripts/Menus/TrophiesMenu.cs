using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/**
 * Gestiona los trofeos del juego
 */
public class TrophiesMenu : MonoBehaviour
{
    private int totalTrofeos = 6, trofeosConseguidos, totalBasuras = 100, numBasuras;
    private string[] listaTrofeos;
    public Slider slider;
    public TextMeshProUGUI porcentajeTotal;
    public Image porcentajeBasuras;
    public GameObject[] estrellasTrofeos;
    private void Awake()
    {
        trofeosConseguidos = 0;
        string[] arrayTrofeos = new[] {"TrofeoNinot", "TrofeoTaronja", "TrofeoPaella","TrofeoOrxata","TrofeoLluna"};

        //Comprueba cuántas basuras del Trofeo Verd tiene
        if (PlayerPrefs.HasKey("TrofeoVerd") && PlayerPrefs.GetInt("TrofeoVerd") != 0)
        {
            numBasuras = PlayerPrefs.GetInt("TrofeoVerd");
            estrellasTrofeos[5].SetActive(true);
            if (numBasuras >= totalBasuras)
            {
                var colorPanel = GameObject.Find("PanelTrofeoVerd").GetComponent<Image>();
                colorPanel.color = new Color(244/255f, 208/255f, 116/255f);
                porcentajeBasuras.color = new Color(29/255f, 147/255f, 33/255f);
                trofeosConseguidos++;
            }
        }
        else
        {
            PlayerPrefs.SetInt("TrofeoVerd", 0);
            numBasuras = 0;
            estrellasTrofeos[5].SetActive(false);
            PlayerPrefs.Save();
        }
        porcentajeBasuras.fillAmount = (float)numBasuras / (float)totalBasuras;

        
        //Comprueba si ha conseguido algún trofeo, cambia el color del panel y muestra la estrella
        for (int i = 0; i < arrayTrofeos.Length; i++)
        {
            if (PlayerPrefs.GetInt(arrayTrofeos[i]) == 1)
            {
                estrellasTrofeos[i].SetActive(true);
                var colorPanel = GameObject.Find("Panel"+ arrayTrofeos[i]).GetComponent<Image>();
                colorPanel.color = new Color(244/255f, 208/255f, 116/255f);
                
                trofeosConseguidos++;
            }
        }
  
        //Comprueba cuántos trofeos ha conseguido y actualiza el slider
        slider.maxValue = totalTrofeos;
        slider.minValue = 0;
        slider.value = trofeosConseguidos;
        
        if (slider.value / slider.maxValue != 1.0)
            porcentajeTotal.text = (slider.value / slider.maxValue * 100).ToString("F0") + "%";
        else
            porcentajeTotal.text = "¡Completado!";
        
    }

    public void AddTrofeo(string nombreTrofeo, int basuras)
    {
        if (nombreTrofeo == "TrofeoVerd")
        {
            numBasuras = PlayerPrefs.GetInt("TrofeoVerd") + basuras;
            PlayerPrefs.SetInt(nombreTrofeo, numBasuras);
            PlayerPrefs.Save();
            if (PlayerPrefs.GetInt("TrofeoVerd") >= 100){}
                trofeosConseguidos++;
        }
        else
        {
            trofeosConseguidos++;
            if (nombreTrofeo != null) PlayerPrefs.SetInt(nombreTrofeo, 1);
            PlayerPrefs.Save();
        }
    }
}

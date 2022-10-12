using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//https://www.youtube.com/watch?v=zc8ac_qUXQY

public class MainMenu : MonoBehaviour
{
    public static string _playersName;
    public TextMeshProUGUI mapSizeText;
    public Button jugarButton, left, right;
    public HighscoreTable _highscoreTable;

    private string[] _mapSize = new string[] {"VALÈNCIA XICOTETA", "VALÈNCIA MITJANA","VALÈNCIA GRAN"};
    private int _mapSizeIndex;
    private AudioSource _allowed;
    private void Start()
    {       
        ShowCursor();
        
        var music = GameObject.FindGameObjectWithTag("music").GetComponent<AudioSource>();
        var pitchBendGroup = music.outputAudioMixerGroup.audioMixer;
        pitchBendGroup.SetFloat("pitchBend", 1f);
        
        _allowed = GameObject.FindGameObjectWithTag("click").GetComponent<AudioSource>();
        _mapSizeIndex = 1;
        mapSizeText.text = _mapSize[_mapSizeIndex];

        //t = GameObject.FindGameObjectWithTag("timerTag").GetComponent<timer>();
        jugarButton.interactable = false;
    }

    
    //Recoge el nombre del jugador
    public void ReadStringInput(string s)
    {
        _playersName = s;
        if (_playersName != "")
        {
            jugarButton.interactable = true;
            PlayerPrefs.SetString("playersName", _playersName);         //Guardamos el nombre del jugador
        }
        else
        {
            jugarButton.interactable = false;
        }
    }
    
    //Empieza la partida
    public void StartGame()
    {
        HideCursor();
        // Reseteamos los player prefs
        if (PlayerPrefs.HasKey("NumVisitPoints"))
        {
            PlayerPrefs.SetInt("NumVisitPoints", 0);
        }
        if (_playersName != null)                           //Si el jugador a puesto su nombre
        {
            if(_mapSizeIndex == 0)                          //Guardamos el tamaño del mapa y cargamos esa escena
            {
                PlayerPrefs.SetString("tamEscena", "Pequeña"); 
                SceneManager.LoadScene("EscenaPequeña");      
            }
            else if (_mapSizeIndex == 1)
            {
                PlayerPrefs.SetString("tamEscena", "Mediana"); 
                SceneManager.LoadScene("EscenaMediana");
            }
            else
            {
                PlayerPrefs.SetString("tamEscena", "Grande");
                SceneManager.LoadScene("EscenaGrande");
            }
        }        
    }
    
    //Cierra la aplicación
    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetMapSizeL()
    {
        if(_mapSizeIndex > 0)
        {
            _allowed.Play();
            _mapSizeIndex--;
            
            if (_mapSizeIndex == 0)
                left.interactable = false;
            else
                right.interactable = true;
        }
        mapSizeText.text = _mapSize[_mapSizeIndex];
    }
    
    public void SetMapSizeR()
    {
        if (_mapSizeIndex < _mapSize.Length - 1)
        {
            _allowed.Play();
            _mapSizeIndex++;
            
            if (_mapSizeIndex == _mapSize.Length-1)
                right.interactable = false;
            else
                left.interactable = true;
        }
        mapSizeText.text = _mapSize[_mapSizeIndex];
    }

    public void LoadTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;     // Permite al cursor moverse libremente
        Cursor.visible = true;                      // Hace el cursor visible
    }

    public void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;   // Fija el cursor al centro de la pantalla
        Cursor.visible = false;                     // Hace el cursor invisible
    }
}

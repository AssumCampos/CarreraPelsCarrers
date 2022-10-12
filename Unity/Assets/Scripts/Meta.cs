using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Playables;

public class Meta : HighscoreTable
{
    public AudioClip aplause;
    public Temporizador timer;
    public GameObject highscore;
    private AudioSource audio;
    public PlayableDirector timeline;
    public GameObject timelineGO;
    public ShowHighscoreTable _showHighscoreTable;
    public AudioSource song;
    public GameObject pauseMenu;
    public GameObject teclaContinuar;
    public Boolean finalJuego = false;

    private void OnTriggerEnter(Collider other)
    {
        finalJuego = true;
        audio = GetComponent<AudioSource>();
        timer.timeStart = false;
        // Añadimos los aplausos del final
        audio.PlayOneShot(aplause, 11.9f);
        Destroy(transform.gameObject, 12.0f);

        // Añadimos el highscore        
        Invoke("LateHighScore", 8.0f);
        song.Pause();
        timelineGO.SetActive(true);
        
        // Presionar cualquier tecla para continuar
        teclaContinuar.SetActive(true);

        // Empieza la animacion del final
        timeline.Play();
        
        //Deshabilitamos el Collider
        transform.GetComponent<Collider>().enabled = false;
    }
    void LateHighScore(){
        highscore.SetActive(true);
        AwakeHighscore();
        _showHighscoreTable.AwakeShowHighscore();
    }
    
}

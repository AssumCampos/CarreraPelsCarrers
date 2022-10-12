using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//https://www.youtube.com/watch?v=FnQ8Bpyqhnk Select Player
//https://www.youtube.com/watch?v=iAbaqGYdnyI Highscore
public class HighscoreTable : MonoBehaviour
{
    public Canvas canvasHighscore;
    public TrophiesMenu trofeos;
    public ThirdPersonController controladorTerceraPersona;
    private string tamEscena;
    public TextAsset txtAsset;
    //private List<HighscoreEntry> highscoreEntryList;
    
    public void AwakeHighscore()
    {
        Temporizador temporizador = GameObject.Find("Time").GetComponent<Temporizador>();

        //Item visitPoints = GameObject.Find("VisitPointsUI").GetComponent<Item>();
        
        //Recuperamos los datos del jugador y la escena en la jugó
        string userName = PlayerPrefs.GetString("playersName");       
        var userSegundos = temporizador.GetSegundos();
        var userMinutos = temporizador.GetMinutos();
        tamEscena = PlayerPrefs.GetString("tamEscena");
        
        //Actualizamos los trofeos de tiempo y de basura
        if(userMinutos * 60 + userSegundos >= 3*60 && !PlayerPrefs.HasKey("TrofeoOrxata"))
            trofeos.AddTrofeo("TrofeoOrxata", 0);
        if (userMinutos * 60 + userSegundos >= 6*60 && !PlayerPrefs.HasKey("TrofeoLluna"))
            trofeos.AddTrofeo("TrofeoLluna", 0);
        if (PlayerPrefs.GetInt("NumVisitPoints") >= 3 && !PlayerPrefs.HasKey("TrofeoTaronja"))
            trofeos.AddTrofeo("TrofeoTaronja", 0);
        if(PlayerPrefs.GetInt("NumVisitPoints") >= 7 && !PlayerPrefs.HasKey("TrofeoPaella"))
            trofeos.AddTrofeo("TrofeoPaella", 0);
        trofeos.AddTrofeo("TrofeoVerd", controladorTerceraPersona.GetTotalGarbage());

        PlayerPrefs.SetInt("NumVisitPoints", 0);

        //Si no hay resultados en el marcador, se guardan los 10 por defecto
        if (PlayerPrefs.GetInt("highscore" + tamEscena + 1) == 0 || !PlayerPrefs.HasKey("highscore" + tamEscena + 1))
        {
            SetDefaultHighscore(tamEscena);
        }

        //Hacemos una lista con el jugador actual y los mejores que están guardados en PlayerPrefs
        List<HighscoreEntry> highscoreEntryList = new List<HighscoreEntry>()
        {
            new HighscoreEntry() {time = userMinutos * 60 + userSegundos, name = userName},
        };
        for(int i = 1; PlayerPrefs.GetInt("highscore"+tamEscena+i ) != 0 && i <= 11; i++)
        {
            highscoreEntryList.Add(new HighscoreEntry(){time = PlayerPrefs.GetInt("highscore"+tamEscena+i), name = PlayerPrefs.GetString("highscoreName"+tamEscena+i)});
        }
        
        //Ordenamos la lista al cargar, si un jugador tiene menor tiempo que otro lo intercambiamos
        for (int i = 0; i < highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscoreEntryList.Count; j++)
            {
                if (highscoreEntryList[j].time < highscoreEntryList[i].time)
                {
                    HighscoreEntry temporal = highscoreEntryList[i];
                    highscoreEntryList[i] = highscoreEntryList[j];
                    highscoreEntryList[j] = temporal;
                }
            }
        }

        //Actualizamos el trofeo del marcador y creamos las entradas
        for (int i = 0; i < highscoreEntryList.Count && i <= 9; i++)
        {
            if(highscoreEntryList[i].name == userName && !PlayerPrefs.HasKey("TrofeoNinot"))
                trofeos.AddTrofeo("TrofeoNinot", 0);
            SaveNewHighscore(highscoreEntryList[i], i);
        }
    }
    
    //Crea una entrada para el marcador y guarda las nuevas posiciones en PlayerPrefs
    private void SaveNewHighscore(HighscoreEntry highscoreEntry, int i)
    {
        
        int rank = i + 1;           

        int time = highscoreEntry.time;
        string name = highscoreEntry.name;
        
        PlayerPrefs.SetInt("highscore"+tamEscena+rank, time);
        
        PlayerPrefs.SetString("highscoreName"+tamEscena+rank, name);
        PlayerPrefs.Save();
        
    }

    //Se guardan 10 resultados iniciales dependiendo del tamaño de la escena
    public void SetDefaultHighscore(string tamanyo)
    {
        string conjuntoResultados = txtAsset.ToString();
        string[] marcadorInicial = conjuntoResultados.Split();
        
        if (tamanyo == "Pequeña")
        {
            for (int i = 1; i <= 10; i++)
            {
                PlayerPrefs.SetString("highscoreName"+tamanyo+i, marcadorInicial[2+(i-1)*4]);
                PlayerPrefs.SetInt("highscore"+tamanyo+i, int.Parse(marcadorInicial[4*i]));
            }
        }
        else if (tamanyo == "Mediana")
        {
            int j = 1;
            for (int i = 1; i <= 10; i++)
            {
                PlayerPrefs.SetString("highscoreName"+tamanyo+i, marcadorInicial[42+(i-1)*4]);
                PlayerPrefs.SetInt("highscore"+tamanyo+i, int.Parse(marcadorInicial[44+(i-1)*4]));
                j++;
            }
        }
        else if (tamanyo == "Grande")
        {
            int j = 1;
            for (int i = 1; i <= 10; i++)
            {
                PlayerPrefs.SetString("highscoreName"+tamanyo+i, marcadorInicial[82+(i-1)*4]);
                PlayerPrefs.SetInt("highscore"+tamanyo+i, int.Parse(marcadorInicial[84+(i-1)*4]));
                j++;
            }
        }
        PlayerPrefs.Save();
    }
    public class HighscoreEntry
    {
        public int time;
        public string name;
    }
}



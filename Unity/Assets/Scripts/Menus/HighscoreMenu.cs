using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//https://www.youtube.com/watch?v=FnQ8Bpyqhnk Select Player
//https://www.youtube.com/watch?v=iAbaqGYdnyI Highscore
public class HighscoreMenu : HighscoreTable
{
    public Button left, right;
    public Text tamEscenaText;
    public GameObject entryContainer, entryTemplate;
    
    private string [] tamanyoEscena = new []{"Pequeña", "Mediana", "Grande"};
    private int tamEscenaIndex = 0;
    private string numEscena;
    private AudioSource _allowed;
    
    //private List<HighscoreEntry> highscoreEntryList;
    
    public void AwakeShowHighscore()
    {
        numEscena = tamanyoEscena[tamEscenaIndex];
        //GameObject entryContainer = GameObject.Find("highscoreEntryContainer");
        //GameObject entryTemplate = GameObject.Find("highscoreEntryTemplate");
        List<Transform> highscoreEntryTransformList;
        if (tamEscenaIndex == 0)
        {
            tamEscenaText.text = "València Xicoteta";
        }
        else if (tamEscenaIndex == 1)
        {
            tamEscenaText.text = "València Mitjana";
        }
        else
        {
            tamEscenaText.text = "València Gran";
        }
        

        if (tamEscenaIndex == 0)
            left.interactable = false;

        _allowed = GameObject.FindGameObjectWithTag("click").GetComponent<AudioSource>();
        
        //Si no hay resultados en el marcador, se guardan los 10 por defecto
        if (PlayerPrefs.GetInt("highscore" + numEscena + 1) == 0 || !PlayerPrefs.HasKey("highscore" + numEscena + 1))
        {
            SetDefaultHighscore(numEscena);
        }
        
        //Hacemos una lista con el jugador actual y los mejores que están guardados en PlayerPrefs
        List<HighscoreEntry> highscoreEntryList = new List<HighscoreEntry>()
        {
            //
        };
        for(int i = 1; PlayerPrefs.GetInt("highscore"+numEscena+i ) != 0 &&  i <= 11; i++)
        {
            highscoreEntryList.Add(new HighscoreEntry(){time = PlayerPrefs.GetInt("highscore"+numEscena+i), name = PlayerPrefs.GetString("highscoreName"+numEscena+i)});
        }

        highscoreEntryTransformList = new List<Transform>();
        //Actualizamos el trofeo del marcador y creamos las entradas
        for (int i = 0; i < highscoreEntryList.Count && i <= 9; i++)
        {
            CreateHighscoreEntryTransform(highscoreEntryList[i], highscoreEntryTransformList, entryTemplate, entryContainer.GetComponent<Transform>(), highscoreEntryTransformList);
        }
    }
    
    //Crea una entrada para el marcador y guarda las nuevas posiciones en PlayerPrefs
    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, List<Transform> highscoreEntryTransformList,GameObject entryTemplate, Transform container, List<Transform> list)
    {
        float height = 50f;
        GameObject entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -height * highscoreEntryTransformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = highscoreEntryTransformList.Count + 1;
        string rankString = rank.ToString();

        entryTransform.GetComponent<Transform>().Find("posText").GetComponent<Text>().text = rankString;

        int time = highscoreEntry.time;
        string name = highscoreEntry.name;
        
        entryTransform.GetComponent<Transform>().Find("timeText").GetComponent<Text>().text = (time/60).ToString("00") + ":" + (time%60).ToString("00");

        entryTransform.GetComponent<Transform>().Find("nameText").GetComponent<Text>().text = name;
        entryTransform.tag = "clones";
        
        list.Add(entryTransform.GetComponent<Transform>());
    }

    public class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }

    [System.Serializable] //necesario para poder hacerlo json
    public class HighscoreEntry
    {
        public int time;
        public string name;
    }
    public void SetRightMapHighscore()
    {
        if (tamEscenaIndex < tamanyoEscena.Length - 1)
        {
            //_allowed.Play();
            tamEscenaIndex++;
            
            if (tamEscenaIndex == tamanyoEscena.Length-1)
                right.interactable = false;
            else
                left.interactable = true;
        }

        DeleteActualHighscore();
        AwakeShowHighscore();
    }
    public void SetLeftMapHighscore()
    {
        if(tamEscenaIndex > 0)
        {
            //_allowed.Play();
            tamEscenaIndex--;
            
            if (tamEscenaIndex == 0)
                left.interactable = false;
            else
                right.interactable = true;
        }
        tamEscenaText.text = "València "+numEscena;

        DeleteActualHighscore();
        AwakeShowHighscore();
    }

    public void DeleteActualHighscore()
    {
        GameObject[] clones;

        clones = GameObject.FindGameObjectsWithTag("clones");

        GameObject clone = GameObject.Find("highscoreEntryTemplate(Clone)");
        foreach (var o in clones) Destroy(o);
    }

}

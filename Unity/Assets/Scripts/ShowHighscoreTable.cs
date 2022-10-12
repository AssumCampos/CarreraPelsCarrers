using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//https://www.youtube.com/watch?v=FnQ8Bpyqhnk Select Player
//https://www.youtube.com/watch?v=iAbaqGYdnyI Highscore
public class ShowHighscoreTable : MonoBehaviour
{
    public Canvas canvasHighscore;
    private string tamEscena;
    //private List<HighscoreEntry> highscoreEntryList;
    
    public void AwakeShowHighscore()
    {
        GameObject entryContainer = GameObject.Find("highscoreEntryContainer");
        GameObject entryTemplate = GameObject.Find("highscoreEntryTemplate");
        List<Transform> highscoreEntryTransformList;
        //Item visitPoints = GameObject.Find("VisitPointsUI").GetComponent<Item>();

        //Recuperamos los datos de la escena en la jugó
        tamEscena = PlayerPrefs.GetString("tamEscena"); 
        
        //Hacemos una lista con el jugador actual y los mejores que están guardados en PlayerPrefs
        List<HighscoreEntry> highscoreEntryList = new List<HighscoreEntry>()
        {
            //new HighscoreEntry() {time = userMinutos * 60 + userSegundos, name = userName},
        };
        for(int i = 1; PlayerPrefs.GetInt("highscore"+tamEscena+i ) != 0 &&  i <= 11; i++)
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

        highscoreEntryTransformList = new List<Transform>();
        //Actualizamos el trofeo del marcador y creamos las entradas
        for (int i = 0; i < highscoreEntryList.Count && i <= 9; i++)
        {
            CreateHighscoreEntryTransform(highscoreEntryList[i], highscoreEntryTransformList, entryTemplate, entryContainer.GetComponent<Transform>(), highscoreEntryTransformList);
        }
    }

    //METER TAM
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
        //PlayerPrefs.SetInt("highscore"+tamEscena+rank, time);

        entryTransform.GetComponent<Transform>().Find("nameText").GetComponent<Text>().text = name;
        /*PlayerPrefs.SetString("highscoreName"+tamEscena+rank, name);
        PlayerPrefs.Save();*/

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
    //highscorer.gameObject.SetActive(true);*/
}




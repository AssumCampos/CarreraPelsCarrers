using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using StarterAssets;
using UnityEngine.SceneManagement;

public class Temporizador : MonoBehaviour
{
    public AudioSource main;
    public AudioClip beep, start;
    public Text segundos, minutos, cuentaAtrasTexto;
    public GameObject fondoTimer;
    public float tiempoSeg = 00, tiempoMin = 00;
    public int cuentaAtrasTiempo = 3, actualButton = 1;
    public ThirdPersonController cc;
    public bool timeStart = false;
    public Meta meta;
    private GameObject dospuntos, pauseButton, continueButton, restartButton, quitButton, controlsButton, pauseMenu, controlsMenu;
    private Button[] arrayButton;
    
    //Hace una cuenta regresiva de 3 a 1 mostrando los números en pantalla, en el 0 muestra ¡Ya! y luego borra el texto
    IEnumerator CuentaAtras()
    {
        AudioSource audio = GetComponent<AudioSource>();
        while (cuentaAtrasTiempo > 0)
        {
            cuentaAtrasTexto.text = cuentaAtrasTiempo.ToString();
            audio.PlayOneShot(beep, 0.2f/cuentaAtrasTiempo);
            yield return new WaitForSeconds(1f); 

            cuentaAtrasTiempo--;
        }

        audio.PlayOneShot(start, 0.3f);
        cuentaAtrasTexto.text = "Ja!";
        cc.enabled = true;                                  //Permite al jugador moverse

        yield return new WaitForSeconds(1f);
        StartTimer();
        fondoTimer.SetActive(true);
        segundos.gameObject.SetActive(true);
        minutos.gameObject.SetActive(true);
        cuentaAtrasTexto.gameObject.SetActive(false);
        dospuntos.gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        var music = GameObject.FindGameObjectWithTag("music").GetComponent<AudioSource>();
        var pitchBendGroup = music.outputAudioMixerGroup.audioMixer;
        pitchBendGroup.SetFloat("pitchBend", 1f);
        
        cc.enabled = false;                                 //No permite al jugador moverse
        fondoTimer.SetActive(false);
        segundos.gameObject.SetActive(false);
        minutos.gameObject.SetActive(false);
        dospuntos = GameObject.Find(":");
        dospuntos.gameObject.SetActive(false);
        
        //Inicializamos los botones
        continueButton = GameObject.Find("ContinueButton");
        pauseButton = GameObject.Find("PauseButton");
        restartButton = GameObject.Find("RestartButton");
        controlsButton = GameObject.Find("ControlsButton");
        quitButton = GameObject.Find("QuitButton");
        arrayButton = new Button[]
        {
            continueButton.GetComponent<Button>(), 
            restartButton.GetComponent<Button>(),
            controlsButton.GetComponent<Button>(),
            quitButton.GetComponent<Button>()
        };
        
        pauseMenu = GameObject.Find("PauseMenu");
        pauseMenu.SetActive(false);
        controlsMenu = GameObject.Find("ControlsMenu");

        StartCoroutine(CuentaAtras());
        segundos.text = "" + tiempoSeg;
        minutos.text = "" + tiempoMin;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeStart)
        {
            tiempoSeg += Time.deltaTime;
            if (tiempoSeg > 59)
            {
                tiempoSeg = 00;
                tiempoMin++;
                minutos.text = tiempoMin.ToString("00");
            }

            if (tiempoMin == 00)
            {
                minutos.text = "00";
            }

            segundos.text = tiempoSeg.ToString("00");
        }

        if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) && !meta.finalJuego)
        {
            if (Time.timeScale == 0f && pauseMenu.activeSelf)
                ResumeGame();
            else
                PauseGame();
        }
        
        if (Input.GetKeyDown(KeyCode.DownArrow) && Time.timeScale == 0f && pauseMenu.activeSelf)
        {
            actualButton++;
            if (actualButton == arrayButton.Length)
                actualButton = 0;
            arrayButton[actualButton].Select();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && Time.timeScale == 0f && pauseMenu.activeSelf)
        {
            actualButton--;
            if (actualButton == -1)
                actualButton = arrayButton.Length - 1;
            arrayButton[actualButton].Select();
        }
        if (Input.GetKeyDown(KeyCode.Space) && Time.timeScale == 0f && pauseMenu.activeSelf)
        {
            switch (actualButton)
            {
                case 0:
                    ResumeGame();
                    break;
                case 1:
                    RestartGame();
                    break;
                case 2:
                    pauseMenu.SetActive(false);
                    //Mostramos los controles
                    break;
                case 3:
                    BackToMainMenu();
                    break;
            }

        }
    }

    public void RestartGame()
    {
        if (PlayerPrefs.HasKey("NumVisitPoints")){
            PlayerPrefs.SetInt("NumVisitPoints", 0);
        }

        HideCursor();
        
        cuentaAtrasTiempo = 3;
        tiempoSeg = 00;
        tiempoMin = 00;
        timeStart = false;
        ResumeGame();
        string scene = PlayerPrefs.GetString("tamEscena");
        scene = "Escena" + scene;    
        SceneManager.LoadScene(scene);
    }
    //Activa a true la variable booleana que actúa como bandera y empieza el cronómetro
    public void StartTimer()
    {
        main.PlayDelayed(1.0f);
        timeStart = true;
    }
    
    //Pausa el juego, se paran el tiempo y la música. Se muestra el menú de pausa
    public void PauseGame()
    {
        ShowCursor();
        
        if (Time.timeScale != 0f)
        {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
        }
    }
    
    //Reanuda el juego, continúan el tiempo y la música. Deja de mostrarse el menú de pausa
    public void ResumeGame()
    {
        HideCursor();
        
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    public void BackToMainMenu()
    {
        cuentaAtrasTiempo = 3;
        tiempoSeg = 00;
        tiempoMin = 00;
        timeStart = false;
        ResumeGame();
        SceneManager.LoadScene("MenuPrincipal");
    }

    //Devuelve los minutos que ha tardado el jugador
    public int GetSegundos()
    {
        return (int) tiempoSeg;
    }
    //Devuelve los minutos que ha tardado el jugador
    public int GetMinutos()
    {
        return (int) tiempoMin;
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



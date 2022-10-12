using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeEnd : MonoBehaviour
{
    private bool wait = true;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0.6f;
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("WaitEnd", 1);
        if (Input.anyKey && !wait)
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene("MenuPrincipal");
        }
    }

    private void WaitEnd()
    {
        wait = false;
    }
    
}

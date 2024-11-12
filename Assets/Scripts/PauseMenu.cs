using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool Paused = false;
    public GameObject PauseMenuCanvas;
    void Start()
    {
        Time.timeScale = 1.0f;
        PauseMenuCanvas.SetActive(false);
        Paused = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        { 
        
            if (Paused)
            {
                Play();
            }
            else
            {
                Stop();
            }
        }
    }
    public void Stop()
    {
        PauseMenuCanvas.SetActive(true);
        Time.timeScale = 0f;
        Paused = true;
    }
    public void Play()
    {
        PauseMenuCanvas.SetActive(false);
        Time.timeScale = 1.0f;
        Paused = false;
    }
    public void MainMenuButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
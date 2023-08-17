using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;

    private static bool gameIsPaused = false;

    private void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        // Pressing Esc will pause and unpause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Paused();
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false); // Set the panel active to true
        settingsMenu.SetActive(false);
        Time.timeScale = 1f; // unfreeze the gameplay
        gameIsPaused = false; // change the bool to true
        Debug.Log("Scene Resumed");
    }

    public void Settings()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void Paused()
    {
        pauseMenu.SetActive(true); // Set the panel active to true
        Time.timeScale = 0f; // freeze the gameplay
        gameIsPaused = true; // change the bool to true
        Debug.Log("Scene Paused");
    }
}

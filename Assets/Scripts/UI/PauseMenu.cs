using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    private static bool gameIsPaused = false;

    // Update is called once per frame
    void Update()
    {
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
        Time.timeScale = 1f; // freeze the gameplay
        gameIsPaused = false; // change the bool to true
        Debug.Log("Scene Resumed");
    }

    private void Paused()
    {
        pauseMenu.SetActive(true); // Set the panel active to true
        Time.timeScale = 0f; // freeze the gameplay
        gameIsPaused = true; // change the bool to true
        Debug.Log("Scene Paused");
    }
}

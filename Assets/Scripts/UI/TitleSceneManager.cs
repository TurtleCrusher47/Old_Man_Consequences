using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    [Header("Menu Elements")]
    [SerializeField] private GameObject settingsMenu;
    //[SerializeField] private GameObject keybindsMenu;

    private bool isSettingClicked = false;

    // Update is called once per frame
    void Update()
    {
        // Pressing Esc will pause and unpause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isSettingClicked)
            {
                Resume();
            }
            else
            {
                Settings();
            }
        }
    }

    public void Resume()
    {
        settingsMenu.SetActive(false); // Set the panel active to true
        //keybindsMenu.SetActive(false);
        isSettingClicked = false; // change the bool to true
        Debug.Log("Scene Resumed");
    }

    public void Credits(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Settings()
    {
        settingsMenu.SetActive(true); // Set the panel active to true
        isSettingClicked = true; // change the bool to true
        Debug.Log("Scene Settings");
    }
}

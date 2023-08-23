using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneManager : MonoBehaviour
{
    [Header("Menu Elements")]
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject keybindsMenu;

    private bool isSettingClicked = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

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
        keybindsMenu.SetActive(false);
        isSettingClicked = false; // change the bool to true
        Debug.Log("Scene Resumed");
    }

    public void Settings()
    {
        settingsMenu.SetActive(true); // Set the panel active to true
        isSettingClicked = true; // change the bool to true
        Debug.Log("Scene Settings");
    }

    public void Keybinds()
    {
        settingsMenu.SetActive(false);
        keybindsMenu.SetActive(true);
        Debug.Log("Scene Keybinds");
    }
}

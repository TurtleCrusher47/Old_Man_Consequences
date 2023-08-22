using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Header("Add UI Scene")]
    [SerializeField] private string uiSceneName;

    private void Start()
    {
        SceneManager.LoadScene(uiSceneName, LoadSceneMode.Additive);
    }
    public void NewGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Credits(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitScene()
    {
        Application.Quit();
    }
}

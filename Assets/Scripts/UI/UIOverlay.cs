using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIOverlay : MonoBehaviour
{
    [SerializeField] private string sceneName = "UIScene";

    private static UIOverlay instance;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadUI();   
    }

    private void ToggleUI()
    {
        if (SceneManager.GetSceneByName(sceneName).isLoaded)
        {
            UnloadUI();
        }
        else
        {
            LoadUI();
        }
    }

    private void LoadUI()
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    private void UnloadUI()
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }
}

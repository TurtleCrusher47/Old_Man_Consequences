using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGameManager : MonoBehaviour
{
    [Header("Add UI Scene")]
    [SerializeField] private string uiSceneName = "UIScene";

    // Start is called before the first frame update
    void Start()
    {
        // Load the UI scene additively
        SceneManager.LoadScene(uiSceneName, LoadSceneMode.Additive);
    }
}

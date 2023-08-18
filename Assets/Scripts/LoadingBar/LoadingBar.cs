using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingBar : MonoBehaviour
{
    [Header("Loading Bar Element")]
    [SerializeField] Slider slider;
    [SerializeField] TMP_Text progressText;
    [Header("Main Scene")]
    [SerializeField] string sceneName;
    [Header("Add UI Scene")]
    [SerializeField] private string uiSceneName;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadLevelAsync(sceneName));
    }

    IEnumerator LoadLevelAsync(string sceneName)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        SceneManager.LoadScene(uiSceneName, LoadSceneMode.Additive);

        while (op.isDone == false)
        {
            float progress = Mathf.Clamp01(op.progress / 0.9f); // Adjusted to give a 0 to 1 range
            slider.value = progress;
            progressText.text = $"{progress * 100}%"; // Update the progress text

            yield return null;
        }
    }
}

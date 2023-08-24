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
    [SerializeField] TMP_Text loadingText;
    [Header("Main Scene")]
    [SerializeField] string sceneName;
    [Header("Add UI Scene")]
    [SerializeField] private string uiSceneName;

    private const float dotInterval = 0.5f; // Interval between dot increment
    private int dotCount = 1; // Current dot count

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadLevelAsync(sceneName));
        StartCoroutine(AnimateDots());
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

    IEnumerator AnimateDots()
    {
        while (true)
        {
            loadingText.text = "Loading Game " + new string('.', dotCount);
            dotCount = (dotCount + 1) % 4; // Cycle through 0,1,2,3

            yield return new WaitForSeconds(dotInterval);
        }
    }
}

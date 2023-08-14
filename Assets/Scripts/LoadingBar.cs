using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] TMP_Text progressText; 

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadLevelAsync(sceneName));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LoadLevelAsync(string sceneName)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);

        while (op.isDone == false)
        {
            progressText.fillAmount = op.progress;
            yield return new WaitForEndOfFrame();
        }
    }
}

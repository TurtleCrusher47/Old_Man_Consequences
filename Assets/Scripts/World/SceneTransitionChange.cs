using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionChange : MonoBehaviour
{   
    public Animator transition;

    public float transitionTimer;
    
    public void LoadScene(string nextScene)
    {
        StartCoroutine(LoadNextScene(nextScene));
    }

    IEnumerator LoadNextScene(string sceneName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTimer);

        SceneManager.LoadScene(sceneName);
    }
}

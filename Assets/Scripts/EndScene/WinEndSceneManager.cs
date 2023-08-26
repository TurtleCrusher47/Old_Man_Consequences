using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinEndSceneManager : MonoBehaviour
{
    public GameObject noticePanel;
    public GameObject endPanel;
    public GameObject blackoutPanel;
    public GameObject beach;


    public Animator boatAnimator;
    public Animator noticeAnimator;
    public Animator blackoutAnimator;

    public AudioSource sfxManager;
    public AudioClip[] clips;

    void Start()
    {
        StartCoroutine(WinEnding());
    }

    private IEnumerator WinEnding()
    {
        yield return new WaitForSeconds(4f);

        sfxManager.clip = clips[0];

        sfxManager.Play();

        noticePanel.SetActive(true);

        yield return new WaitForSeconds(8f);

        sfxManager.clip = clips[0];

        sfxManager.Play();

        noticeAnimator.SetBool("Exit", true);

        yield return new WaitForSeconds(4f);

        sfxManager.clip = clips[1];

        sfxManager.Play();

        boatAnimator.SetBool("Exit", true);

        yield return new WaitForSeconds(3f);

        blackoutPanel.SetActive(true);

        yield return new WaitForSeconds(1f);

        sfxManager.clip = clips[2];

        sfxManager.Play();

        beach.SetActive(true);

        yield return new WaitForSeconds(4f);

        endPanel.SetActive(true);

    }
}

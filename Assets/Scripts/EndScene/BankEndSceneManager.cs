using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankEndSceneManager : MonoBehaviour
{
    public GameObject noticePanel;
    public GameObject endPanel;


    public Animator boatAnimator;
    public Animator noticeAnimator;

    public AudioSource sfxManager;

    public AudioClip[] clips;


    void Start()
    {
        StartCoroutine(BankEnding());
    }

    private IEnumerator BankEnding()
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

        boatAnimator.SetBool("Exit", true);

        sfxManager.clip = clips[1];

        sfxManager.Play();

        yield return new WaitForSeconds(3f);

        endPanel.SetActive(true);
    }
}

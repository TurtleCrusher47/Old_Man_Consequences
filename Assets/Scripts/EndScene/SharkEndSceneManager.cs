using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SharkEndSceneManager : MonoBehaviour
{
    public GameObject sharkPanel;
    public GameObject sharkFin;
    public GameObject endPanel;

    public Animator boatAnimator;
    public Animator sharkAnimator;
    
    public AudioSource sfxManager;

    public AudioClip[] clips;

    void Start()
    {
        StartCoroutine(SharkEnding());
    }
    public IEnumerator SharkEnding()
    {
        yield return new WaitForSeconds(4f);

        sfxManager.clip = clips[0];

        sfxManager.Play();

        sharkPanel.SetActive(true);

        yield return new WaitForSeconds(6f);

        sfxManager.Play();

        sharkAnimator.SetBool("Exit", true);

        yield return new WaitForSeconds(6f);

        sharkFin.SetActive(true);

        yield return new WaitForSeconds(1f);

        sfxManager.clip = clips[1];

        sfxManager.Play();

        yield return new WaitForSeconds(3f);

        sfxManager.clip = clips[2];

        sfxManager.Play();

        boatAnimator.SetBool("Sink", true);

        yield return new WaitForSeconds(2.7f);

        endPanel.SetActive(true);

    }
}

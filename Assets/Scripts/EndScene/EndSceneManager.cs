using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EndSceneManager : MonoBehaviour
{
    public GameObject sharkPanel;
    public GameObject sharkFin;
    public GameObject endPanel;

    public Animator boatAnimator;
    public Animator sharkAnimator;


    void Start()
    {
        StartCoroutine(SharkEnding());
    }
    public IEnumerator SharkEnding()
    {
        yield return new WaitForSeconds(4f);

        sharkPanel.SetActive(true);

        yield return new WaitForSeconds(6f);

        sharkAnimator.SetBool("Exit", true);

        yield return new WaitForSeconds(6f);

        sharkFin.SetActive(true);

        yield return new WaitForSeconds(4f);

        boatAnimator.SetBool("Sink", true);

        yield return new WaitForSeconds(2.7f);

        endPanel.SetActive(true);

    }
}

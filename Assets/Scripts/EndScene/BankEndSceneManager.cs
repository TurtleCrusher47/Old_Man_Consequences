using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankEndSceneManager : MonoBehaviour
{
    public GameObject noticePanel;
    public GameObject endPanel;


    public Animator boatAnimator;
    public Animator noticeAnimator;

    void Start()
    {
        StartCoroutine(BankEnding());
    }

    private IEnumerator BankEnding()
    {
        yield return new WaitForSeconds(4f);

        noticePanel.SetActive(true);

        yield return new WaitForSeconds(8f);

        noticeAnimator.SetBool("Exit", true);

        yield return new WaitForSeconds(4f);

        boatAnimator.SetBool("Exit", true);

        yield return new WaitForSeconds(3f);

        endPanel.SetActive(true);
    }
}

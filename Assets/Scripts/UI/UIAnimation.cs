using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimation : MonoBehaviour
{
    [Header("Coin Image")]
    [SerializeField] private Image coinImage;
    [Header("Coin Sprites")]
    [SerializeField] private Sprite[] coinArray;
    [Header("Animation Speed")]
    [SerializeField] private float animSpeed;

    private int indexSprite;
    private Coroutine coroutineAim;
    private bool isDone;

    private void Start()
    {
        StartCoroutine(PlayAnimUI());
    }

    private IEnumerator PlayAnimUI()
    {
        yield return new WaitForSeconds(animSpeed);
        if(indexSprite >= coinArray.Length)
        {
            indexSprite = 0;
        }
        coinImage.sprite = coinArray[indexSprite];
        indexSprite += 1;
        if(isDone == false)
        {
            coroutineAim = StartCoroutine(PlayAnimUI());
        }
    }
}

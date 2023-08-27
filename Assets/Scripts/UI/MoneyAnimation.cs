using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyAnimation : MonoBehaviour
{
    // public TMP_Text[] moneyTexts; // Array of Text UI elements to display money
    // private float animationDuration = 1.0f; // Duration of the animation in seconds

    // private Coroutine[] currentAnimations; // Array to store ongoing animations

    // private void Start()
    // {
    //     currentAnimations = new Coroutine[moneyTexts.Length];
    // }

    // public void AnimateMoneyChange(int textIndex, int startValue, int targetValue)
    // {
    //     if (textIndex < 0 || textIndex >= moneyTexts.Length)
    //         return;

    //     if (currentAnimations[textIndex] != null)
    //         StopCoroutine(currentAnimations[textIndex]);

    //     currentAnimations[textIndex] = StartCoroutine(Animate(textIndex, startValue, targetValue));
    // }

    // private IEnumerator Animate(int textIndex, int startValue, int targetValue)
    // {
    //     float elapsedTime = 0;

    //     while (elapsedTime < animationDuration)
    //     {
    //         float t = elapsedTime / animationDuration;
    //         int newValue = Mathf.RoundToInt(Mathf.Lerp(startValue, targetValue, t));

    //         moneyTexts[textIndex].text = newValue.ToString();

    //         elapsedTime += Time.deltaTime;
    //         yield return null;
    //     }

    //     moneyTexts[textIndex].text = targetValue.ToString();
    // }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextUpdater : MonoBehaviour
{
    public TextMeshProUGUI[] textFields;
    public float targetValue = 100;
    public float animationDuration = 2;
    public bool countUp = true;
    public KeyCode testKey = KeyCode.Space; // Change this to the desired test key

    private float[] startValues;
    private float startTime;
    private bool countingStarted = false;

    void Start()
    {
        // startValues = new float[textFields.Length];
        // startTime = Time.time;

        // for (int i = 0; i < textFields.Length; i++)
        // {
        //     startValues[i] = countUp ? 0 : targetValue;
        //     textFields[i].text = startValues[i].ToString();
        // }
    }

    void Update()
    {
        if (Input.GetKeyDown(testKey) && !countingStarted)
        {
            StartCounting();
            countingStarted = true;
        }

        if (countingStarted)
        {
            float elapsedTime = Time.time - startTime;
            float lerpProgress = elapsedTime / animationDuration;

            for (int i = 0; i < textFields.Length; i++)
            {
                float newValue = Mathf.Lerp(startValues[i], targetValue, lerpProgress);
                textFields[i].text = Mathf.RoundToInt(newValue).ToString();

                if ((countUp && newValue >= targetValue) || (!countUp && newValue <= targetValue))
                {
                    textFields[i].text = targetValue.ToString();
                }
            }
        }
    }

    public void StartCounting()
    {
        startTime = Time.time;

        for (int i = 0; i < startValues.Length; i++)
        {
            startValues[i] = countUp ? 0 : targetValue;
        }
    }
}

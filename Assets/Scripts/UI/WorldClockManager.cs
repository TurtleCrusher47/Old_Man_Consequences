using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using TMPro;

public class WorldClockManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TMP_Text timeText; // Assign the time text
    [SerializeField] private TMP_Text dayText; // Assign the day text

    [Header("Time Settings")]
    [SerializeField] private float secondsPerMinute = 6.0f; // Adjust this to control time speed

    [Header("Global Light Settings")]
    [SerializeField] private Light2D globalLight;
    [SerializeField] private float minDensity = 0.3f;
    [SerializeField] private float maxDensity = 1.2f;
    [SerializeField] private float intensityChangeInterval = 600f;

    private int currentDay = 1;
    private int currentDayIndex = 1; // Index of the current day
    private string[] daysOfWeek = { "Sun.", "Mon.", "Tue.", "Wed.", "Thu.", "Fri.", "Sat." }; // Array of day names

    private int hours = 7; // Starting hour (7 AM)
    private int minutes = 0;
    private bool isMorning = true; // AM

    private float timeCounter = 0.0f;
    private float intensityChangeCounter = 0.0f;

    private void Update()
    {
        UpdateTime();
        //UpdateGlobalLight();
    }

    private void UpdateTime()
    {
        // Update the timeCounter based on real-time seconds
        timeCounter += Time.deltaTime;

        // Calculate minutes and hours
        while (timeCounter >= secondsPerMinute)
        {
            timeCounter -= secondsPerMinute;
            minutes++;

            if (minutes >= 60)
            {
                minutes = 0;
                hours++;

                if (hours >= 12)
                {
                    hours = 0;
                    isMorning = !isMorning;

                    if (isMorning && hours == 0) // Transition from 11:59 PM to 12:00 AM
                    {
                        currentDay++;
                        currentDayIndex = (currentDayIndex + 1) % 7; // Move to the next day of the week
                        dayText.text = daysOfWeek[currentDayIndex] + " " + currentDay;
                    }
                }
            }
        }

        if (minutes % 10 == 0)
        {
            // Update UI
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        // Format time in 12-hour format with AM and PM
        string amPm = isMorning ? "am" : "pm";
        int displayHours = hours % 12 == 0 ? 12 : hours % 12; // Handle 12:00
        string formattedTime = string.Format("{0:D1}:{1:D2} {2}", displayHours, minutes, amPm);

        // Update timeText UI element
        timeText.text = formattedTime;

        // Debug the time, days and numOfTheDays
        Debug.Log("Time " + formattedTime + " " + daysOfWeek[currentDayIndex] + " Day " + currentDayIndex);
    }

    /*
    private void UpdateGlobalLight()
    {
        intensityChangeCounter += Time.deltaTime;

        if(intensityChangeCounter >= intensityChangeInterval)
        {
            intensityChangeCounter -= intensityChangeInterval;

            float normalizedTime = hours * 60.0f + minutes;
            float intensityRatio = Mathf.Clamp01(normalizedTime / (12 * 60));
            float newIntensity = Mathf.Lerp(minDensity, maxDensity, intensityRatio);

            globalLight.intensity = newIntensity;
        }
    }
    */
}

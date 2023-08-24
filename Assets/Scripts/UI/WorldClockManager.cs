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

    [Header("World Clock Stats")]
    [SerializeField] private WorldClockData worldClockData;

    private bool isMorning = true; // AM

    private float timeCounter = 0.0f;

    private void Update()
    {
        UpdateTime();
    }

    private void UpdateTime()
    {
        // Update the timeCounter based on real-time seconds
        timeCounter += Time.deltaTime;

        // Calculate minutes and hours
        while (timeCounter >= worldClockData.secondsPerMinute)
        {
            timeCounter -= worldClockData.secondsPerMinute;
            worldClockData.minutes++;

            if (worldClockData.minutes >= 60)
            {
                worldClockData.minutes = 0;
                worldClockData.hours++;

                if (worldClockData.hours >= 12)
                {
                    worldClockData.hours = 0;
                    isMorning = !isMorning;

                    if (isMorning && worldClockData.hours == 0) // Transition from 11:59 PM to 12:00 AM
                    {
                        worldClockData.currentDay++;
                        worldClockData.currentDayIndex = (worldClockData.currentDayIndex + 1) % 7; // Move to the next day of the week
                    }
                }
            }
        }

        if (worldClockData.minutes % 10 == 0)
        {
            // Update UI
            UpdateUI();
        }

        // Debug the time, days and numOfTheDays
        Debug.Log("Time " + timeText.text + " " + worldClockData.daysOfWeek[worldClockData.currentDayIndex] + " Day " + worldClockData.currentDayIndex);
    }

    private void UpdateUI()
    {
        // Format time in 12-hour format with AM and PM
        string amPm = isMorning ? "am" : "pm";
        int displayHours = worldClockData.hours % 12 == 0 ? 12 : worldClockData.hours % 12; // Handle 12:00
        string formattedTime = string.Format("{0:D1}:{1:D2} {2}", displayHours, worldClockData.minutes, amPm);

        // Update timeText UI element
        timeText.text = formattedTime;
        // Update dayText UI element
        dayText.text = worldClockData.daysOfWeek[worldClockData.currentDayIndex] + " " + worldClockData.currentDay;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TMP_Text timeText; // Assign the time text
    [SerializeField] private TMP_Text dayText; // Assign the day text

    [Header("Time Settings")]
    [SerializeField] private float secondsPerMinute = 6.0f; // Adjust this to control time speed

    private int currentDay = 1;
    private int hours = 7; // Starting hour (7 AM)
    private int minutes = 0;
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
                        dayText.text = "Day " + currentDay.ToString();
                    }
                }
            }
        }

        Debug.Log("Time: " + hours + ":" + minutes + " " + timeCounter);

        // Update the Time UI in intervals of 15 Minutes
        if (minutes % 15 == 0) 
        {
            // Update UI
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        // Format time in 12-hour format with AM and PM
        string amPm = isMorning ? "AM" : "PM";
        int displayHours = hours % 12 == 0 ? 12 : hours % 12; // Handle 12:00
        string formattedTime = string.Format("{0:D2}:{1:D2} {2}", displayHours, minutes, amPm);

        // Update timeText UI element
        timeText.text = formattedTime;
    }
}

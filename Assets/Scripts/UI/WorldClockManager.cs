using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using TMPro;

public class WorldClockManager : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] UIPlayerStats uIPlayerStats;

    [Header("UI Elements")]
    [SerializeField] private TMP_Text timeText; // Assign the time text
    [SerializeField] private TMP_Text dayText; // Assign the day text

    [Header("World Clock Stats")]
    [SerializeField] private WorldClockData worldClockData;

    public static WorldClockManager Instance { get; private set; }

    private bool isMorning = true; // AM
    private float timeCounter = 0.0f;


    private void Awake()
    {
        // Ensure there is only one instance of UIPlayerStats
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }

        UpdateUI();
        uIPlayerStats.UpdateUIFromPlayerData();
    }

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
                        // Move to the next day of the week
                        NextDay();

                        if (worldClockData.currentDay / 7 > worldClockData.currentWeek)
                        {
                            NextWeek();
                        }
                    }
                }
            }
        }

        if (worldClockData.minutes % 5 == 0)
        {
            // Update UI
            UpdateUI();

            // Update global light
        }

        // Debug the time, days and numOfTheDays
        //Debug.Log("Time " + timeText.text + " " + worldClockData.daysOfWeek[worldClockData.currentDayIndex] + " Day " + worldClockData.currentDayIndex);
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

    public void NextDay()
    {
        worldClockData.currentDay++;
        worldClockData.currentDayIndex = (worldClockData.currentDayIndex + 1) % 7;
        worldClockData.hours = 7;
        worldClockData.minutes = 0;

        // Check shark debt
        if (worldClockData.currentDayIndex == 2)
        {
            // Check every tuesday if the player still owes money to the shark
            if (playerData.SharkDebt > 0)
            playerData.SharkDebtWeeks ++;
            else
            playerData.SharkDebtWeeks = 0;

            if (playerData.SharkDebtWeeks == 2)
            {
                // Put in shark debt warning for one week overdue
            }

            // Check if the player has owed the shark for 2 weeks
            if (playerData.SharkDebtWeeks >= 3)
            {
                // Put in code for what happens when it has been 3 weeks
                // Debug.Log("Ship has sunk");
            }
        }

        playerData.CurrentStamina = playerData.MaxStamina;

        UpdateUI();
        uIPlayerStats.UpdateUIFromPlayerData();
    }

    public void FaintNextDay()
    {
        worldClockData.currentDay++;
        worldClockData.currentDayIndex = (worldClockData.currentDayIndex + 1) % 7;
        worldClockData.hours = 7;
        worldClockData.minutes = 0;

        playerData.CurrentStamina = playerData.MaxStamina * 0.5f;

        UpdateUI();
        uIPlayerStats.UpdateUIFromPlayerData();
    }

    public void NextWeek()
    {
        worldClockData.currentWeek = worldClockData.currentDay / 7;

        // Loss condition
        if (worldClockData.currentWeek >= 11)
        {
            if (playerData.BankDebt > 0)
            {
                // Insert what to do if player loses
            }
        }

        // Code to transition to beach scene

        UpdateUI();
        uIPlayerStats.UpdateUIFromPlayerData();
    }
}

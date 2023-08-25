using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using TMPro;

public class WorldClockManager : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] UIPlayerStats uIPlayerStats;
    [SerializeField] NotificationManager notificationManager;

    [Header("UI Elements")]
    [SerializeField] private TMP_Text timeText; // Assign the time text
    [SerializeField] private TMP_Text dayText; // Assign the day text

    [Header("World Clock Stats")]
    [SerializeField] private WorldClockData worldClockData;

    [Header("Global Light")]
    [SerializeField] private Light2D globalLight; // Reference to the 2D light
    [SerializeField] private float baseIntensity = 1.0f; // Base intensity of the light
    [SerializeField] private float intensityChangePerHour = 0.1f; // Intensity change per hour

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

    IEnumerator Start()
    {
        yield return null;
        notificationManager = GameObject.FindGameObjectWithTag("NotificationManager").GetComponent<NotificationManager>();
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

        if (worldClockData.minutes % 30 == 0) // Check if it's a new hour
        {
            // Calculate intensity based on time of day
            float intensity = CalculateIntensity();

            // Apply the intensity to the global light
            globalLight.intensity = intensity;
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
                StartCoroutine(notificationManager.ShowNotification("SharkWarning"));
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

        // If player has 1 week left to repay bank
        if (worldClockData.currentWeek == 6)
        {
            if (playerData.BankDebt > 0)
            {
                StartCoroutine(notificationManager.ShowNotification("BankOneWeek"));
            }
        }
        // If player has 2 weeks left to repay bank
        else if (worldClockData.currentWeek == 7)
        {
            if (playerData.BankDebt > 0)
            {
                StartCoroutine(notificationManager.ShowNotification("BankTwoWeeks"));
            }
        }
        // If player has 3 weeks left to repay bank
        else if (worldClockData.currentWeek == 8)
        {
            if (playerData.BankDebt > 0)
            {
                StartCoroutine(notificationManager.ShowNotification("BankThreeWeeks"));
            }
        }
        // Loss condition
        else if (worldClockData.currentWeek == 11)
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

    private float CalculateIntensity()
    {
        float intensityMultiplier = 0.0f;

        if (worldClockData.hours >= 20 || worldClockData.hours < 7)
        {
            // Night: Intensity at 8 PM is 0.3
            intensityMultiplier = Mathf.Lerp(1.1f, 0.3f, Mathf.Clamp01((float)(worldClockData.hours + 12 - 20) / 4.0f));
        }
        else if (worldClockData.hours < 12)
        {
            // Morning: Intensity from 7 AM to 12 PM
            intensityMultiplier = Mathf.Lerp(0.3f, 1.1f, (float)(worldClockData.hours - 7) / 5.0f);
        }
        else if (worldClockData.hours < 18)
        {
            // Afternoon: Intensity from 12 PM to 6 PM
            intensityMultiplier = Mathf.Lerp(1.1f, 0.5f, (float)(worldClockData.hours - 12) / 6.0f);
        }
        else if (worldClockData.hours >= 18 && worldClockData.hours < 20)
        {
            // Evening: Intensity from 6 PM to 8 PM
            intensityMultiplier = Mathf.Lerp(0.5f, 0.3f, (float)(worldClockData.hours - 18) / 2.0f);
        }
        else
        {
            // Night: Intensity at and after 8 PM is 0.3
            intensityMultiplier = 0.3f;
        }

        // Apply a change every 30 minutes
        float progressThroughHour = (float)worldClockData.minutes / 60.0f;
        float change = intensityChangePerHour * progressThroughHour;
        intensityMultiplier += change;

        // Clamp intensity to prevent going above 1.1 or below 0.3
        intensityMultiplier = Mathf.Clamp(intensityMultiplier, 0.3f, 1.1f);

        return baseIntensity * intensityMultiplier;
    }
}

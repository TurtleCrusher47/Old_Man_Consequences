using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using TMPro;
using UnityEditor.SearchService;

public class WorldClockManager : MonoBehaviour
{
    [Header("Player Data")]
    [SerializeField] PlayerData playerData;
    [Header("Player Stats")]
    [SerializeField] UIPlayerStats uIPlayerStats;
    [Header("Notification Elements")]
    [SerializeField] GameObject notificationManagerGO;
    [SerializeField] NotificationManager notificationManager;
    [Header("Phone Elements")]
    [SerializeField] PhoneManager phoneManager;
    [Header("Blackout Elements")]
    [SerializeField] GameObject blackoutPanel;

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
        notificationManagerGO = GameObject.FindGameObjectWithTag("NotificationManager");
        notificationManager = notificationManagerGO.GetComponent<NotificationManager>();
        
        yield return null;
        if (notificationManager)
        notificationManagerGO.SetActive(false);
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
                    }
                }
            }
        }

        if (worldClockData.minutes % 5 == 0)
        {
            // Update UI
            UpdateUI();
        }

        if (worldClockData.minutes % 1 == 0)
        {
            // Update global light
            // Calculate intensity and color based on time of day
            float intensity = CalculateIntensity();

            // Apply the intensity and color to the global light
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
        StartCoroutine(Blackout());
        
        if (playerData.BankDebt <= 0)
        {
            SceneChanger.ChangeScene("WinEndScene");
            return;
        }
        
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
                SceneChanger.ChangeScene("SharkEndScene");
            }
        }

        playerData.CurrentStamina = playerData.MaxStamina;

        if (playerData.CurrentHydration < 30)
        {
            playerData.CurrentHydration = 30;
        }

        UpdateUI();
        uIPlayerStats.UpdateUIFromPlayerData();

        if (worldClockData.currentDay / 7 > worldClockData.currentWeek)
        {
            NextWeek();
        }
    }

    public void FaintNextDay()
    {
        if (playerData.BankDebt <= 0)
        {
            SceneChanger.ChangeScene("WinEndScene");
            return;
        }
        
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
                SceneChanger.ChangeScene("SharkEndScene");
            }
        }

        playerData.CurrentStamina = playerData.MaxStamina * 0.5f;

        if (playerData.CurrentHydration < 30)
        {
            playerData.CurrentHydration = 30;
        }

        UpdateUI();
        uIPlayerStats.UpdateUIFromPlayerData();

        if (worldClockData.currentDay / 7 >= worldClockData.currentWeek)
        {
            NextWeek();
        }
    }

    public void NextWeek()
    {
        playerData.CurrentStamina = playerData.MaxStamina;

            playerData.CurrentHydration = playerData.MaxHydration;


        SceneChanger.ChangeScene("NPCScene");
        
        worldClockData.currentWeek ++;
        worldClockData.currentDay = worldClockData.currentWeek * 7 - 6;

        worldClockData.currentDayIndex = 0;
        worldClockData.hours = 7;
        worldClockData.minutes = 0;

        phoneManager.UpdateWeekText();

        // If player has 1 week left to repay bank
        if (worldClockData.currentWeek == 10)
        {
            if (playerData.BankDebt > 0)
            {
                StartCoroutine(notificationManager.ShowNotification("BankOneWeek"));
            }
        }
        // If player has 2 weeks left to repay bank
        else if (worldClockData.currentWeek == 9)
        {
            if (playerData.BankDebt > 0)
            {
                StartCoroutine(notificationManager.ShowNotification("BankTwoWeeks"));
            }
        }
        // If player has 3 weeks left to repay bank
        else if (worldClockData.currentWeek == 9)
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
                SceneChanger.ChangeScene("BankEndScene");
            }

        }

        // Code to transition to beach scene
        
        UpdateUI();
        uIPlayerStats.UpdateUIFromPlayerData();

        phoneManager.UpdateWeekText();

        UpdateUI();
        uIPlayerStats.UpdateUIFromPlayerData();
    }

    private float CalculateIntensity()
    {
        float intensityMultiplier = 0.0f;

        // Convert 12-hour format to a normalized value between 0 and 1
        float normalizedHour = (worldClockData.hours % 12 + worldClockData.minutes / 60.0f) / 12.0f;

        if (isMorning == true && worldClockData.hours >= 7 && worldClockData.hours < 12)
        {
            // Set intensityMultiplier for morning
            intensityMultiplier = Mathf.Lerp(0.6f, 1f, normalizedHour);
        }
        else if (isMorning == false && worldClockData.hours >= 0 && worldClockData.hours <= 6)
        {
            // Set intensityMultiplier for afternoon
            intensityMultiplier = Mathf.Lerp(1f, 0.7f, normalizedHour);
        }
        else if (isMorning == false && worldClockData.hours >= 6 && worldClockData.hours < 9)
        {
            // Set intensityMultiplier for evening
            intensityMultiplier = Mathf.Lerp(0.7f, 0.5f, normalizedHour);
        }
        else if (isMorning == false && worldClockData.hours >= 9 && worldClockData.hours < 0)
        {
            // Set intensityMultiplier for night
            intensityMultiplier = Mathf.Lerp(0.5f, 0.3f, normalizedHour);
        }

        // Clamp intensity to prevent going above 1.05 or below 0.3
        intensityMultiplier = Mathf.Clamp(intensityMultiplier, 0.3f, 1.05f);

        return baseIntensity * intensityMultiplier;
    }

    private IEnumerator Blackout()
    {
        blackoutPanel.SetActive(true);

        yield return new WaitForSeconds(2f);

        SceneChanger.ChangeScene("BoatScene");

        yield return new WaitForSeconds(1f);

        blackoutPanel.SetActive(false);

       
    }
}

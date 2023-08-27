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

    [Header("Light Colour")]
    [SerializeField] private Color morningColor;
    [SerializeField] private Color afternoonColor;
    [SerializeField] private Color eveningColor;
    [SerializeField] private Color nightColor;

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

            // Update global light
        }

        if (worldClockData.minutes % 30 == 0) // Check if it's a new hour
        {
            // Calculate intensity and color based on time of day
            float intensity = CalculateIntensity();
            Color lightColor = GetLightColor();

            // Apply the intensity and color to the global light
            globalLight.intensity = intensity;
            globalLight.color = lightColor;
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

        UpdateUI();
        uIPlayerStats.UpdateUIFromPlayerData();

        if (worldClockData.currentDay / 7 > worldClockData.currentWeek)
        {
            NextWeek();
        }
    }

    public void NextWeek()
    {
        phoneManager.UpdateWeekText();

        SceneChanger.ChangeScene("NPCScene");
        
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

    private Color GetLightColor()
    {
        if (worldClockData.hours >= 0.5f && worldClockData.hours < 1.05f)
        {
            return Color.Lerp(morningColor, afternoonColor, (worldClockData.hours - 0.5f) / (1.05f - 0.5f));
        }
        else if (worldClockData.hours >= 1.05f && worldClockData.hours < 0.6f)
        {
            return Color.Lerp(afternoonColor, eveningColor, (worldClockData.hours - 1.05f) / (0.6f - 1.05f));
        }
        else if (worldClockData.hours >= 0.6f && worldClockData.hours < 0.3f)
        {
            return Color.Lerp(eveningColor, nightColor, (worldClockData.hours - 0.6f) / (0.3f - 0.6f));
        }
        else
        {
            return nightColor;
        }
    }

    private float CalculateIntensity()
    {
        float intensityMultiplier = 0.0f;
        Color targetColor = Color.white; // Default color

        // Calculate intensityMultiplier as before

        // Convert 12-hour format to a normalized value between 0 and 1
        float normalizedHour = worldClockData.hours / 12.0f;

        if (worldClockData.hours >= 0.5f && worldClockData.hours < 1.05f)
        {
            // Set intensityMultiplier for morning
            targetColor = morningColor;
            intensityMultiplier = Mathf.Lerp(0.3f, 1.1f, (normalizedHour + 0.5f) % 1.0f);
        }
        else if (worldClockData.hours >= 1.05f && worldClockData.hours < 6.0f)
        {
            // Set intensityMultiplier for afternoon
            targetColor = afternoonColor;
            intensityMultiplier = Mathf.Lerp(1.1f, 0.5f, (normalizedHour + 1.05f) % 1.0f);
        }
        else if (worldClockData.hours >= 6.0f && worldClockData.hours < 7.5f)
        {
            // Set intensityMultiplier for evening
            targetColor = eveningColor;
            intensityMultiplier = Mathf.Lerp(0.5f, 0.3f, (normalizedHour + 6.0f) % 1.0f);
        }
        else
        {
            // Set intensityMultiplier for night
            targetColor = nightColor;
            intensityMultiplier = Mathf.Lerp(0.3f, 0.3f, (normalizedHour + 7.5f) % 1.0f);
        }

        // Apply the color to the global light's color property
        globalLight.color = targetColor;

        // Apply the change every 30 minutes
        float progressThroughHour = (float)worldClockData.minutes / 60.0f;
        float change = intensityChangePerHour * progressThroughHour;
        intensityMultiplier += change;

        // Clamp intensity to prevent going above 1.1 or below 0.3
        intensityMultiplier = Mathf.Clamp(intensityMultiplier, 0.3f, 1.1f);

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Header("Player Data")]
    public PlayerData playerData;

    [Header("World Clock Data")]
    public WorldClockData worldClockData;

    public void NewGame(string sceneName)
    {
        //NewGameData();

        playerData.Reset();
        worldClockData.Reset();

        SceneManager.LoadScene(sceneName);
    }

    public void LoadGame(string sceneName)
    {
        // Load player data from PlayerPrefs
        playerData.CurrentStamina = PlayerPrefs.GetFloat("CurrentStamina");
        playerData.CurrentHydration = PlayerPrefs.GetFloat("CurrentHydration");
        playerData.Balance = PlayerPrefs.GetInt("Balance");
        playerData.SharkDebt = PlayerPrefs.GetInt("SharkDebt");
        playerData.BankDebt = PlayerPrefs.GetInt("BankDebt");

        // Load World Clock Data from PlayerPrefs
        worldClockData.hours = PlayerPrefs.GetInt("CurrentHour");
        worldClockData.minutes = PlayerPrefs.GetInt("CurrentMinute");
        worldClockData.currentDay = PlayerPrefs.GetInt("CurrentDay");
        worldClockData.currentDayIndex = PlayerPrefs.GetInt("CurrentDayIndex");
        worldClockData.currentWeek = PlayerPrefs.GetInt("CurrentWeek");

        Debug.Log("run");

        SceneManager.LoadScene(sceneName);
    }

    public void Credits(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitScene()
    {
        Application.Quit();
    }

    public void NewGameData()
    {
        // Player Data Reset
        playerData.CurrentStamina = 100;
        playerData.CurrentHydration = 100;
        playerData.Balance = 100;
        playerData.SharkDebt = 100;
        playerData.BankDebt = 1000;

        // World Clock Data Reset
        worldClockData.hours = 7;
        worldClockData.minutes = 0;
        worldClockData.currentDay = 1;
        worldClockData.currentDayIndex = 1;
        worldClockData.currentWeek = 0;

        //Debug.Log("Run");
    }
}

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
        SceneManager.LoadScene(sceneName);
        NewGameData();
    }

    public void LoadGame(string sceneName)
    {
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

    private void NewGameData()
    {
        // Player Data Reset
        playerData.CurrentStamina = 100;
        playerData.CurrentHydration = 100;
        playerData.Balance = 100;
        playerData.SharkDebt = 100;
        playerData.BankDebt = 100000;

        // World Clock Data Reset
        worldClockData.hours = 7;
        worldClockData.minutes = 0;
        worldClockData.currentDay = 1;
        worldClockData.currentDayIndex = 1;
    }
}

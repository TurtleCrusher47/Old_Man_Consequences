using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIPlayerStats : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;
    
    [Header("Hunger Elements")]
    [SerializeField] private Image hungerBar; // Assign image object

    [Header("Thirst Elements")]
    [SerializeField] private Image thirstBar; // Assign image object

    [Header("PlayerStats")]
    public PlayerData playerData;

    public static UIPlayerStats Instance { get; private set; }

    private float nextThirstDecreaseTime;
    private float nextHungerDecreaseTime;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        nextThirstDecreaseTime = Time.time + playerData.HydrationDecreaseInterval;
        nextHungerDecreaseTime = Time.time + playerData.StaminaDecreasePerSecond;
    }

    // Update is called once per frame
    void Update()
    {
        // Handle thirst decrease over time
        if (Time.time >= nextThirstDecreaseTime)
        {
            DecreaseThirst(playerData.HydrationDecreasePerSecond);
            nextThirstDecreaseTime = Time.time + playerData.HydrationDecreaseInterval;
        }

        // Handle hunger decrease over time
        if (Time.time >= nextHungerDecreaseTime)
        {
            DecreaseHunger(playerData.StaminaDecreasePerSecond);
            nextHungerDecreaseTime = Time.time + playerData.StaminaDecreaseInterval;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            DecreaseHunger(5);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            playerData.Balance = 5;
        }
    }

    public void UpdateUIFromPlayerData()
    {
        // Update hunger UI
        hungerBar.fillAmount = playerData.CurrentStamina / playerData.MaxStamina;

        // Update thirst UI
        thirstBar.fillAmount = playerData.CurrentHydration / playerData.MaxHydration;
    }

    private void PlayerHungerRefill()
    {
        hungerBar.fillAmount = playerData.MaxStamina;
    }

    private void PlayerThirstRefill()
    {
        float newThirstValue = Mathf.Clamp(thirstBar.fillAmount + (30f / playerData.MaxHydration), 0f, 1f);
        thirstBar.fillAmount = newThirstValue;
    }

    public void DecreaseHunger(float amount)
    {
        // Update hunger UI
        float newHungerValue = Mathf.Clamp(playerData.CurrentStamina - amount, 0f, playerData.MaxStamina);
        playerData.CurrentStamina = newHungerValue;
        hungerBar.fillAmount = newHungerValue / playerData.MaxStamina;

        if (playerData.CurrentStamina <= 0)
        playerManager.PlayerFaintStamina();

    }

    private void DecreaseThirst(float amount)
    {
        // Update thirst UI
        float newThirstValue = Mathf.Clamp(playerData.CurrentHydration - amount, 0f, playerData.MaxHydration);
        playerData.CurrentHydration = newThirstValue;
        thirstBar.fillAmount = newThirstValue / playerData.MaxHydration;

        if (playerData.CurrentHydration <= 0)
        playerManager.PlayerFaintHydration();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIPlayerStats : MonoBehaviour
{
    [Header("Hunger Elements")]
    [SerializeField] private Image hungerBar; // Assign image object

    [Header("Thirst Elements")]
    [SerializeField] private Image thirstBar; // Assign image object

    [Header("PlayerStats")]
    public PlayerData playerData;

    [Header("Players Money")]
    [SerializeField] private TMP_Text playerMoney;

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

        // if (Input.GetKeyDown(KeyCode.Mouse0))
        // {
        //     DecreaseHunger(5);
        // }

        UpdateMoneyText();
        UpdateUIFromPlayerData();
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
    }

    private void DecreaseThirst(float amount)
    {
        // Update thirst UI
        float newThirstValue = Mathf.Clamp(playerData.CurrentHydration - amount, 0f, playerData.MaxHydration);
        playerData.CurrentHydration = newThirstValue;
        thirstBar.fillAmount = newThirstValue / playerData.MaxHydration;
    }

    private void UpdateMoneyText()
    {
        playerMoney.text = playerData.BankDebt.ToString();
        DynamicTextFontSize(playerMoney);
    }

    private void DynamicTextFontSize(TMP_Text textComponent)
    {
        float originalFontSize = textComponent.fontSize;
        TMP_TextInfo textInfo = textComponent.textInfo;

        float preferredWidth = textComponent.preferredWidth;
        float availableWidth = textComponent.rectTransform.rect.width;

        while (preferredWidth > availableWidth && textComponent.fontSize > 1)
        {
            textComponent.fontSize--;
            preferredWidth = textComponent.preferredWidth;
        }

        if (preferredWidth > availableWidth)
        {
            textComponent.fontSize = originalFontSize; // Reset to original size if necessary
        }
    }
}

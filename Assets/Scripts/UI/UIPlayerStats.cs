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

        nextThirstDecreaseTime = Time.time + playerData.thirstDecreaseInterval;
        nextHungerDecreaseTime = Time.time + playerData.hungerDecreasePerSecond;
    }

    // Update is called once per frame
    void Update()
    {
        // Handle thirst decrease over time
        if (Time.time >= nextThirstDecreaseTime)
        {
            DecreaseThirst(playerData.thirstDecreaseAmount);
            nextThirstDecreaseTime = Time.time + playerData.thirstDecreaseInterval;
        }

        // Handle hunger decrease over time
        if (Time.time >= nextHungerDecreaseTime)
        {
            DecreaseHunger(playerData.hungerDecreasePerSecond);
            nextHungerDecreaseTime = Time.time + playerData.hungerDecreaseInterval;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            DecreaseHunger(5);
        }

        UpdateMoneyText();
    }

    public void UpdateUIFromPlayerData()
    {
        // Update hunger UI
        hungerBar.fillAmount = playerData.currentHunger / playerData.maxHunger;

        // Update thirst UI
        thirstBar.fillAmount = playerData.currentThirst / playerData.maxThirst;
    }

    private void PlayerHungerRefill()
    {
        hungerBar.fillAmount = playerData.maxHunger;
    }

    private void PlayerThirstRefill()
    {
        float newThirstValue = Mathf.Clamp(thirstBar.fillAmount + (30f / playerData.maxThirst), 0f, 1f);
        thirstBar.fillAmount = newThirstValue;
    }

    public void DecreaseHunger(float amount)
    {
        // Update hunger UI
        float newHungerValue = Mathf.Clamp(playerData.currentHunger - amount, 0f, playerData.maxHunger);
        playerData.currentHunger = newHungerValue;
        hungerBar.fillAmount = newHungerValue / playerData.maxHunger;
    }

    private void DecreaseThirst(float amount)
    {
        // Update thirst UI
        float newThirstValue = Mathf.Clamp(playerData.currentThirst - amount, 0f, playerData.maxThirst);
        playerData.currentThirst = newThirstValue;
        thirstBar.fillAmount = newThirstValue / playerData.maxThirst;
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

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
    [SerializeField] private PlayerData playerStats;

    [Header("Players Money")]
    [SerializeField] private TMP_Text playerMoney;

    public static UIPlayerStats Instance { get; private set; }

    private float nextThirstDecreaseTime;
    private float nextHungerDecreaseTime;

    private void Awake()
    {
        // Ensure there is only one instance of UIPlayerStats
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        nextThirstDecreaseTime = Time.time + playerStats.thirstDecreaseInterval;
        nextHungerDecreaseTime = Time.time + playerStats.hungerDecreasePerSecond;
    }

    // Update is called once per frame
    void Update()
    {
        // Handle thirst decrease over time
        if (Time.time >= nextThirstDecreaseTime)
        {
            DecreaseThirst(playerStats.thirstDecreaseAmount);
            nextThirstDecreaseTime = Time.time + playerStats.thirstDecreaseInterval;
        }

        // Handle hunger decrease over time
        if (Time.time >= nextHungerDecreaseTime)
        {
            DecreaseHunger(playerStats.hungerDecreasePerSecond);
            nextHungerDecreaseTime = Time.time + playerStats.hungerDecreaseInterval;
        }

        UpdateMoneyText();
    }

    private void PlayerHungerRefill()
    {
        hungerBar.fillAmount = playerStats.maxHunger;
    }

    private void PlayerThirstRefill()
    {
        float newThirstValue = Mathf.Clamp(thirstBar.fillAmount + (30f / playerStats.maxThirst), 0f, 1f);
        thirstBar.fillAmount = newThirstValue;
    }

    public void DecreaseHunger(float amount)
    {
        // Update hunger UI
        float newHungerValue = Mathf.Clamp(hungerBar.fillAmount - (amount / playerStats.maxHunger), 0f, 1f);
        hungerBar.fillAmount = newHungerValue;
    }

    private void DecreaseThirst(float amount)
    {
        // Update thirst UI
        thirstBar.fillAmount -= amount / playerStats.maxThirst; 
    }

    private void UpdateMoneyText()
    {
        playerMoney.text = playerStats.Debt.ToString();
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
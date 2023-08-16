using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TestPlayerStats : MonoBehaviour
{
    [Header("Hunger")]
    [SerializeField] private Image hungerBar;
    [SerializeField] private float maxHunger = 100f;
    [SerializeField] private float hungerDecreaseAmountOnClick = 5f; // Decrease hunger on mouse click

    [Header("Thirst")]
    [SerializeField] private Image thirstBar;
    [SerializeField] private float maxThirst = 150f; // Increase max thirst to 150f
    [SerializeField] private float thirstDecreaseInterval = 5f; // Decrease thirst every 5 seconds
    [SerializeField] private float thirstDecreaseAmount = 5f;

    // Scrap idea but c first
    [Header("Energy")]
    [SerializeField] private TMP_Text energyProgressText;
    [SerializeField] private Slider energySlider;
    [SerializeField] private float energyDecreaseInterval = 8f; // Decrease energy every 8 seconds
    [SerializeField] private float energyDecreaseAmount = 10f;

    private float nextThirstDecreaseTime;
    private float nextEnergyDecreaseTime;

    private float initialHunger;
    private float initialThirst;
    private float initialEnergy;

    // Start is called before the first frame update
    void Start()
    {
        nextThirstDecreaseTime = Time.time + thirstDecreaseInterval;
        nextEnergyDecreaseTime = Time.time + energyDecreaseInterval;

        initialHunger = maxHunger;
        initialThirst = maxThirst;
        initialEnergy = energySlider.maxValue;  
    }

    // Update is called once per frame
    void Update()
    {
        // Handle hunger decrease on mouse click
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            DecreaseHunger(hungerDecreaseAmountOnClick);
        }

        // Handle Test reset of players Hunger
        if (Input.GetKeyDown(KeyCode.H))
        {
            PlayerHungerRefill();
        }

        // Handle Test reset of players Thirst
        if (Input.GetKeyDown(KeyCode.T))
        {
            PlayerThirstRefill(30);
        }

        // Handle Test reset of players Energy
        if (Input.GetKeyDown(KeyCode.E))
        {
            PlayerEnergyRefill();
        }

        // Handle thirst decrease over time
        if (Time.time >= nextThirstDecreaseTime)
        {
            DecreaseThirst(thirstDecreaseAmount);
            nextThirstDecreaseTime = Time.time + thirstDecreaseInterval;
        }

        // Handle energy decrease over time
        if (Time.time >= nextEnergyDecreaseTime)
        {
            DecreaseEnergy(energyDecreaseAmount);
            nextEnergyDecreaseTime = Time.time + energyDecreaseInterval;
        }
    }

    private void PlayerHungerRefill()
    {
        hungerBar.fillAmount = initialHunger / maxHunger;
    }

    private void PlayerThirstRefill(float amount)
    {
        thirstBar.fillAmount = initialThirst / amount;
        // Refill thirst by 30 
    }

    private void PlayerEnergyRefill()
    {
        energySlider.value = initialEnergy;
        energyProgressText.text = $"{initialEnergy} %";
    }

    private void DecreaseHunger(float amount)
    {
        // Update hunger UI
        hungerBar.fillAmount -= amount / maxHunger;
    }

    private void DecreaseThirst(float amount)
    {
        // Update thirst UI
        thirstBar.fillAmount -= amount / maxThirst; 
    }

    private void DecreaseEnergy(float amount)
    {
        // Update energy UI
        energySlider.value -= amount;     
        energyProgressText.text = $"{energySlider.value} %";
    }
}

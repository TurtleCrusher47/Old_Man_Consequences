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
    [SerializeField] private float hungerDecreaseAmountOnClick = 10f; // Decrease hunger on mouse click

    [Header("Thirst")]
    [SerializeField] private Image thirstBar;
    [SerializeField] private float maxThirst = 100f;
    [SerializeField] private float thirstDecreaseInterval = 5f; // Decrease thirst every 5 seconds
    [SerializeField] private float thirstDecreaseAmount = 5f;

    [Header("Energy")]
    [SerializeField] private TMP_Text energyProgressText;
    [SerializeField] private Slider energySlider;
    [SerializeField] private float energyDecreaseInterval = 8f; // Decrease energy every 8 seconds
    [SerializeField] private float energyDecreaseAmount = 10f;

    private float nextThirstDecreaseTime;
    private float nextEnergyDecreaseTime;

    // Start is called before the first frame update
    void Start()
    {
        nextThirstDecreaseTime = Time.time + thirstDecreaseInterval;
        nextEnergyDecreaseTime = Time.time + energyDecreaseInterval;
    }

    // Update is called once per frame
    void Update()
    {
        // Handle hunger decrease on mouse click
        if (Input.GetMouseButtonDown(0))
        {
            DecreaseHunger(hungerDecreaseAmountOnClick);
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

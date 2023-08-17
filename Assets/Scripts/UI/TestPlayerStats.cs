using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TestPlayerStats : MonoBehaviour
{
    [Header("Hunger Elements")]
    [SerializeField] private Image hungerBar; // Assign image object
    [Header("Hunger Variables")]
    [SerializeField] private float maxHunger = 100f; // assign a max amount of hunger
    [SerializeField] private float hungerDecreaseAmountOnClick = 5f; // Decrease hunger on mouse click
    [SerializeField] private float hungerDecreaseInterval = 8f; // Decrease energy every certain
    [SerializeField] private float hungerDecreasePerSecond = 10f; // Decrease hunger a certain amount

    [Header("Thirst Elements")]
    [SerializeField] private Image thirstBar; // Assign image object
    [Header("Thirst Variables")]
    [SerializeField] private float maxThirst = 100f; // assign a max amount of hunger
    [SerializeField] private float thirstDecreaseInterval = 5f; // Decrease thirst every certain seconds
    [SerializeField] private float thirstDecreaseAmount = 5f; // Decrease thirst a certain amount

    // Scrap idea but c first
    /*
    [Header("Energy")]
    [SerializeField] private TMP_Text energyProgressText;
    [SerializeField] private Slider energySlider;
    [SerializeField] private float energyDecreaseInterval = 8f; // Decrease energy every 8 seconds
    [SerializeField] private float energyDecreaseAmount = 10f;*/

    private float nextThirstDecreaseTime;
    private float nextHungerDecreaseTime;

    // Start is called before the first frame update
    void Start()
    {
        nextThirstDecreaseTime = Time.time + thirstDecreaseInterval;
        nextHungerDecreaseTime = Time.time + hungerDecreasePerSecond;
    }

    // Update is called once per frame
    void Update()
    {
        // Handle hunger decrease on enter click
        if (Input.GetKeyDown(KeyCode.Return))
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
            PlayerThirstRefill();
        }

        // Handle Test reset of players Energy
        /*
        if (Input.GetKeyDown(KeyCode.E))
        {
            PlayerEnergyRefill();
        }*/

        // Handle thirst decrease over time
        if (Time.time >= nextThirstDecreaseTime)
        {
            DecreaseThirst(thirstDecreaseAmount);
            nextThirstDecreaseTime = Time.time + thirstDecreaseInterval;
        }

        // Handle hunger decrease over time
        if (Time.time >= nextHungerDecreaseTime)
        {
            DecreaseHunger(hungerDecreasePerSecond);
            nextHungerDecreaseTime = Time.time + hungerDecreaseInterval;
        }
    }

    private void PlayerHungerRefill()
    {
        hungerBar.fillAmount = maxHunger;
    }

    private void PlayerThirstRefill()
    {
        float newThirstValue = Mathf.Clamp(thirstBar.fillAmount + (30f / maxThirst), 0f, 1f);
        thirstBar.fillAmount = newThirstValue;
    }

    /*
    private void PlayerEnergyRefill()
    {
        energySlider.value = initialEnergy;
        energyProgressText.text = $"{initialEnergy} %";
    } */

    private void DecreaseHunger(float amount)
    {
        // Update hunger UI
        float newHungerValue = Mathf.Clamp(hungerBar.fillAmount - (amount / maxHunger), 0f, 1f);
        hungerBar.fillAmount = newHungerValue;
    }

    private void DecreaseThirst(float amount)
    {
        // Update thirst UI
        thirstBar.fillAmount -= amount / maxThirst; 
    }

    /*
    private void DecreaseEnergy(float amount)
    {
        // Update energy UI
        energySlider.value -= amount;     
        energyProgressText.text = $"{energySlider.value} %";
    } */
}

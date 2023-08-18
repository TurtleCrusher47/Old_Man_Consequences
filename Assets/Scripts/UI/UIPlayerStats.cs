using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerStats : MonoBehaviour
{
    [Header("Hunger Elements")]
    [SerializeField] private Image hungerBar; // Assign image object

    [Header("Thirst Elements")]
    [SerializeField] private Image thirstBar; // Assign image object

    [Header("PlayerStats")]
    [SerializeField] private PlayerData playerStats;

    private float nextThirstDecreaseTime;
    private float nextHungerDecreaseTime;

    // Start is called before the first frame update
    void Start()
    {
        nextThirstDecreaseTime = Time.time + playerStats.thirstDecreaseInterval;
        nextHungerDecreaseTime = Time.time + playerStats.hungerDecreasePerSecond;
    }

    // Update is called once per frame
    void Update()
    {
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
            DecreaseThirst(playerStats.thirstDecreaseAmount);
            nextThirstDecreaseTime = Time.time + playerStats.thirstDecreaseInterval;
        }

        // Handle hunger decrease over time
        if (Time.time >= nextHungerDecreaseTime)
        {
            DecreaseHunger(playerStats.hungerDecreasePerSecond);
            nextHungerDecreaseTime = Time.time + playerStats.hungerDecreaseInterval;
        }
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

    /*
    private void PlayerEnergyRefill()
    {
        energySlider.value = initialEnergy;
        energyProgressText.text = $"{initialEnergy} %";
    } */

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

    /*
    private void DecreaseEnergy(float amount)
    {
        // Update energy UI
        energySlider.value -= amount;     
        energyProgressText.text = $"{energySlider.value} %";
    } */
}

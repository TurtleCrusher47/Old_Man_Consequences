using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    [Header("Hunger Variables")]
    public float maxHunger = 100f; // assign a max amount of hunger
    public float hungerDecreaseAmountOnClick = 5f; // Decrease hunger on mouse click
    public float hungerDecreaseInterval = 8f; // Decrease energy every certain
    public float hungerDecreasePerSecond = 10f; // Decrease hunger a certain amount

    [Header("Thirst Variables")]
    public float maxThirst = 100f; // assign a max amount of hunger
    public float thirstDecreaseInterval = 5f; // Decrease thirst every certain seconds
    public float thirstDecreaseAmount = 5f; // Decrease thirst a certain amount

    [Header("Player's Money")]
    public float currMoney;

    [Header("Miscellaneous")]

    [SerializeField] private int movementSpeed = 5;
    public int MovementSpeed => movementSpeed;

    [SerializeField] private float sprintSpeed = 6.5f;
    public float SprintSpeed => sprintSpeed;

    // Stats
    [SerializeField] private float maxStamina = 10f;
    public float MaxStamina => maxStamina;

    [SerializeField] private float currentStamina = 10f;
    public float CurrentStamina
    {
        get => currentStamina;
        set => currentStamina = value;
    }

    [SerializeField] private float currentHunger = 10f;
    public float CurrentHunger
    {
        get => currentHunger;
        set => currentHunger = value;
    }

    [SerializeField] private float currentHydration = 10f;
    public float CurrentHydration
    {
        get => currentHydration;
        set => currentHydration = value;
    }

    [SerializeField] private int balance = 0;
    public int Balance
    {
        get => balance;
        set => balance = value;
    }

    [SerializeField] private int debt = 100000;
    public int Debt
    {
        get => debt;
        set => debt = value;
    }


}

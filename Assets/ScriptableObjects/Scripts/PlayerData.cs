using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    [SerializeField] private int movementSpeed = 5;
    public int MovementSpeed => movementSpeed;

    [SerializeField] private float sprintSpeed = 6.5f;
    public float SprintSpeed => sprintSpeed;

    // Stats
    // Stamina
    [Header("Stamina")]
    [SerializeField] private float currentStamina = 10f;
    public float CurrentStamina
    {
        get => currentStamina;
        set => currentStamina = value;
    }

    [SerializeField] private float maxStamina = 100f;
    public float MaxStamina => maxStamina;

    [SerializeField] private float staminaDecreaseAmountOnClick = 5f;
    public float StaminaDecreaseAmountOnClick => staminaDecreaseAmountOnClick;

    [SerializeField] private float staminaDecreaseInterval = 6f;
    public float StaminaDecreaseInterval => staminaDecreaseInterval;

    [SerializeField] private float staminaDecreasePerSecond = 1f;
    public float StaminaDecreasePerSecond => staminaDecreasePerSecond;

    // Hydration
    [Header("Hydration")]
    [SerializeField] private float currentHydration = 10f;
    public float CurrentHydration
    {
        get => currentHydration;
        set => currentHydration = value;
    }

    [SerializeField] private float maxHydration = 100f;
    public float MaxHydration => maxHydration;

    [SerializeField] private float hydrationDecreaseInterval = 5f;
    public float HydrationDecreaseInterval => hydrationDecreaseInterval;

    [SerializeField] private float hydrationDecreasePerSecond = 5f;
    public float HydrationDecreasePerSecond =>  hydrationDecreasePerSecond;

    [Header("Misc")]
    [SerializeField] private int balance = 0;
    public int Balance
    {
        get => balance;
        set => balance = value;
    }

    [SerializeField] private int sharkDebt = 0;
    public int SharkDebt
    {
        get => sharkDebt;
        set => sharkDebt = value;
    }

    [SerializeField] private int sharkDebtWeeks = 0;
    public int SharkDebtWeeks
    {
        get => sharkDebtWeeks;
        set => sharkDebtWeeks = value;
    }

    [SerializeField] private int bankDebt = 100000;
    public int BankDebt
    {
        get => bankDebt;
        set => bankDebt = value;
    }


}

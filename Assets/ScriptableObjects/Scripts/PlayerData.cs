using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    // Movement
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

    [SerializeField] private float maxHunger = 10f;
    public float MaxHunger => maxHunger;

    [SerializeField] private float currentHunger = 10f;
    public float CurrentHunger
    {
        get => currentHunger;
        set => currentHunger = value;
    }

    [SerializeField] private float maxHydration = 10f;
    public float MaxHydration => maxHydration;

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

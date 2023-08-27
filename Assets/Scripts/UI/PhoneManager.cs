using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PhoneManager : MonoBehaviour
{
    [SerializeField] TMP_Text weekText;
    [SerializeField] TMP_Text balanceText;
    [SerializeField] TMP_Text bankDebtText;
    [SerializeField] TMP_Text sharkDebtText;

    [SerializeField] Animator animator;

    [SerializeField] WorldClockData worldClockData;
    [SerializeField] WorldClockManager worldClockManager;
    [SerializeField] PlayerData playerData;

    private void Start()
    {
        UpdateBalanceAndDebtText();
    }

    public void UpdateWeekText()
    {
        weekText.text = "Week " + worldClockData.currentWeek.ToString();
        Debug.Log("Updated week text");
    }

    public void UpdateBalanceAndDebtText()
    {
        balanceText.text = "Balance: " + playerData.Balance.ToString();
        bankDebtText.text = "Debt: " + playerData.BankDebt.ToString();
    }

    public void UpdateSharkDebtText()
    {
        sharkDebtText.text = "I owe Diego (Shark Guy): $" + playerData.SharkDebt.ToString();
    }

    public void TurnOnPhone()
    {
        UpdateSharkDebtText();
        UpdateWeekText();
        animator.SetBool("PhoneClicked", true);
    }

    public void TurnOffPhone()
    {
        animator.SetBool("PhoneClicked", false);
    }
}

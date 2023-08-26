using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BankDebtManager : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    [SerializeField] private GameObject sufficientPanel;
    [SerializeField] private GameObject insufficientPanel;
    [SerializeField] PhoneManager phoneManager;

    private void Start()
    {
        //StartCoroutine(InsufficientBalance());
    }
    
    public void ReturnOneThousand()
    {
        if (ReturnDebtCheck(1000))
        {
            RemoveDebt(1000);
            phoneManager.UpdateBalanceAndDebtText();
            StartCoroutine(SufficientBalance());
        }
        else
        {
            StartCoroutine(InsufficientBalance());
        }
    }

    public void ReturnTenThousand()
    {
        if (ReturnDebtCheck(5000))
        {
            RemoveDebt(5000);
            phoneManager.UpdateBalanceAndDebtText();
            StartCoroutine(SufficientBalance());
        }
        else
        {
            StartCoroutine(InsufficientBalance());
        }
    }

    public void ReturnAll()
    {
        if (ReturnDebtCheck(playerData.BankDebt))
        {
            RemoveDebt(playerData.BankDebt);
            phoneManager.UpdateBalanceAndDebtText();
            StartCoroutine(SufficientBalance());
        }
        else
        {
            StartCoroutine(InsufficientBalance());
        }
    }

    // If the player owes money
    public bool DebtCheck()
    {
        if (playerData.SharkDebt > 0)
        return true;
        else
        return false;
    }

    // If the player has enough money
    private bool ReturnDebtCheck(int debtToPay)
    {
        if (playerData.Balance >= debtToPay)
        return true;
        else
        return false;
    }

    // Removing the amount of debt and making sure the debt is not negative
    private void RemoveDebt(int debtToRemove)
    {
        playerData.Balance -= debtToRemove;
        playerData.BankDebt -= debtToRemove;

        if (playerData.BankDebt < 0)
        playerData.BankDebt = 0;
    }

    public IEnumerator InsufficientBalance()
    {
        insufficientPanel.SetActive(true);
        yield return new WaitForSeconds(1);
        insufficientPanel.SetActive(false);
    }

    public IEnumerator SufficientBalance()
    {
        sufficientPanel.SetActive(true);
        yield return new WaitForSeconds(1);
        sufficientPanel.SetActive(false);
    }
}

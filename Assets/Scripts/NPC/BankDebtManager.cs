using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankDebtManager : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    [SerializeField] public GameObject insufficientPanel;


    private void Start()
    {
        StartCoroutine(InsufficientBalance());
    }
    
    public void ReturnOneThousand()
    {
        if (ReturnDebtCheck(1000))
        {
            RemoveDebt(1000);
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
        yield return new WaitForSeconds(2);
        insufficientPanel.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebtManager : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    [SerializeField] private NPC nPC;

    private GameObject notificationManagerGO;
    private NotificationManager notificationManager;

    IEnumerator Start()
    {
        yield return null;
        notificationManagerGO = GameObject.FindGameObjectWithTag("NotificationManager");
        notificationManager = notificationManagerGO.GetComponent<NotificationManager>();

        yield return null;
        notificationManagerGO.SetActive(false);

        if (notificationManager == null)
        {
            Debug.Log("Not Found");
        }
    }

    void Update()
    {
       
    }

    public void BorrowOneThousand()
    {
        if (DebtCheck())
        {
            StartCoroutine(notificationManager.ShowNotification("SharkExistingDebt"));
            nPC.DisableChoice();
        }
        else
        {
            playerData.Balance += 1000;
            playerData.SharkDebt += 1000;
        }
    }

    public void BorrowFiveThousand()
    {
        if (DebtCheck())
        {
            StartCoroutine(notificationManager.ShowNotification("SharkExistingDebt"));
            nPC.DisableChoice();
        }
        else
        {
            playerData.Balance += 5000;
            playerData.SharkDebt += 5000;
        }
    }

    public void BorrowTenThousand()
    {
        if (DebtCheck())
        {
            StartCoroutine(notificationManager.ShowNotification("SharkExistingDebt"));
            nPC.DisableChoice();
        }
        else
        {
            playerData.Balance += 10000;
            playerData.SharkDebt += 10000;
        }
    }

    public void ReturnOneThousand()
    {
        if (ReturnDebtCheck(1000))
        {
            RemoveDebt(1000);
        }
        else
        {
            StartCoroutine(notificationManager.ShowNotification("SharkInsufficientMoney"));
            nPC.DisableChoice();
        }
    }

    public void ReturnFiveThousand()
    {
        if (ReturnDebtCheck(5000))
        {
            RemoveDebt(5000);
        }
        else
        {
            StartCoroutine(notificationManager.ShowNotification("SharkInsufficientMoney"));
            nPC.DisableChoice();
        }
    }

    public void ReturnAll()
    {
        if (ReturnDebtCheck(playerData.SharkDebt))
        {
            RemoveDebt(playerData.SharkDebt);
        }
        else
        {
            StartCoroutine(notificationManager.ShowNotification("SharkInsufficientMoney"));
            nPC.DisableChoice();
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
        playerData.SharkDebt -= debtToRemove;

        if (playerData.SharkDebt < 0)
        playerData.SharkDebt = 0;
    }
}

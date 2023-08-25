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
        // If the player owes money
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
        // If the player owes money
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
        // If the player owes money
        if (DebtCheck())
        {
            notificationManager.ShowNotification("SharkExistingDebt");
            nPC.DisableChoice();
        }
        else
        {
            playerData.Balance += 10000;
            playerData.SharkDebt += 10000;
        }
    }

    public bool DebtCheck()
    {
        if (playerData.SharkDebt > 0)
        return true;
        else
        return false;
    }
}

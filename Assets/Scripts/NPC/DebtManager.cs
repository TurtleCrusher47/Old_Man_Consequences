using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebtManager : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    [SerializeField] private NPC nPC;

    public void BorrowOneThousand()
    {
        // If the player owes money
        if (DebtCheck())
        {
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
            nPC.DisableChoice();
        }
        else
        {
            playerData.Balance += 10000;
            playerData.SharkDebt += 10000;
        }

    }

    // Whether the player owes money or not
    public bool DebtCheck()
    {
        if (playerData.SharkDebt > 0)
        return true;
        else
        return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebtManager : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    public void BorrowOneThousand()
    {
        playerData.Balance += 1000;
        playerData.SharkDebt += 1000;
    }

    public void BorrowFiveThousand()
    {
        playerData.Balance += 5000;
        playerData.SharkDebt += 5000;
    }

    public void BorrowTenThousand()
    {
        playerData.Balance += 10000;
        playerData.SharkDebt += 10000;
    }
}

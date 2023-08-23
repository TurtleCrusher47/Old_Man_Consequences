using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    PlayerData playerData;
    DebtManager debtManager;

    // Constant checks
    public bool StaminaCheck()
    {
        if (playerData.CurrentStamina <= 0)
        return true;
        else
        return false;
    }

    public bool ThirstCheck()
    {
        if (playerData.CurrentHydration <= 0)
        return true;
        else
        return false;
    }
}

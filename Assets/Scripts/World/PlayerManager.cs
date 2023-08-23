using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] WorldClockManager worldClockManager;

    // Weekly checks
    public bool SharkCheck()
    {
        if (playerData.SharkDebt <= 0)
        return true;
        else
        return false;
    }

    public void PlayerFaintStamina()
    {
        worldClockManager.FaintNextDay();
    }

    public void PlayerFaintHydration()
    {
        worldClockManager.NextWeek();
    }
}

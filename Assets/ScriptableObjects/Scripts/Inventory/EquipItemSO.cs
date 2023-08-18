using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EquipItemSO : ItemSO, IDestroyableItem, IItemAction
{
    
    public string ActionName => "Equip";

    public bool PerformAction(GameObject character, List<ItemParameter> itemState)
    {
        AgentWeapon weaponSystem = character.GetComponent<AgentWeapon>();
        if (weaponSystem != null)
        {
            weaponSystem.SetWeapon(this, itemState == null ?
                DefaultParameterList : itemState);
            return true;
        }
        return false;
    }
}

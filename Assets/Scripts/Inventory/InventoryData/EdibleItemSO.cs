using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

[CreateAssetMenu]
public class EdibleItemSO : PurchaseableItemSO, IDestroyableItem, IItemAction
{
<<<<<<< Updated upstream:Assets/Scripts/Inventory/InventoryData/EdibleItemSO.cs

    [SerializeField]
    private List<ModifierData> modifiersData = new List<ModifierData>();
=======
    
    [SerializeField] private List<ModifierData> modifiersData = new List<ModifierData>();
>>>>>>> Stashed changes:Assets/ScriptableObjects/Scripts/Inventory/EdibleItemSO.cs
    public string ActionName => "Consume";

    public bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
    {
        foreach (ModifierData data in modifiersData)
        {
            data.statModifier.AffectCharacter(character, data.value);
        }
        return true;
    }
}

public interface IDestroyableItem
{

}

public interface IItemAction
{
    public string ActionName { get; }
    bool PerformAction(GameObject character, List<ItemParameter> itemState);

}

[Serializable]
public class ModifierData
{
    public CharacterStatModifierSO statModifier;
    public float value;
}




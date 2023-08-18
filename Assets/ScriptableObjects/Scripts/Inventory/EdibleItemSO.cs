using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

using UnityEngine.EventSystems;

[CreateAssetMenu]
public class EdibleItemSO : ItemSO, IDestroyableItem, IItemAction
{
    [SerializeField] private int purchasePrice;
    public int PurchasePrice 
    {
        get => purchasePrice;

        set => purchasePrice = value;
    }
    
    [SerializeField] private List<ModifierData> modifiersData = new List<ModifierData>();
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




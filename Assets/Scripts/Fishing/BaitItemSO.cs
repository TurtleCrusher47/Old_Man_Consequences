using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BaitItemSO : PurchaseableItemSO, IItemAction
{
    [SerializeField] private List<ModifierData> modifierData = new List<ModifierData>();
    public string ActionName => "Use";

    //addAction to item in inventoryController wahoo
    public bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
    {
        foreach (ModifierData data in modifierData)
        {
            data.statModifier.AffectCharacter(character, data.value);
        }
        return true;
    }
    // Where does the bait sit on the bait flavour scale?
    [field: SerializeField] private Vector2 flavourProfile;
    // X value: crunchy or chewy, Y value: sweet or salty
    public Vector2 FlavourProfile
    {
        get => flavourProfile;
        set => flavourProfile = value;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseableItemSO : ItemSO
{
   [field: SerializeField] private int purchasePrice;

    public int PurchasePrice 
    {
        get => purchasePrice;

        set => purchasePrice = value;
    }
}

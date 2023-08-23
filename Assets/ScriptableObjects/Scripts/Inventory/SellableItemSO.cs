using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

using UnityEngine.EventSystems;

[CreateAssetMenu]

public class SellableItemSO : ItemSO
{

   [SerializeField] private int sellPrice;

   public int SellPrice 
   {
        get => sellPrice;

        set => sellPrice = value;
   }
    
}

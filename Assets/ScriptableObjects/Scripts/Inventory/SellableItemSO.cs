using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

using UnityEngine.EventSystems;

[CreateAssetMenu]

//maybe add the IItemAction interface? to create a sell button and perform a 'sell' action
//but gotta make it toggleable so that button only appears in the shop. 
//this class is basically just used to make fish lol 
public class SellableItemSO : ItemSO, IDestroyableItem
{

   [SerializeField] private int sellPrice;

   public int SellPrice 
   {
        get => sellPrice;

        set => sellPrice = value;
   }
    
}

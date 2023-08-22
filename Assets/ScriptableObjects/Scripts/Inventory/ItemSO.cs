using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemSO : ScriptableObject
{
    [field: SerializeField] private bool canStack;
    public bool CanStack
    {
        get => canStack;
        set => canStack = value;
    }

    public int ID => GetInstanceID();

    [field: SerializeField] private int maxStackSize;
    public int MaxStackSize
    { 
        get => maxStackSize;
        set => maxStackSize = value; 
    }

    [field: SerializeField] private string name;
    public string Name
    {
        get => name;
        set => name = value;
    }

    [field: SerializeField] [field: TextArea] private string description;
    public string Description
    {
        get => description;
        set => description = value;
    }

    [field: SerializeField] private Sprite itemImage;
    public Sprite ItemImage
    {
        get => itemImage;
        set => itemImage = value;
    }

    [field: SerializeField] private List<ItemParameter> defaultParameterList;
    public List<ItemParameter> DefaultParameterList
    {
        get => defaultParameterList;
        set => defaultParameterList = value;
    }


}

[Serializable]

public struct ItemParameter : IEquatable<ItemParameter>
{
    public ItemParameterSO itemParameter;
    public float value; 

    public bool Equals(ItemParameter other)
    {
        return other.itemParameter == itemParameter;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishEnums;

[CreateAssetMenu]
public class FishItemSO : SellableItemSO, IDestroyableItem
{
    // Where does the fish sit on the bait flavour scale?
    [field: SerializeField] private Vector2 flavourPrefScale;
    // X value: crunchy or chewy, Y value: sweet or salty
    public Vector2 FlavourPrefScale
    {
        get => flavourPrefScale;
        set => flavourPrefScale = value;
    }
    // How much more or less do you get when you sell the fish?
    [field: SerializeField] private float[] sizeMultiplier;
    public float[] SizeMultiplier
    {
        get => sizeMultiplier;
    }
    // What are the chances of this fish spawning?
    [field: SerializeField] private float spawnChance;
    public float SpawnChance
    {
        get => spawnChance;
    }
}

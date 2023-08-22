using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BaitItemSO : SellableItemSO
{
    // Where does the bait sit on the bait flavour scale?
    [field: SerializeField] private Vector2 flavourProfile;
    // X value: crunchy or chewy, Y value: sweet or salty
    public Vector2 FlavourProfile
    {
        get => flavourProfile;
        set => flavourProfile = value;
    }

}

using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

[CreateAssetMenu]
public class Notification : ScriptableObject
{
    [SerializeField] private Sprite sprite;
    public Sprite Sprite => sprite;

    [SerializeField] private string message;
    public string Message => message;
}

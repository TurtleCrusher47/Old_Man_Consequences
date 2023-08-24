using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu]
public class WorldClockData : ScriptableObject
{
    [Header("Time Settings")]
    public float secondsPerMinute = 6.0f; // Adjust this to control time speed

    public int currentDay = 1;
    public int currentDayIndex = 1; // Index of the current day
    public string[] daysOfWeek = { "Sun.", "Mon.", "Tue.", "Wed.", "Thu.", "Fri.", "Sat." }; // Array of day names

    public int hours = 7; // Starting hour (7 AM)
    public int minutes = 0;
}

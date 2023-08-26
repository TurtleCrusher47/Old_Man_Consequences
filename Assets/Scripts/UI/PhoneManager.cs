using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PhoneManager : MonoBehaviour
{
    [SerializeField] TMP_Text weekText;
    [SerializeField] Animator animator;

    [SerializeField] WorldClockData worldClockData;

    public void UpdateWeekText()
    {
        weekText.text = "Week " + worldClockData.currentWeek.ToString();
    }

    public void TurnOnPhone()
    {
        animator.SetBool("PhoneClicked", true);
    }

    public void TurnOffPhone()
    {
        animator.SetBool("PhoneClicked", false);
    }
}

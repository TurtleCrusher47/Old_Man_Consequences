using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* 

HOW TO USE THIS CLASS:
-Create a ItemActionPanel in Canvas
-Add this script + content size fitter to panel
-content size fitter set to min/min


WHAT THIS CLASS DOES:
-Creates buttons for eat, drop + other interactions with objects
-Toggles them on/off when player right clicks on the inventory slot
*/

public class ItemActionPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonPrefab;

    public void AddButon(string name, Action onClickAction)
    {
        GameObject button = Instantiate(buttonPrefab, transform);
        button.GetComponent<Button>().onClick.AddListener(() => onClickAction());
        button.GetComponentInChildren<TMPro.TMP_Text>().text = name;
    }

    public void Toggle(bool val)
    {
        if (val == true)
            RemoveOldButtons();
        gameObject.SetActive(val);
    }

    public void RemoveOldButtons()
    {
        foreach (Transform transformChildObjects in transform)
        {
            Destroy(transformChildObjects.gameObject);
        }
    }
}

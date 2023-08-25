using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanelManager : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e"))
        shopPanel.SetActive(!shopPanel.activeSelf);
    }
}

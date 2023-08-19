using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] PlayerData playerData;

    public TMP_Text balance;
    // public PurchaseableItemSO[] purchaseableItemSO;
    public GameObject[] shopPanelGO;
    public ShopTemplate[] shopPanel;
    public Button[] purchaseButtons;
    
    // Start is called before the first frame update
    void Start()
    {
        // for (int i = 0; i < purchaseableItemSO.Length; i ++)
        // {
        //     shopPanelGO[i].SetActive(true);
        // }

        UpdateBalance();
        LoadPanels();
        CheckPurchaseable();
    }

    // Call on start and on sell
    public void CheckPurchaseable()
    {
        // for (int i = 0; i < purchaseableItemSO.Length; i ++)
        // {
        //     if (playerData.Balance >= purchaseableItemSO[i].price)
        //     {
        //         purchaseButton[i].interactable = true;
        //     }
        //     else
        //     {
        //         purchaseButton[i].interactable = false;
        //     }
        // }
    }

    public void PurchaseItem(int buttonNumber)
    {
        // if (playerData.Balance >= purchaseableItemSO[buttonNumber].price)
        // {
        //     playerData.Balance -= purchaseableItemSO[buttonNumber].price;
        //     UpdateBalance();
        //     CheckPurchaseable();
        // }
    }

    public void LoadPanels()
    {
        // for (int i = 0; i < purchaseableItemSO.Length; i ++)
        // {
        //     shopPanel[i].name.text = purchasableItemSO[i].name;
        //     shopPanel[i].description.text = purchasableItemSO[i].description;
        //     shopPanel[i].price.text = "$" + purchasableItemSO[i].cost.ToString();
        // }
    }

    public void UpdateBalance()
    {
        balance.text = "$" + playerData.Balance.ToString();
    }
}

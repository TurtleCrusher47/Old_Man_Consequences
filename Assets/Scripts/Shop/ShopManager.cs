using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] InventorySO inventoryData;

    public TMP_Text balance;
    public PurchaseableItemSO[] purchaseableItemSO;
    public GameObject[] shopPanelGO;
    public ShopTemplate[] shopPanel;
    public Button[] purchaseButtons;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < purchaseableItemSO.Length; i ++)
        {
            shopPanelGO[i].SetActive(true);
        }

        LoadPanels();
        UpdateBalance();
        CheckPurchaseable();
    }

    // Call on start and on sell
    public void CheckPurchaseable()
    {
        for (int i = 0; i < purchaseableItemSO.Length; i ++)
        {
            if (playerData.Balance >= purchaseableItemSO[i].PurchasePrice)
            {
                purchaseButtons[i].interactable = true;
            }
            else
            {
                purchaseButtons[i].interactable = false;
            }
        }
    }

    public void PurchaseItem(int buttonNumber)
    {
        if (playerData.Balance >= purchaseableItemSO[buttonNumber].PurchasePrice)
        {
            playerData.Balance -= purchaseableItemSO[buttonNumber].PurchasePrice;

            inventoryData.AddItem(purchaseableItemSO[buttonNumber], 1);

            //add this code back when we add the inventory to the scene
            //inventoryUI is an InventoryPage instance that should be serialized
            // foreach (var item in inventoryData.GetCurrentInventoryState())
            // {
            //     inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
            // }
            UpdateBalance();
            CheckPurchaseable();
        }
    }

    public void LoadPanels()
    {
        for (int i = 0; i < purchaseableItemSO.Length; i++)
        {
            shopPanel[i].name.text = purchaseableItemSO[i].Name;
            shopPanel[i].price.text = "$" + purchaseableItemSO[i].PurchasePrice.ToString();
            shopPanel[i].sprite.sprite = purchaseableItemSO[i].ItemImage;
            shopPanel[i].hoverTip.tipToShow = purchaseableItemSO[i].Description;
        }
    }

    public void UpdateBalance()
    {
        balance.text = "$" + playerData.Balance.ToString();
    }
}

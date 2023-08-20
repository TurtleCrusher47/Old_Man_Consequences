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
    public GameObject[] buyShopPanelGO;
    public ShopTemplate[] buyShopPanel;
    public Button[] purchaseButtons;

    public SellableItemSO[] sellableItemSO;
    public GameObject[] sellShopPanelGO;
    public ShopTemplate[] sellShopPanel;
    public Button[] sellButtons;

    void Awake()
    {
        LoadInventoryItems();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < purchaseableItemSO.Length; i ++)
        {
            buyShopPanelGO[i].SetActive(true);
        }

        for (int i = 0; i < sellableItemSO.Length; i ++)
        {
            sellShopPanelGO[i].SetActive(true);
        }

        LoadPurchaseablePanels();
        LoadSellablePanels();
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

    public void CheckSellable()
    {
        // Check whether the player still has any of the items left

        // else
        // {
        //     sellableButtons[i].interactable = false;
        // }
    }

    // Purchase the item
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

    // Sell the item
    public void SellItem(int buttonNumber)
    {
        // For Tania: delete the item, add to player's balance
        UpdateBalance();
        CheckSellable();
    }

    public void SellAllItems()
    {
        // For each sellable item in the inventory, remove it from inventory and add to player balance
        
        UpdateBalance();
        CheckSellable();
    }

    // Load a panel for each of the purchasable items
    public void LoadPurchaseablePanels()
    {
        for (int i = 0; i < purchaseableItemSO.Length; i++)
        {
            buyShopPanel[i].name.text = purchaseableItemSO[i].Name;
            buyShopPanel[i].price.text = "$" + purchaseableItemSO[i].PurchasePrice.ToString();
            buyShopPanel[i].sprite.sprite = purchaseableItemSO[i].ItemImage;
            buyShopPanel[i].hoverTip.tipToShow = purchaseableItemSO[i].Description;
        }
    }

    // Loop through the player's inventory and add the fish to the list of sellable items
    public void LoadInventoryItems()
    {
        // For Tania: loop through the inventory, check if the item is sellable, if it is, add it to the list

        // for (int i = 0; i < inventoryList.Length; i++)
        // {

        // }
    }

    // Load a panel for each of the sellable items
    public void LoadSellablePanels()
    {
        for (int i = 0; i < sellableItemSO.Length; i++)
        {
            sellShopPanel[i].name.text = sellableItemSO[i].Name;
            sellShopPanel[i].price.text = "$" + sellableItemSO[i].SellPrice.ToString();
            sellShopPanel[i].sprite.sprite = sellableItemSO[i].ItemImage;
            sellShopPanel[i].hoverTip.tipToShow = sellableItemSO[i].Description;
        }
    }

    public void UpdateBalance()
    {
        balance.text = "$" + playerData.Balance.ToString();
    }
}

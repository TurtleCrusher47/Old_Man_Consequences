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

    private int existingSellableItems = 0;
     
    //private Dictionary<int, InventoryItemStruct> inventoryList;

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

        for (int i = 0; i < existingSellableItems; i++)
        {
            if (sellableItemSO[i] != null)
            {
                sellShopPanelGO[i].SetActive(true);
            }
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

    // public void CheckSellable()
    // {
    //     // Check whether the player still has any of the items left

    //     //loop through the list of sellable items
    //     //find if the correspondng slot in the inventory has a quantity > 0
    //     //if > 0, button that corresponds to item being checked set active
    //     //else, button that corresponds set to inactive

    //     for (int i = 0; i < sellableItemSO.Length; i++)
    //     {
    //         for (int j = 0; j < inventoryData.InventoryItems.Count; j++)
    //         {
    //             // if (inventoryData.InventoryItems[j].item == sellableItemSO[i] && inventoryData.InventoryItems[j].quantity > 0)
    //             // {
    //             //     sellButtons[i].interactable = true;
    //             // }

    //             //  else
    //             // {
    //             //     Debug.Log("Sell Button" + i + "inactive");   
    //             //     sellButtons[i].interactable = false;
    //             // }
    //         }
    //     }
        
    // }

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

        playerData.Balance += sellableItemSO[buttonNumber].SellPrice;

        int itemIndex = -1;

        for (int i = 0; i < inventoryData.Size; i++)
        {
            if (inventoryData.InventoryItems[i].item == sellableItemSO[buttonNumber])
            {
                itemIndex = i;
                break;
            }
        }

        if (itemIndex > -1)
        {
             if (inventoryData.InventoryItems[itemIndex].quantity > 1)
            {
                int oldQuantity = inventoryData.InventoryItems[itemIndex].quantity;
                inventoryData.InventoryItems[itemIndex].ChangeQuantity(oldQuantity - 1);
            }

            else
            {
                InventoryItemStruct emptyItemStruct = InventoryItemStruct.GetEmptyItem();
                inventoryData.InventoryItems[itemIndex] = emptyItemStruct;
                sellButtons[buttonNumber].interactable = false;
            }

            inventoryData.RemoveItem(itemIndex, 1);
            
            //UpdateInventoryList();
            UpdateBalance();
            CheckPurchaseable();
            //CheckSellable();
        }

    }

    public void SellAllItems()
    {
        // For each sellable item in the inventory, remove it from inventory and add to player balance

        //UpdateInventoryList();
        UpdateBalance();
        //CheckSellable();
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
        //inventoryList = inventoryData.GetCurrentInventoryState();
        int j = 0;
        for (int i = 0; i < inventoryData.InventoryItems.Count; i++)
        {
            if (inventoryData.InventoryItems[i].item is SellableItemSO)
            {
                if (j < sellableItemSO.Length)
                {
                    sellableItemSO[j] = inventoryData.InventoryItems[i].item as SellableItemSO; 

                    existingSellableItems++;

                    j++;
                }
            }
        }
    }

    // Load a panel for each of the sellable items
    public void LoadSellablePanels()
    {
        for (int i = 0; i < existingSellableItems; i++)
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

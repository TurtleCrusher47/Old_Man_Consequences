using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GeneralStoreManager : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] InventorySO inventoryData;

    public TMP_Text balance;
    
    public PurchaseableItemSO[] purchaseableItemSO;
    public GameObject[] buyShopPanelGO;
    public ShopTemplate[] buyShopPanel;
    public Button[] purchaseButtons;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < purchaseableItemSO.Length; i ++)
        {
            buyShopPanelGO[i].SetActive(true);
        }

        LoadPurchaseablePanels();
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

    public void UpdateBalance()
    {
        balance.text = "$" + playerData.Balance.ToString();
    }

}

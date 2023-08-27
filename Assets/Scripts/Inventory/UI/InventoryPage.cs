using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPage : MonoBehaviour
{
    [SerializeField]
    private InventoryItem itemPrefab;

    [SerializeField]
    private RectTransform contentPanel;

    [SerializeField]
    private InventoryDescrip itemDescrip;

    [SerializeField]
    private MouseFollower mouseFollower;

    [SerializeField]
    private ItemActionPanel actionPanel;

    List<InventoryItem> listOFUIItems = new List<InventoryItem>();

    private int currDraggedItemIndex = -1;

    public event Action<int> OnDescripRequested, OnItemActionRequested, OnStartDragging;
    public event Action<int, int> OnSwapItems;

    private void Awake()
    {
        Hide();
        mouseFollower.Toggle(false);
        itemDescrip.ResetDescription();
    }
    public void InitInventoryUI(int inventorySize)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            InventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);
            listOFUIItems.Add(uiItem);
            uiItem.OnItemClicked += HandleItemSelection;
            uiItem.OnItemBeginDrag += HandleBeginDrag;
            uiItem.OnItemDroppedOn += HandleSwap;
            uiItem.OnItemEndDrag += HandleEndDrag;
            uiItem.OnRightMouseBtnClick += HandleShowItemActions;
        }

    }

    internal void ResetAllItems()
    {
        //Debug.Log("run reset");
        foreach (var item in listOFUIItems)
        {
            item.ResetData();
            item.Deselect();
        }
    }

    internal void UpdateDescrip(int itemIndex, Sprite itemImage, string name, string descrip)
    {
        itemDescrip.SetDescrip(itemImage, name, descrip);
        DeselectAllItems();
        listOFUIItems[itemIndex].Select();
    }

    public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
    {
        if (listOFUIItems.Count > itemIndex)
        {
            listOFUIItems[itemIndex].SetData(itemImage, itemQuantity);
        }
    }
    private void HandleShowItemActions(InventoryItem inventoryItem)
    {
        int index = listOFUIItems.IndexOf(inventoryItem);
        if (index == -1)
        {
            return;
        }

        OnItemActionRequested?.Invoke(index);
    }

    private void HandleEndDrag(InventoryItem inventoryItem)
    {
        ResetDraggedItem();
    }

    private void HandleSwap(InventoryItem inventoryItem)
    {
        int index = listOFUIItems.IndexOf(inventoryItem);
        if (index == -1)
        {
            return;
        }

        OnSwapItems?.Invoke(currDraggedItemIndex, index);
        HandleItemSelection(inventoryItem);

    }

    private void ResetDraggedItem()
    {
        mouseFollower.Toggle(false);
        currDraggedItemIndex = -1;
    }

    private void HandleBeginDrag(InventoryItem inventoryItem)
    {
        int index = listOFUIItems.IndexOf(inventoryItem);
        if (index == -1)
            return;
        currDraggedItemIndex = index;
        HandleItemSelection(inventoryItem);
        OnStartDragging?.Invoke(index);

    }

    public void CreateDraggedItem(Sprite sprite, int quantity)
    {
        mouseFollower.Toggle(true);
        mouseFollower.SetData(sprite, quantity);
    }
    private void HandleItemSelection(InventoryItem inventoryItem)
    {
        int index = listOFUIItems.IndexOf(inventoryItem);
        if (index == -1)
            return;
        OnDescripRequested?.Invoke(index);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        ResetSelection();
    }

    public void ResetSelection()
    {
        itemDescrip.ResetDescription();
        DeselectAllItems();
    }
    public void AddAction(string actionName, Action performAction)
    {
        actionPanel.AddButon(actionName, performAction);
    }

    public void ShowItemAction(int itemIndex)
    {
        actionPanel.Toggle(true);
        actionPanel.transform.position = listOFUIItems[itemIndex].transform.position;
    }

    private void DeselectAllItems()
    {
        foreach (InventoryItem item in listOFUIItems)
        {
            item.Deselect();
        }
        actionPanel.Toggle(false);
    }

    public void Hide()
    {
        actionPanel.Toggle(false);
        gameObject.SetActive(false);
        ResetDraggedItem();
    }
}

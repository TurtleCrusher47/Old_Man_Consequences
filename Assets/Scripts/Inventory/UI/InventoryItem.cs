using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDropHandler, IDragHandler
{
    [SerializeField]
    private Image itemImage;

    [SerializeField]
    private Image borderImage;

    [SerializeField]
    private TMP_Text quantityText;

    public event Action<InventoryItem> OnItemClicked, OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag, OnRightMouseBtnClick;

    private bool empty = true;

    public void Awake()
    {
        ResetData();
        Deselect();
    }

    public void ResetData()
    {   
        if (this.itemImage != null)
        {
            this.itemImage.gameObject.SetActive(false);
            empty = true;  
        }
        
    }

    public void Deselect()
    {
        if (this.borderImage != null)
        {
            borderImage.enabled = false;
        }
    }

    public void SetData(Sprite sprite, int quantity)
    {
        if (this.itemImage != null)
        {
            this.itemImage.gameObject.SetActive(true);
            this.itemImage.sprite = sprite;

        }
        
        this.quantityText.text = quantity + "";
        empty = false;
    }

    public void Select()
    {
        borderImage.enabled = true;
    }

    public void OnPointerClick(PointerEventData pointerData)
    {

        if (pointerData.button == PointerEventData.InputButton.Right)
        {
            OnRightMouseBtnClick?.Invoke(this);
        }
        else
        {
            OnItemClicked?.Invoke(this);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (empty)
            return;
        OnItemBeginDrag?.Invoke(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnItemEndDrag?.Invoke(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        OnItemDroppedOn?.Invoke(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
       
    }
}

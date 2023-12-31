using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HoverTipManager : MonoBehaviour
{
    public TextMeshProUGUI tipText;
    public RectTransform tipWindow;

    public static Action<string, Vector2> OnMouseHover;
    public static Action OnMouseExit;

    private void OnEnable()
    {
        OnMouseHover += ShowTip;
        OnMouseExit += HideTip;
    }

    private void OnDisable()
    {
        OnMouseHover -= ShowTip;
        OnMouseExit -= HideTip; 
    }

    // Start is called before the first frame update
    void Start()
    {
        HideTip();
    }

    private void ShowTip(string tip, Vector2 mousePosition)
    {
        tipText.text = tip;
        tipWindow.sizeDelta = new Vector2(tipText.preferredWidth > 800 ? 800 : tipText.preferredWidth + 100, tipText.preferredHeight + 150);
        tipWindow.transform.position = new Vector2(mousePosition.x + (tipWindow.sizeDelta.x * 0.5f), mousePosition.y);
        tipWindow.gameObject.SetActive(true);
    }

    private void HideTip()
    {
        tipText.text = default;
        tipWindow.gameObject.SetActive(false);
    }
}

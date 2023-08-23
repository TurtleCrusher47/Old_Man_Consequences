using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DynamicTextSize : MonoBehaviour
{
    [Header("PlayerStats")]
    public PlayerData playerData;

    [Header("Money Text")]
    [SerializeField] private TMP_Text playerMoney;

    // Update is called once per frame
    void Update()
    {
        UpdateMoneyText();
    }

    private void UpdateMoneyText()
    {
        playerMoney.text = playerData.BankDebt.ToString();
        DynamicTextFontSize(playerMoney);
    }

    private void DynamicTextFontSize(TMP_Text textComponent)
    {
        float originalFontSize = textComponent.fontSize;
        TMP_TextInfo textInfo = textComponent.textInfo;

        float preferredWidth = textComponent.preferredWidth;
        float availableWidth = textComponent.rectTransform.rect.width;

        while (preferredWidth > availableWidth && textComponent.fontSize > 1)
        {
            textComponent.fontSize--;
            preferredWidth = textComponent.preferredWidth;
        }

        if (preferredWidth > availableWidth)
        {
            textComponent.fontSize = originalFontSize; // Reset to original size if necessary
        }
    }
}

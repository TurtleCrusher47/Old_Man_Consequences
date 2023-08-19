using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using Microsoft.Unity.VisualStudio.Editor;

public class ShopTemplate : MonoBehaviour
{
    public TMP_Text name;
    public HoverTip hoverTip;
    public TMP_Text price;
    public Image sprite;

    void Start()
    {
        hoverTip = gameObject.GetComponent<HoverTip>();
    }
}

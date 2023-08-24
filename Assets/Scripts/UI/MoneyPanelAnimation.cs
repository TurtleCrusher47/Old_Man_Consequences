using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPanelAnimation : MonoBehaviour
{
    private Animator panelAnimation;
    private bool isPanelVisible = false;

    // Start is called before the first frame update
    void Start()
    {
        panelAnimation = GetComponent<Animator>();
    }

    public void TogglePanelAnimation()
    {
        isPanelVisible = !isPanelVisible;
        panelAnimation.SetBool("isOpen", isPanelVisible);
    }
}

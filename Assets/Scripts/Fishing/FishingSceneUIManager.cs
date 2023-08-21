using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishingSceneUIManager : MonoBehaviour
{

    [SerializeField]
    private Slider hungerBar;
    [SerializeField]
    private Slider staminaBar;
    [SerializeField]
    private Slider thirstBar;
    //[SerializeField]
    //private PlayerData playerData;

    // Start is called before the first frame update
    void Start()
    {
        hungerBar.value = 0;
        staminaBar.value = 0;
        thirstBar.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //
    }
}

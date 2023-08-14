using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishingSceneUIManager : MonoBehaviour
{
    [SerializeField]
    private Slider _hungerBar;
    [SerializeField]
    private Slider _staminaBar;
    [SerializeField]
    private Slider _thirstBar;
    //[SerializeField]
    //private PlayerData playerData;

    // Start is called before the first frame update
    void Start()
    {
        _hungerBar.value = 0;
        _staminaBar.value = 0;
        _thirstBar.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //
    }
}

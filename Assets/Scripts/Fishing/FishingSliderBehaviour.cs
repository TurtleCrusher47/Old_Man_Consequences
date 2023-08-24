using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FishingSliderBehaviour : MonoBehaviour
{

    [SerializeField]
    private Slider staminaSlider;
    [SerializeField]
    private Slider fishCatchPercentSlider;
    [SerializeField]
    private Slider fishSpriteSlider;
    [SerializeField]
    private Slider playerRodSlider;
    [SerializeField]
    private Button addButton;
    [SerializeField]
    private Button releaseButton;

    [SerializeField]
    private TMP_Text catchText;
    [SerializeField]
    private TMP_Text percentText;

    [SerializeField]
    private Image fishingSliderCatchImage;

    [SerializeField]
    private Image caughtFishSprite;

    [SerializeField]
    private FishManager fishManager;

    private float elaspedTime;
    public float a = 0.6f;
    public bool fishCaught;

    Gradient sliderColorGrad = new Gradient();
    GradientColorKey[] sliderColor = new GradientColorKey[3];
    GradientAlphaKey[] sliderAlpha = new GradientAlphaKey[3];
    
    // Start is called before the first frame update
    void Awake()
    {
        //elaspedTime = 11.526f;
        elaspedTime = 0;
        fishCaught = false;
        sliderColor[0] = new GradientColorKey(Color.red, 0.0f);
        sliderColor[1] = new GradientColorKey(new Color(1, 1, 0), 0.5f);
        sliderColor[2] = new GradientColorKey(Color.green, 1.0f);

        sliderAlpha[0] = new GradientAlphaKey(1, 0.0f);
        sliderAlpha[1] = new GradientAlphaKey(1, 0.5f);
        sliderAlpha[2] = new GradientAlphaKey(1, 1.0f);

        sliderColorGrad.SetKeys(sliderColor, sliderAlpha);

        fishCatchPercentSlider.onValueChanged.AddListener(delegate { SetFishCatchColor(); });
        ResetSliderValues();
    }

    // Update is called once per frame
    void Update()
    {
        elaspedTime += Time.deltaTime;
        // value = Sin((1/2 * dt) * Cos(1/2*dt))
        //fishSpriteSlider.value = Mathf.Sin((0.5f * elaspedTime) * Mathf.Cos(0.5f * elaspedTime));

        // value = 1/2 * Cos(a * dt * (cos a * dt)) + 0.5f
        //fishSpriteSlider.value = 0.5f * Mathf.Cos(a * elaspedTime * (Mathf.Cos(a * elaspedTime))) + 0.5f;

        // Value = sin(dt)
        // Debug.Log(Mathf.Sin(elaspedTime));
        if (fishCatchPercentSlider.value < fishCatchPercentSlider.maxValue && staminaSlider.value > staminaSlider.minValue)
        {
            fishSpriteSlider.value = (0.5f * Mathf.Sin(0.5f * a * elaspedTime)) + 0.5f;
            staminaSlider.value -= 0.02f * Time.deltaTime;
            if (Input.GetMouseButton(0))
            {
                playerRodSlider.value += 0.5f * Time.deltaTime;
                staminaSlider.value -= 0.05f * Time.deltaTime;
            }
            else
            {
                playerRodSlider.value -= 0.5f * Time.deltaTime;
            }
            if (Mathf.Abs(playerRodSlider.value - fishSpriteSlider.value) < 0.1f)
            {
                fishCatchPercentSlider.value += Time.deltaTime * 1.5f;
            }
            else if (fishCatchPercentSlider.value > 0)
            {
                fishCatchPercentSlider.value -= 0.25f * Time.deltaTime;
            }
            if (fishCatchPercentSlider.value == fishCatchPercentSlider.maxValue)
            {
                OnFishCaught();
            }

            if (staminaSlider.value < 0.001f)
            {
                releaseButton.gameObject.SetActive(true);
                releaseButton.GetComponentInChildren<TMP_Text>().text = "Okay";
                catchText.text = "Uh oh! You ran out of energy! The fish got away.";
            }
            percentText.text = "Catch percentage: " + (int)((fishCatchPercentSlider.value / fishCatchPercentSlider.maxValue) * 100) + "%\n" +
            "Stamina left: " + (int)(staminaSlider.value * 100) + "%";
        }
        
    }
    void SetFishCatchColor()
    {
        fishingSliderCatchImage.color = sliderColorGrad.Evaluate(fishCatchPercentSlider.value / fishCatchPercentSlider.maxValue);
    }
    public void ResetSliderValues()
    {
      
        addButton.gameObject.SetActive(false);
        releaseButton.gameObject.SetActive(false);
        fishCatchPercentSlider.value = 0;
        staminaSlider.value = 1;
        elaspedTime = 0.6f;
    }
    void OnFishCaught()
    {
        FishBehaviour caughtFish = fishManager.GetCurrentCaughtFish();
        if (caughtFish != null)
        {
            fishCaught = true;
            addButton.gameObject.SetActive(true);
            releaseButton.gameObject.SetActive(true);
            catchText.text = "We caught a\n" + caughtFish.fishData.Name + " !";
            caughtFishSprite.sprite = caughtFish.fishData.ItemImage;
        }
    }
}

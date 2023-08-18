using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private float elaspedTime;
    public float a = 0.6f;
    // Start is called before the first frame update
    void Start()
    {
        elaspedTime = 11.526f;
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
        Debug.Log(Mathf.Sin(elaspedTime));
        fishSpriteSlider.value = (0.5f * Mathf.Sin(0.5f * a *elaspedTime)) + 0.5f;
        if (Input.GetMouseButton(0))
        {
            playerRodSlider.value += 0.5f * Time.deltaTime;
        }
        else
        {
            playerRodSlider.value -= 0.5f * Time.deltaTime;
        }
        if (Mathf.Abs(playerRodSlider.value - fishSpriteSlider.value) < 0.1f)
        {
            fishCatchPercentSlider.value += Time.deltaTime;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishingController : MonoBehaviour
{
    // Point where player is fishing
    [SerializeField]
    private GameObject fishingPoint;
    private SpriteRenderer sr;
    private Animator ar;
    // vertical direction
    private float dirV;
    // strength at which player is throwing the rod at that point in time
    private float fishingStrength;
    // amt of time that player has been fishinig
    private float fishingElapsedTime;
    // used for later
    private float levelDistMult;
    // base multipler for how far the rod will be thrown
    private const float baseXMultipler = 8f;
    // base multipler for how fast the rod strength changes
    private const float baseRodStrengthMult = 2f;
    private float levelStrengthMult;
    // funny bool
    private bool isCasted;
    // funny bool
    bool clicked;
    // Rod strength meter
    [SerializeField]
    private Slider slider;
    // Canvas with the crosshair
    [SerializeField]
    private Canvas fishingCrosshairCanvas;
    void Awake()
    {
        dirV = 0;
        ar = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        fishingStrength = 0;
        fishingElapsedTime = 0;
        clicked = false;
        levelDistMult = 1f;
        levelStrengthMult = 1f;
        isCasted = false;
    }
    // Update is called once per frame
    void Update()
    {
        // Show or hide the crosshair depending on if the player is clicking or not
        fishingCrosshairCanvas.enabled = (clicked || isCasted);
        // Show or hide the slider depending on if the olayer is clicking or not
        slider.enabled = clicked;
        // On player click
        if (Input.GetMouseButton(0))
        {
            if (!clicked)
                clicked = true;
        }
        else
        {
            clicked = false;
            if (fishingStrength > 0)
            {
                CastRod();
            }
        }
        if (clicked)
        {
            BuildRodStrength();
        }
        else
        {
            Walking();
        }
           

    }

    void Walking()
    {

        // Get player's vertical direction
        dirV = Input.GetAxis("Vertical");
        // Reset the crosshair if the player moves
        if (dirV != 0)
        {
            isCasted = false;
        }
        // Move based on that direction
        Vector3 newPos = transform.position + new Vector3(0, dirV, 0) * Time.deltaTime;
        // Transform the position based on the direction
        transform.position = newPos;

        UpdateSpriteDirection();
    }
    void BuildRodStrength()
    {
        // Set elapsed time for sin curve
        fishingElapsedTime += Time.deltaTime;
        // Set the fishing strength
        fishingStrength = Mathf.Abs(Mathf.Sin(baseRodStrengthMult*fishingElapsedTime)); 
        slider.value = fishingStrength;
        Vector3 newFishingPos = new Vector3(transform.position.x - (baseXMultipler * fishingStrength), transform.position.y);
        fishingCrosshairCanvas.transform.position = newFishingPos;
    }
    void CastRod()
    {
        fishingElapsedTime = 0;
        // Take rod strength
        // Calculate X position of rod based on rod strength
        Vector3 newFishingPtPos = new Vector3(transform.position.x - (baseXMultipler * fishingStrength), transform.position.y, 0);
        fishingPoint.transform.position = newFishingPtPos;
        if (!isCasted)
        {
            isCasted = true;
        }
    }
    void UpdateSpriteDirection()
    {
        // If player is facing downwards
        if (dirV < -0.001f)
        {
            ar.Play("Walk Down");
        }
        // If player is facing upwards
        else if (dirV > 0.001f)
        {
            ar.Play("Walk Up");
        }
        else
        {
            ar.Play("Idle Left");
        }
    }
}

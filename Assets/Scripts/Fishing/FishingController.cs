using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishingController : MonoBehaviour
{
    [SerializeField]
    private GameObject fishingPoint;
    private SpriteRenderer sr;
    private Animator ar;
    private float dirV;
    private float fishingStrength;
    private float fishingElapsedTime;
    private float levelDistMult;
    private const float baseXMultipler = 8f;
    private bool isCasted;
    [SerializeField]
    private Slider slider;
    bool clicked;
    void Awake()
    {
        dirV = 0;
        ar = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        fishingStrength = 0;
        fishingElapsedTime = 0;
        clicked = false;
        levelDistMult = 1f;
        isCasted = false;
    }
    // Update is called once per frame
    void Update()
    {
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
        //if (!isCasted)
        //    fishingPoint.transform.position = transform.position;

    }

    void Walking()
    {
        // Get player's vertical direction
        dirV = Input.GetAxis("Vertical");
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
        fishingStrength = Mathf.Abs(Mathf.Sin(2*fishingElapsedTime)); 
        slider.value = fishingStrength;
    }
    void CastRod()
    {
        // Take rod strength
        // Calculate X position of rod based on rod strength
        Vector3 newFishingPtPos = new Vector3(transform.position.x - (baseXMultipler * fishingStrength), 0, 0);
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

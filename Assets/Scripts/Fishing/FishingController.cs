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
        }
        if (clicked)
        {
            Fishing();
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
        // Move based on that direction
        Vector3 newPos = transform.position + new Vector3(0, dirV, 0) * Time.deltaTime;
        // Transform the position based on the direction
        transform.position = newPos;

        UpdateSpriteDirection();
    }
    void Fishing()
    {
        // Set elapsed time for sin curve
        fishingElapsedTime += Time.deltaTime;
        // Set the fishing strength
        fishingStrength = Mathf.Abs(Mathf.Sin(2*fishingElapsedTime));
        Debug.Log(fishingStrength);
        slider.value = fishingStrength;
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

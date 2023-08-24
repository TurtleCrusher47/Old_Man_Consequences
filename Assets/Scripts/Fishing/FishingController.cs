using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class FishingController : MonoBehaviour
{
    // Point where player is fishing
    public GameObject fishingPoint;
    private SpriteRenderer sr;
    private Animator ar;
    // vertical direction
    private float dirV;
    // horizontal direction
    private float dirH;
    // strength at which player is throwing the rod at that point in time
    private float fishingStrength;
    // amt of time that player has been fishinig
    private float fishingElapsedTime;
    // used for later
    private float levelDistMult;
    // base multipler for how far the rod will be thrown
    private const float baseXMultipler = 13f;
    // base multipler for how fast the rod strength changes
    private const float baseRodStrengthMult = 2f;
    private float levelStrengthMult;
    // funny bool
    public bool isCasted;
    // funny bool
    bool clicked;
    // Rod strength meter
    [SerializeField]
    private Slider slider;
    // Canvas with the crosshair
    [SerializeField]
    private Canvas fishingCrosshairCanvas;

    public bool isReeling;

    // Canvas with fishing rods
    [SerializeField]
    private Canvas fishingCanvas;

    [SerializeField] public InventoryItemStruct testItem;

    [SerializeField] private InventorySO inventoryData;

    [SerializeField] private PlayerData playerData;

    // List of sellable items
    [SerializeField]
    private List<BaitItemSO> baitItems;

    [HideInInspector]
    public List<BaitItemSO> BaitItems
    {
        get => baitItems;
    }

    [HideInInspector]
    public BaitItemSO selectedBait;

    [SerializeField]
    private GameObject theRod;
    private LineRenderer lr;

    private AudioSource audioSource;
    [SerializeField]
    private AudioClip plotSoundClip;

    bool audioPlayed;
    void Awake()
    {
        dirV = 0;
        dirH = 0;
        ar = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        fishingStrength = 0;
        fishingElapsedTime = 0;
        clicked = false;
        levelDistMult = 1f;
        levelStrengthMult = 1f;
        isCasted = false;
        isReeling = false;
        selectedBait = baitItems[0];
        lr = theRod.GetComponent<LineRenderer>();
        lr.startColor = Color.black;
        lr.endColor = Color.black;
        lr.SetPosition(0, theRod.transform.position + new Vector3(-0.4f, 0.4f, 0));
        lr.SetPosition(1, theRod.transform.position + new Vector3(-0.4f, 0.4f, 0));
        lr.startWidth = 0.05f;
        lr.endWidth = 0.05f;
        audioSource = GetComponent<AudioSource>();
        audioPlayed = false;
        audioSource.loop = false;
    }
    // Update is called once per frame
    void Update()
    {
        // Show or hide the crosshair depending on if the player is clicking or not
        fishingCrosshairCanvas.enabled = (clicked || isCasted);
        // Show or hide the slider depending on if the olayer is clicking or not
        slider.enabled = clicked;
        // On player click
        if (isReeling == false)
        {
            if (Input.GetAxis("Fire3") > 0)
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
           

    }

    void Walking()
    {
        // Get player's vertical direction
        dirV = Input.GetAxis("Vertical");
        // Get player's horizontal direction
        dirH = Input.GetAxis("Horizontal");
        // Move based on that direction
        Vector3 newPos = transform.position + new Vector3(dirH, dirV, 0) * Time.deltaTime;
        // Reset the crosshair if the player moves
        if (dirV != 0)
        {
            ResetFishingPoint();
            lr.SetPosition(0, theRod.transform.position + new Vector3(-0.4f, 0.4f, 0));
            lr.SetPosition(1, theRod.transform.position + new Vector3(-0.4f, 0.0f, 0));
        }
        if (isCasted)
        {
            lr.SetPosition(1, fishingPoint.transform.position);
        }
        // Transform the position based on the direction
        transform.position = newPos;
        // Reset the fishing point's position

        UpdateSpriteDirection();
    }
    void BuildRodStrength()
    {
        // Set elapsed time for sin curve
        fishingElapsedTime += Time.deltaTime;
        // Set the fishing strength
        fishingStrength = Mathf.Abs(Mathf.Sin(baseRodStrengthMult*fishingElapsedTime)); 
        slider.value = fishingStrength;
        Vector3 newFishingPos = new Vector3(transform.position.x - (baseXMultipler * fishingStrength) - 1, transform.position.y);
        fishingCrosshairCanvas.transform.position = newFishingPos;
        lr.SetPosition(1, fishingCrosshairCanvas.transform.position);
    }
    void CastRod()
    {
        fishingElapsedTime = 0;

        if (!audioSource.isPlaying && !audioPlayed) {
            audioSource.clip = plotSoundClip;
            audioSource.Play();
            audioPlayed = true;
        }
        //fishingStrength = 0;
        // Take rod strength
        // Calculate X position of rod based on rod strength
        Vector3 newFishingPtPos = new Vector3(transform.position.x - (baseXMultipler * fishingStrength) - 1, transform.position.y, 0);
        fishingPoint.transform.position = newFishingPtPos;
        if (!isCasted)
        {
            isCasted = true;
        }
        lr.SetPosition(1, fishingPoint.transform.position);
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
        else if (dirH > 0.001f)
        {
            ar.Play("Walk Right");
        }
        else if (dirH < -0.001f)
        {
            ar.Play("Walk Left");
        }
        else
        {
            ar.Play("Idle Left");
        }
    }
    public void ResetFishingPoint()
    {
        Vector3 newPos = transform.position + new Vector3(0, dirV, 0) * Time.deltaTime;
        fishingPoint.SetActive(false);
        fishingStrength = 0;
        fishingPoint.transform.position = newPos;
        isCasted = false;
        slider.value = 0;
        lr.SetPosition(0, theRod.transform.position + new Vector3(-0.4f, 0.4f, 0));
        lr.SetPosition(1, theRod.transform.position + new Vector3(-0.4f, 0.4f, 0));
        audioPlayed = false;
    }
    public BaitItemSO GetCurrentBait()
    {
        return selectedBait;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using FishEnums;

public class FishManager : MonoBehaviour
{
    // Prefabs to generate
    [SerializeField]
    private GameObject bigFishPrefab;
    [SerializeField]
    private GameObject medFishPrefab;
    [SerializeField]
    private GameObject smallFishPrefab;
    // List of points where fishes will flock
    [SerializeField]
    private GameObject pointsContainer;
    // List of fish
    private List<GameObject> fishList = new List<GameObject>();

    // Bool to store if fish are schooling
    public bool schooling;

    // Maximum time each fish will stay in each location
    [SerializeField]
    private float maxTimePerDest;

    // Timer which stores how long the fish have been chilling
    private float destTimer;
    // The number of fish to be spawned
    private float medFishCount;
    private float smallFishCount;
    private float bigFishCount;

    private float schoolTimer;

    [SerializeField]
    private FishingController player;

    // Inventory data
    [SerializeField]
    private InventorySO inventoryData;

    // List of sellable items
    [SerializeField]
    private List<FishItemSO> fishItems;

    private float totalWeight;
    private List<float> weightList = new List<float>();

    // Canvas with fishing rods
    [SerializeField]
    private Canvas fishingCanvas;

    
    void Start()
    {
        InitFishingSO();
        destTimer = 0;
        schoolTimer = 0;
        smallFishCount = Random.Range(2, 5);
        medFishCount = Random.Range(2, 5);
        bigFishCount = Random.Range(2, 5);
        for (int i = 0; i < smallFishCount; i++)
        {
            // Instantiate each fish
            GameObject newFish = Instantiate(smallFishPrefab, new Vector3(Random.Range(-9, 8), Random.Range(-5, 5), 0), Quaternion.identity);
            // Set each fish's parent
            newFish.GetComponent<FishBehaviour>().wayPointContainer = pointsContainer;
            newFish.GetComponent<FishBehaviour>().Init();
            newFish.transform.parent = gameObject.transform;
            newFish.GetComponent<FishBehaviour>().fishType = FishType.TYPE_SMALL;
            GenerateFishingSO(newFish.GetComponent<FishBehaviour>());
        }
        for (int i = 0; i < medFishCount; i++)
        {
            // Instantiate each fish
            GameObject newFish = Instantiate(medFishPrefab, new Vector3(Random.Range(-9, 8), Random.Range(-5, 5), 0), Quaternion.identity);
            // Set each fish's parent
            newFish.GetComponent<FishBehaviour>().wayPointContainer = pointsContainer;
            newFish.GetComponent<FishBehaviour>().Init();
            newFish.transform.parent = gameObject.transform;
            newFish.GetComponent<FishBehaviour>().fishType = FishType.TYPE_MEDIUM;
            GenerateFishingSO(newFish.GetComponent<FishBehaviour>());
        }
        for (int i = 0; i < bigFishCount; i++)
        {
            // Instantiate each fish
            GameObject newFish = Instantiate(bigFishPrefab, new Vector3(Random.Range(-9, 8), Random.Range(-5, 5), 0), Quaternion.identity);
            // Set each fish's parent
            newFish.GetComponent<FishBehaviour>().wayPointContainer = pointsContainer;
            newFish.GetComponent<FishBehaviour>().Init();
            newFish.transform.parent = gameObject.transform;
            newFish.GetComponent<FishBehaviour>().fishType = FishType.TYPE_BIG;
            GenerateFishingSO(newFish.GetComponent<FishBehaviour>());
        }
        // Set all fish's schooling status
        SetAllFishSchooling(schooling);
        foreach (Transform fish in gameObject.transform)
        {
            // Add each fish to the fishList
            fishList.Add(fish.gameObject);
            fish.GetComponent<FishBehaviour>().player = player;

        }
        if (schooling)
        {
            int newDestination = Random.Range(0, pointsContainer.transform.childCount);
            SetAllFishDestinations(newDestination);
        }
    }

    // Update is called once per frame
    void Update()
    {
        destTimer += Time.deltaTime;

        // Check if any fish has been bitten
        if (fishList.FindAll(f => f.GetComponent<FishBehaviour>().isBiting ? true : false).Count > 0 && player.isReeling == false)
        {
            SetAllFishCanBite(false);
            OnFishBite();
        }
        // Check if all fish have reached their destination
        // Searches the list and counts the number of fish that have reached their destination
        if (player.isCasted == false)
        {
            //UpdateFishState(SwimState.SWIM);
            if (fishList.FindAll(f => f.GetComponent<FishBehaviour>().destReached ? true : false).Count == fishList.Count
            || destTimer > maxTimePerDest)
            {
                SetAllFishDestinations(Random.Range(0, pointsContainer.transform.childCount));
                destTimer = 0;
            }
        }
    }

    // Fish variable setters
    // Set the schooling status of each fish
    void SetAllFishSchooling(bool isSchooling)
    {
        foreach (Transform fish in gameObject.transform)
        {

            if (fish.gameObject.activeInHierarchy)
            {
                fish.gameObject.GetComponent<FishBehaviour>().schooling = isSchooling;
                Debug.Log("Changed by SetAllFishSchool");
            }
            else
            {
                continue;
            }
        }
    }

    /// <summary>
    ///  Set the new destination index of each fish
    /// </summary>
    /// <param name="destinationIndex">The index in the waypoint list that the fish will go to next</param>
    void SetAllFishDestinations(int destinationIndex)
    {

        foreach (Transform fish in gameObject.transform)
        {
            if (fish.gameObject.activeInHierarchy)
            {
                fish.gameObject.GetComponent<FishBehaviour>().SetDestination(destinationIndex);
                fish.gameObject.GetComponent<FishBehaviour>().destReached = false;
                Debug.Log("Changed by SetAllFishDest Index");
            }
            else
            {
                continue;
            }
        }
    }

    /// <summary>
    /// Set the new destination of each fish
    /// </summary>
    /// <param name="newDest">A destination in the 3rd game world where the fish will go to</param>
    void SetAllFishDestinations(Vector3 newDest)
    {

        foreach (Transform fish in gameObject.transform)
        {
            if (fish.gameObject.activeInHierarchy)
            {
                fish.gameObject.GetComponent<FishBehaviour>().SetDestination(newDest);
                fish.gameObject.GetComponent<FishBehaviour>().destReached = false;
                Debug.Log("Changed by SetAllFishDest Vec3");
            }
            else
            {
                continue;
            }
        }
    }

    /// <summary>
    /// Set the state of all fish
    /// </summary>
    /// <param name="newState">the new state that the fish will be</param>
    void SetAllFishStates(SwimState newState)
    {
        foreach (Transform fish in gameObject.transform)
        {
            if (fish.gameObject.activeInHierarchy)
            {
                if (fish.gameObject.GetComponent<FishBehaviour>().GetState() != newState)
                {
                    fish.gameObject.GetComponent<FishBehaviour>().ChangeState(newState);
                    Debug.Log("State Updated");
                }
            }
            else
            {
                continue;
            }
        }
    }


    /// <summary>
    /// Set the "canbite" boolean of all fish. Determines if they can bite or not.
    /// </summary>
    /// <param name="canIBite">The state of "canbite" </param>
    void SetAllFishCanBite(bool canIBite)
    {

        foreach (Transform fish in gameObject.transform)
        {
            if (fish.gameObject.activeInHierarchy)
            {
                fish.gameObject.GetComponent<FishBehaviour>().canBite = canIBite;
                Debug.Log("Changed by SetAllFishCanBite");
            }
            else
            {
                continue;
            }
        }
    }

    /// Fishing-related functions
    
   
        
    /// <summary>
    /// Disable the fish that is biting on the player's fishing rod
    /// </summary>
    void DisableBitingFish(bool removeFromLst)
    {
        var fishToRemove = fishList.FindAll(f => f.GetComponent<FishBehaviour>().isBiting)[0];
        fishToRemove.SetActive(false);
        if (removeFromLst)
        {
            fishList.Remove(fishToRemove);
            fishToRemove.transform.parent = null;
            Destroy(fishToRemove);
        }

    }

    private void EnableBitingFish()
    {
        var fishToRemove = fishList.FindAll(f => f.GetComponent<FishBehaviour>().isBiting)[0];
        fishToRemove.SetActive(true);
        fishToRemove.GetComponent<FishBehaviour>().ChangeState(SwimState.SWIM);
    }

    /// <summary>
    /// Call this function when the fish bites the rod.
    /// </summary>
    void OnFishBite()
    {
        player.isReeling = true;
        if (!fishingCanvas.gameObject.activeInHierarchy)
        {
            fishingCanvas.gameObject.SetActive(true);
        }
        DisableBitingFish(false);
        SetAllFishStates(SwimState.SWIM);
    }
    /// <summary>
    /// Call this function when the player either releases the fish back into the water OR adds the fish to their inventory
    /// </summary>
    public void FinishedFishing()
    {
        Debug.Log("Canvas set inactive by finished fishing");
        fishingCanvas.gameObject.SetActive(false);
        player.isReeling = false;
        SetAllFishCanBite(true);
        player.ResetFishingPoint();
    }

    /// <summary>
    /// Initialise the fishing SO list
    /// </summary>
    void InitFishingSO()
    {
        weightList.Add(0);
        foreach (FishItemSO fishItem in fishItems)
        {
            totalWeight += fishItem.SpawnChance;
            weightList.Add(totalWeight);
        }

    }

    /// <summary>
    /// Pick a random fish SO to assign to the each fish, do this based on weight.
    /// </summary>
    void GenerateFishingSO(FishBehaviour fishBehaviour)
    {
        float randomVal = Random.Range(0, totalWeight);
        for (int i = 1; i < weightList.Count + 1; i++)
        {
            if (randomVal < weightList[i])
            {
                fishBehaviour.fishData = fishItems[i - 1];
                break;
            }
        }
    }

   /// <summary>
   /// Add the biting fish to the inventory
   /// </summary>
    public void AddToInventory()
    {
        // Find the biting fish
        FishBehaviour bitingFish = fishList.Find(f => f.GetComponent<FishBehaviour>().isBiting).GetComponent<FishBehaviour>();
        // Get its inventory data
        SellableItemSO fishItem = bitingFish.fishData;
        // Add it to inventory
        inventoryData.AddItem(fishItem, 1);
        Debug.Log("Added a "+ fishItem.Name + " to inventory! Item quantity:"  + inventoryData.InventoryItems.Find(f => fishItem).quantity);
        FinishedFishing();
        // Remove from the fishList and the fishList gameobject's child
        DisableBitingFish(true);

    }

    /// <summary>
    /// Release the biting fish back into the sea. I.e, re-enable the fish in the list.
    /// </summary>
    public void Release()
    {
        Debug.Log("Fish released!");
        
        FinishedFishing();
        EnableBitingFish();
    }
}

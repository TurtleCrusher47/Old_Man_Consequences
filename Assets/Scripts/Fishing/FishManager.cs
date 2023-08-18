using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using FishStates;

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
    [SerializeField]
    private float medFishCount = 2;
    [SerializeField]
    private float smallFishCount = 2;
    [SerializeField]
    private float bigFishCount = 2;

    private float schoolTimer;

    [SerializeField]
    private FishingController player;

    void Start()
    {
        destTimer = 0;
        schoolTimer = 0;

        for (int i = 0; i < smallFishCount; i++)
        {
            // Instantiate each fish
            GameObject newFish = Instantiate(smallFishPrefab, new Vector3(Random.Range(-9, 8), Random.Range(-5, 5), 0), Quaternion.identity);
            // Set each fish's waypoints
            newFish.GetComponent<FishBehaviour>().wayPointContainer = pointsContainer;
            // Initialise each fish
            newFish.GetComponent<FishBehaviour>().Init();
            // Set each fish's parent
            newFish.transform.parent = gameObject.transform;
        }
        for (int i = 0; i < medFishCount; i++)
        {
            // Instantiate each fish
            GameObject newFish = Instantiate(medFishPrefab, new Vector3(Random.Range(-9, 8), Random.Range(-5, 5), 0), Quaternion.identity);
            // Set each fish's waypoints
            newFish.GetComponent<FishBehaviour>().wayPointContainer = pointsContainer;
            // Initialise each fish
            newFish.GetComponent<FishBehaviour>().Init();
            // Set each fish's parent
            newFish.transform.parent = gameObject.transform;
        }
        for (int i = 0; i < bigFishCount; i++)
        {
            // Instantiate each fish
            GameObject newFish = Instantiate(bigFishPrefab, new Vector3(Random.Range(-9, 8), Random.Range(-5, 5), 0), Quaternion.identity);
            // Set each fish's waypoints
            newFish.GetComponent<FishBehaviour>().wayPointContainer = pointsContainer;
            // Initialise each fish
            newFish.GetComponent<FishBehaviour>().Init();
            // Set each fish's parent
            newFish.transform.parent = gameObject.transform;
        }
        // Set all fish's schooling status
        SetAllFishSchooling(schooling);
        foreach (Transform fish in gameObject.transform)
        {
            // Add each fish to the fishList
            fishList.Add(fish.gameObject);
        }
        if (schooling)
        {
            int newDestination = Random.Range(0, pointsContainer.transform.childCount);
            Debug.Log(newDestination);  
            SetAllFishDestinations(newDestination);
        }
    }

    // Update is called once per frame
    void Update()
    {
        destTimer += Time.deltaTime;

        // Check if any fish has been bitten
        if (fishList.FindAll(f => f.GetComponent<FishBehaviour>().isBiting ? true : false).Count > 0)
        {
            player.isReeling = true;
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
        else
        {
            UpdateFishState(SwimState.LURED);
        }
    }
    // Set the schooling status of each fish
    void SetAllFishSchooling(bool isSchooling)
    {
        foreach (Transform fish in gameObject.transform)
        {
            fish.gameObject.GetComponent<FishBehaviour>().schooling = isSchooling;
            Debug.Log("Changed by SetAllFishSchool");
        }
    }
    // Set the new destination index of each fish
    void SetAllFishDestinations(int destinationIndex)
    {

        foreach (Transform fish in gameObject.transform)
        {
            fish.gameObject.GetComponent<FishBehaviour>().SetDestination(destinationIndex);
            fish.gameObject.GetComponent<FishBehaviour>().destReached = false;
            Debug.Log("Changed by SetAllFishDest Index");
        }
    }
    void SetAllFishDestinations(Vector3 newDest)
    {

        foreach (Transform fish in gameObject.transform)
        {
            fish.gameObject.GetComponent<FishBehaviour>().SetDestination(newDest);
            fish.gameObject.GetComponent<FishBehaviour>().destReached = false;
            Debug.Log("Changed by SetAllFishDest Vec3");
        }
    }
    void UpdateFishState(SwimState newState)
    {
        foreach (Transform fish in gameObject.transform)
        {
            if (fish.gameObject.GetComponent<FishBehaviour>().GetState() != newState)
            {
                fish.gameObject.GetComponent<FishBehaviour>().ChangeState(newState);
                Debug.Log("State Updated");
            }
        }
    }
    void SetAllFishCanBite(bool canIBite)
    {
        foreach (Transform fish in gameObject.transform)
        {
            fish.gameObject.GetComponent<FishBehaviour>().canBite = canIBite;
            Debug.Log("Changed by SetAllFishCanBite");
        }
    }
}

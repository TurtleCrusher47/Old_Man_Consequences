using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FishManager : MonoBehaviour
{
    [SerializeField]
    private GameObject fishPrefab;
    [SerializeField]
    private GameObject pointsContainer;
    private List<GameObject> fishList = new List<GameObject>();
    public bool schooling;

    [SerializeField]
    private float maxTimePerDest;

    private float destTimer;
    [SerializeField]
    private float fishCount = 5;
    // Start is called before the first frame update
    void Start()
    {
        destTimer = 0;
        schooling = true;

        for (int i = 0; i < fishCount; i++)
        {
            // Instantiate each fish
            GameObject newFish = Instantiate(fishPrefab, new Vector3(Random.Range(-9, 8), Random.Range(-5, 5), 0), Quaternion.identity);
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
        // Check if all fish have reached their destination
        // Searches the list and counts the number of fish that have reached their destination
        if (fishList.FindAll(f => f.GetComponent<FishBehaviour>().destReached ? true : false).Count == fishList.Count 
            || destTimer > maxTimePerDest)
        {
            SetAllFishDestinations(Random.Range(0, pointsContainer.transform.childCount));
            destTimer = 0;
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
            Debug.Log("Changed by SetAllFishDest");
        }
    }
}

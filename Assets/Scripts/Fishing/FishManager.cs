using System.Collections;
using System.Collections.Generic;
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

            GameObject newFish = Instantiate(fishPrefab, new Vector3(Random.Range(-9, 8), Random.Range(-5, 5), 0), Quaternion.identity);
            newFish.GetComponent<FishBehaviour>().wayPointContainer = pointsContainer;
            newFish.GetComponent<FishBehaviour>().Init();
            newFish.transform.parent = gameObject.transform;
        }
        SetAllFishSchooling(schooling);
        foreach (Transform fish in gameObject.transform)
        {
            fishList.Add(fish.gameObject);
        }
        if (schooling)
        {
            SetAllFishDestinations(Random.Range(0, pointsContainer.transform.childCount));
        }
    }

    // Update is called once per frame
    void Update()
    {
        destTimer += Time.deltaTime;
        int counter = 0;
        // Check if all fish have reached their destination
        for (int i = 0; i < fishList.Count; i++)
        {
            if (!fishList[i].GetComponent<FishBehaviour>().destReached)
            {
                break;
            }
            else
            {
                counter++;
            }
        }
        if (counter == fishCount || destTimer > maxTimePerDest)
        {
            SetAllFishDestinations(Random.Range(0, pointsContainer.transform.childCount));
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

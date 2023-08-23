using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishEnums;
namespace FishEnums
{
    public enum SwimState
    {
        IDLE = 0,
        SWIM = 1,
        LURED = 2,
        FLEE = 3,
        NUM_SWIMSTATE
    }
    enum LuredState
    {
        LURE,
        BITE,
        NUM_LUREDSTATE
    }
    public enum FishType
    {
        TYPE_SMALL = 0,
        TYPE_MEDIUM = 1,
        TYPE_BIG = 2,
        NUM_TYPE
    }
}
public class FishBehaviour : MonoBehaviour
{
   
    public FishType fishType;
    // Sprite and animations
    private SpriteRenderer sr;
    private Animator ar;
    private Vector3 targetDir;
    // For fish physics and swimming
    private Rigidbody2D rb;
    // Waypoints to swim to
    public GameObject wayPointContainer;

    private List<GameObject> waypointList = new List<GameObject>();

    // Current waypoint index
    [SerializeField]
    private int currWaypointIndex = 0;
    // Current waypoint location
    private Vector3 currWaypointLoc;
    // Minimum distance to next point
    [SerializeField]
    private float minDistToNextPoint = 1f;
    // FSM for fish swimming 
   
    [SerializeField]
    private SwimState swimState;
    [SerializeField]
    private LuredState luredState;
    // Bool to check if the fish is schooling or not
    public bool schooling = false;


    // Timers for ALL states
    // Idle state
    private float idleTimer;

    // max time between swims
    [SerializeField]
    private float maxSwimInterval = 2;
    private float maxTurnInterval;
    [SerializeField]
    private float rotationMultipler = 1.0f;
    // Timer that counts down time till next swim
    private float swimForwardTimer;
    private float swimTurnTimer;
    // No idea for now
    private float lureTimer;
    private float biteTimer;
    // Has the fish reached its current destination?
    public bool destReached;
    // Point where the fishing is fishing
    private GameObject fishingPoint;
    public bool isBiting;
    public bool canBite;
    private float movementSpeed = 1f;
    public FishItemSO fishData;
    public FishingController player;
    public void Init()
    {
        // Get components
        sr = GetComponent<SpriteRenderer>();
        ar = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        // Set initial swim state
        swimState = SwimState.IDLE;
        luredState = LuredState.LURE;
        // Set initial direction
        targetDir = new Vector3(0, 0, 0);
        // Get map children from map container object
        int children = wayPointContainer.transform.childCount;
        for (int i = 0; i < children; i++)
        {
            if (wayPointContainer.transform.GetChild(i).gameObject.name != "FishingPoint")
                waypointList.Add(wayPointContainer.transform.GetChild(i).gameObject);
           
            //Debug.Log("Index " + i + " X: " + wayPointContainer.transform.GetChild(i).position.x + " Y: " + wayPointContainer.transform.GetChild(i).position.y);
        }
        if (!schooling)
            currWaypointIndex = Random.Range(0, waypointList.Count);
        currWaypointLoc = waypointList[currWaypointIndex].transform.position;
        swimForwardTimer = 0;
        swimTurnTimer = 0;
        maxTurnInterval = maxSwimInterval / 3;
        lureTimer = 5;
        fishingPoint = GameObject.Find("FishingPoint");
        isBiting = false;
        destReached = false;
        canBite = true;
        // Randomize the movement speed
        movementSpeed = Random.Range(1, 3);
        biteTimer = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (!isBiting)
        {
            switch (swimState)
            {
                case SwimState.IDLE:
                    Idle();
                    break;
                case SwimState.SWIM:
                    Swim();
                    UpdateSpriteDirection();
                    break;
                case SwimState.LURED:
                    Lured();
                    break;
            }
        }
    }

    protected void Idle()
    {
        if(!ar.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            ar.Play("Idle");
        // Just chill until you hit the maxmimum number of seconds allowed in a state.
        idleTimer += Time.deltaTime;
        if (idleTimer > 5)
        {
            ChangeState(SwimState.SWIM);
        }
    }

    protected void Swim()
    {
        if (!ar.GetCurrentAnimatorStateInfo(0).IsName("Swim"))
            ar.Play("Swim");
        swimForwardTimer += Time.deltaTime;
        swimTurnTimer += Time.deltaTime;
        if (swimTurnTimer > maxTurnInterval)
        {
            LookTowardsDest();
        }
        if (swimForwardTimer > maxSwimInterval)
        {
            swimForwardTimer = 0;

            // Swim forward in the direction the fish is facing.
            rb.AddForce(transform.right.normalized * movementSpeed, ForceMode2D.Impulse);

        }
        // If the fish is within minDistToBextPoint units of next way point, move to next waypoint.
        destReached = Vector2.Distance(currWaypointLoc, gameObject.transform.position) < minDistToNextPoint;
        if (destReached)
        {
            if (swimState == SwimState.SWIM)
            {
                if (schooling == false)
                {
                    currWaypointIndex = Random.Range(0, waypointList.Count);
                    Debug.Log("Changed by FishBehaviour");
                }
                currWaypointLoc = waypointList[currWaypointIndex].transform.position;
            }
            ChangeState(SwimState.IDLE);
        }
        if (Vector3.Distance(gameObject.transform.position, fishingPoint.transform.position) < 2 * minDistToNextPoint && canBite)
        {
            ChangeState(SwimState.LURED);
        }
    }

    protected void LookTowardsDest()
    {
        targetDir = currWaypointLoc - gameObject.transform.position;
        var angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, movementSpeed * rotationMultipler * Time.deltaTime);
    }
  
    protected void Lured()
    {
        lureTimer += Time.deltaTime;
        if (!ar.GetCurrentAnimatorStateInfo(0).IsName("Swim"))
            ar.Play("Swim");
        // Charge towards fishing rod...?
        switch (luredState)
        {
            case LuredState.LURE:
                SwimTowardsTarget();
                break;
            case LuredState.BITE:
                currWaypointLoc = fishingPoint.transform.position;
                if (Vector3.Distance(fishingPoint.transform.position, transform.position) > 0.05f)
                {
                    LookTowardsDest();
                    Vector3 fishDir = (transform.position - fishingPoint.transform.position).normalized;
                    float dotProd = Vector3.Dot(fishDir, gameObject.transform.right);
                    if (Mathf.Abs(dotProd) > 0.9)
                    {
                        SwimTowardsTarget();
                    }
                }
                break;

        }
      
        destReached = Vector2.Distance(currWaypointLoc, gameObject.transform.position) < minDistToNextPoint;
        if (destReached)
        {
            biteTimer += Time.deltaTime;
            if (biteTimer > 2)
            {
                // Check: how different are the flavour profiles between the bait and the fish?
                float baitPrefDiff = (Vector2.Distance(fishData.FlavourPrefScale, player.GetComponent<FishingController>().selectedBait.FlavourProfile));
                Debug.Log("Pref diff: " + baitPrefDiff * 10);
                // Calculate chances to bite 
                float biteChance = Mathf.Abs((10 - (baitPrefDiff * 10)) * 10);
                Debug.Log("Bite chance: " + biteChance);
                int newInt = Random.Range(0, 100);
                if (newInt < biteChance)
                {
                    luredState = LuredState.BITE;
                    Debug.Log("Would've bitten.");
                }
                biteTimer = 0;
            }
        }
        else if (lureTimer > 10)
        {
            ChangeState(SwimState.SWIM);
        }
    }

    void SwimTowardsTarget()
    {
        swimForwardTimer += Time.deltaTime;
        swimTurnTimer += Time.deltaTime;
        if (swimTurnTimer > maxTurnInterval)
        {
            LookTowardsDest();
        }
        if (swimForwardTimer > maxSwimInterval)
        {
            swimForwardTimer = 0;

            // Swim forward in the direction the fish is facing.
            rb.AddForce(transform.right.normalized * movementSpeed, ForceMode2D.Impulse);

        }
    }

    // make da fishy look in right direction
    protected void UpdateSpriteDirection()
    {
        // Flip the sprite if the rotation angle of the sprite is > 90
        sr.flipY = Mathf.Abs(gameObject.transform.localRotation.eulerAngles.z) > 90;
    }
    public void ChangeState(SwimState newState)
    {
        switch(newState)
        {
            case SwimState.IDLE:
                idleTimer = 0;
                luredState = LuredState.LURE;
                break;
            case SwimState.SWIM:
                swimForwardTimer = 0;
                swimTurnTimer = 0;
                currWaypointIndex = Random.Range(0, waypointList.Count);
                luredState = LuredState.LURE;
                break;
            case SwimState.LURED:
                lureTimer = 0;
                currWaypointLoc = fishingPoint.transform.position + Random.insideUnitSphere * 5f;
                break;
        }
        swimState = newState;
    } 
    public void SetDestination(int index)
    {
        currWaypointIndex = index;
        currWaypointLoc = waypointList[currWaypointIndex].transform.position;
    }
    public void SetDestination(Vector3 newDest)
    {
        currWaypointIndex = -1;
        currWaypointLoc = newDest;
    }
    public SwimState GetState()
    {
        return swimState;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == fishingPoint && canBite == true && luredState == LuredState.BITE)
        {
            Debug.Log(gameObject.name + "biting");
            isBiting = true;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0;
            fishingPoint.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBehaviour : MonoBehaviour
{
    // Sprite and animations
    private SpriteRenderer sr;
    private Animator ar;
    private Vector3 targetDir;
    // For fish physics and swimming
    private Rigidbody2D rb;
    // Waypoints to swim to
    public GameObject wayPointContainer;

    private GameObject fishingPoint;
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
    protected enum SwimState
    {
        IDLE = 0, 
        SWIM = 1,
        BITE = 2,
        FLEE = 3,
        NUM_SWIMSTATE
    }
    [SerializeField]
    private SwimState swimState;

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
    //private float biteTimer;
    // Has the fish reached its current destination?
    public bool destReached;


    public void Init()
    {
        // Get components
        sr = GetComponent<SpriteRenderer>();
        ar = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        // Set initial swim state
        swimState = SwimState.IDLE;
        // Set initial direction
        targetDir = new Vector3(0, 0, 0);
        // Get map children from map container object
        int children = wayPointContainer.transform.childCount;
        for (int i = 0; i < children; i++)
        {
            if (wayPointContainer.transform.GetChild(i).gameObject.name != "FishingPoint")
                waypointList.Add(wayPointContainer.transform.GetChild(i).gameObject);
            
            else
                fishingPoint = wayPointContainer.transform.GetChild(i).gameObject;
            Debug.Log("Index " + i + " X: " + wayPointContainer.transform.GetChild(i).position.x + " Y: " + wayPointContainer.transform.GetChild(i).position.y);
        }
        if (!schooling)
            currWaypointIndex = Random.Range(0, waypointList.Count);
        currWaypointLoc = waypointList[currWaypointIndex].transform.position;
        swimForwardTimer = 0;
        swimTurnTimer = 0;
        maxTurnInterval = maxSwimInterval / 2;

    }
    // Update is called once per frame
    void Update()
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
            case SwimState.BITE:
                Bite();
                break;
        }
    }

    protected void Idle()
    {
        // Just chill until you hit the maxmimum number of seconds allowed in a state.
        idleTimer += Time.deltaTime;
        if (idleTimer > 5)
        {
            ChangeState(SwimState.SWIM);
        }
    }
    [SerializeField]
    private float movementSpeed = 1f;
    protected void Swim()
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
        // If the fish is within minDistToBextPoint units of next way point, move to next waypoint.
        destReached = Vector2.Distance(currWaypointLoc, gameObject.transform.position) < minDistToNextPoint;
        if (destReached)
        {
            if (schooling == false)
            {
                currWaypointIndex = Random.Range(0, waypointList.Count);
                Debug.Log("Changed by self");
            }
            currWaypointLoc = waypointList[currWaypointIndex].transform.position;
            ChangeState(SwimState.IDLE);
        }
    }

    protected void LookTowardsDest()
    {
        targetDir = currWaypointLoc - gameObject.transform.position;
        var angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, movementSpeed * rotationMultipler * Time.deltaTime);
    }
  
    protected void Bite()
    {
        // Charge towards fishing rod...?

    }

    // make da fishy look in right direction
    protected void UpdateSpriteDirection()
    {
        // Flip the sprite if the rotation angle of the sprite is > 90
        sr.flipY = Mathf.Abs(gameObject.transform.localRotation.eulerAngles.z) > 90;
    }
    protected void ChangeState(SwimState newState)
    {
        switch(newState)
        {
            case SwimState.IDLE:
                idleTimer = 0;
                break;
            case SwimState.SWIM:
                swimForwardTimer = 0;
                swimTurnTimer = 0;
                break;
            case SwimState.BITE:
                //biteTimer = 0;
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
        currWaypointLoc = newDest;
    }
}

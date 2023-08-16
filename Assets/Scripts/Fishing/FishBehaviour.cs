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
    [SerializeField]
    private GameObject wayPointContainer;

    private GameObject fishingPoint;
    private List<GameObject> waypointList = new List<GameObject>();

    // Current waypoint index
    private int currWaypointIndex;
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

    // Timers for ALL states
    // Idle state
    private float idleTimer;

    // max time between swims
    [SerializeField]
    private float maxSwimInterval = 2;
    // Timer that counts down time till next swim
    private float swimForwardTimer;
    // No idea for now
    private float biteTimer;


    void Awake()
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
        Debug.Log(children);
        for (int i = 0; i < children; i++)
        {
            if (wayPointContainer.transform.GetChild(i).gameObject.name != "FishingPoint")
                waypointList.Add(wayPointContainer.transform.GetChild(i).gameObject);
            else
                fishingPoint = wayPointContainer.transform.GetChild(i).gameObject;
        }
        currWaypointIndex = Random.Range(0, waypointList.Count);
        currWaypointLoc = waypointList[currWaypointIndex].transform.position;

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
                LookTowardsDest();
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
        if (idleTimer > 1)
        {
            ChangeState(SwimState.SWIM);
        }
    }
    [SerializeField]
    private float movementSpeed = 1f;
    protected void Swim()
    {
        swimForwardTimer += Time.deltaTime;
        if (swimForwardTimer > maxSwimInterval)
        {
            swimForwardTimer = 0;
           
            // Swim forward in the direction the fish is facing.
            rb.AddForce(transform.right.normalized * movementSpeed, ForceMode2D.Impulse);
            
        }
        // If the fish is within 5 units of next way point, move to next waypoint.
        if (Vector2.Distance(currWaypointLoc, gameObject.transform.position) < minDistToNextPoint)
        {
            currWaypointIndex = Random.Range(0, waypointList.Count);
            currWaypointLoc = waypointList[currWaypointIndex].transform.position;
            ChangeState(SwimState.IDLE);
        }
    }

    protected void LookTowardsDest()
    {
        targetDir = currWaypointLoc - gameObject.transform.position;
        var angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
        var angleToTurn = angle / swimForwardTimer;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, movementSpeed * Time.deltaTime);
    }
  
    protected void Bite()
    {
        // Charge towards fishing rod...?

    }

    // make da fishy look in right direction
    protected void UpdateSpriteDirection()
    {
        sr.flipY = currWaypointLoc.x < transform.position.x;
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
                break;
            case SwimState.BITE:
                biteTimer = 0;
                break;
        }
        swimState = newState;
    } 

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBehaviour : MonoBehaviour
{
    // Sprite and animations
    private SpriteRenderer sr;
    private Animator ar;
    private Vector3 dir;
    // For fish physics and swimming
    private Rigidbody2D rb;
    // Waypoints to swim to
    [SerializeField]
    private GameObject wayPointContainer;

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
        GATHER = 2,
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
    private float gatherTimer;


    void Awake()
    {
        // Get components
        sr = GetComponent<SpriteRenderer>();
        ar = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        // Set initial swim state
        swimState = SwimState.IDLE;
        // Set initial direction
        dir = new Vector3(0, 0, 0);
        // Get map children from map container object
        int children = wayPointContainer.transform.childCount;
        Debug.Log(children);
        for (int i = 0; i < children; i++)
        {
            if (wayPointContainer.transform.GetChild(i).gameObject.name != "FishingPoint")
                waypointList.Add(wayPointContainer.transform.GetChild(i).gameObject);   
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
                Swim();
                UpdateSpriteDirection();
                break;
            case SwimState.GATHER:

                Gather();
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
            // Look in direction of next waypoint and swim there
            dir = currWaypointLoc - gameObject.transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            // To swim there, apply an impulse force
            rb.AddForce(dir.normalized * movementSpeed, ForceMode2D.Impulse);
            
        }
        // If the fish is within 5 units of next way point, move to next waypoint.
        if (Vector2.Distance(currWaypointLoc, gameObject.transform.position) < minDistToNextPoint)
        {
            currWaypointIndex = Random.Range(0, waypointList.Count);
            currWaypointLoc = waypointList[currWaypointIndex].transform.position;
            ChangeState(SwimState.IDLE);
        }
    }
  
    protected void Gather()
    {
        // Swim around a single point???
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
            case SwimState.GATHER:
                gatherTimer = 0;
                break;
        }
        swimState = newState;
    } 

}

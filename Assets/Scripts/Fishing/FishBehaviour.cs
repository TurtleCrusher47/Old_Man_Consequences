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
    private Rigidbody rb;
    // Waypoints to swim to
    [SerializeField]
    private List<GameObject> waypointList = new List<GameObject>();

    // Current waypoint index
    private int currWaypointIndex;
    // Current waypoint location
    private Vector3 currWaypointLoc;
    // Minimum distance to next point
    [SerializeField]
    private float minDistToNextPoint = 1f;
    // FSM for fish swimming 
    enum SwimState
    {
        IDLE = 0, 
        SWIM = 1,
        GATHER = 2,
        NUM_SWIMSTATE
    }
    [SerializeField]
    private SwimState swimState;


    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        ar = GetComponent<Animator>();
        swimState = SwimState.IDLE;
        dir = new Vector3(0, 0, 0);
        currWaypointIndex = 0;
        currWaypointLoc = waypointList[currWaypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
       
        switch (swimState)
        {
            case SwimState.IDLE:
               
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

    // For testing

    [SerializeField]
    private float movementSpeed = 1f;
    void FixedUpdate()
    {
        // Get the current position of the game object
        Vector2 currentPos = transform.position;
        // Get the input from horizontal and vertical axis - x and y
        Vector2 moveDirection = new Vector2(Input.GetAxis("Horizontal"),
        Input.GetAxis("Vertical"));
        // To ensure the vector is unit length
        moveDirection = Vector2.ClampMagnitude(moveDirection, 1);
        // Calculate the new position based on velocity (moveDirection * movementSpeed)
        Vector2 movement = moveDirection * movementSpeed;
        // Calculate new position
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        transform.position = newPos;
    }
    protected void Idle()
    {
        // Just chill until you hit the maxmimum number of seconds allowed in a state.
    }
    protected void Swim()
    {
        // Look in direction of next waypoint and swim there
        dir = currWaypointLoc - gameObject.transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        // To swim there, apply an impulse force.
        // If the fish is within 5 units of next way point, move to next waypoint.
        if (Vector2.Distance(currWaypointLoc, gameObject.transform.position) < minDistToNextPoint)
        {
            currWaypointIndex++;
            currWaypointIndex %= waypointList.Count;
            currWaypointLoc = waypointList[currWaypointIndex].transform.position;
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBehaviour : MonoBehaviour
{
    // Sprite and animations
    private SpriteRenderer _sr;
    private Animator _ar;
    private Vector2 _dir;
    // For fish physics and swimming
    private Rigidbody _rb;
    // Waypoints to swim to
    [SerializeField]
    private Transform[] _waypointList;
    // Current waypoint
    private int _currWaypoint;

    // FSM for fish swimming 
    enum SwimState
    {
        IDLE = 0, 
        SWIM = 1,
        GATHER = 2,
        NUM_SWIMSTATE
    }
    [SerializeField]
    private SwimState _swimState;


    void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _ar = GetComponent<Animator>();
        _swimState = SwimState.IDLE;
        _dir = new Vector2(0, 0);
        _currWaypoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        switch (_swimState)
        {
            case SwimState.IDLE:
               
                break;
            case SwimState.SWIM:
                Swim();
                break;
            case SwimState.GATHER:

                Gather();
                break;
        }
    }
    protected void Idle()
    {
        // Just chill until you hit the maxmimum number of seconds allowed in a state.
    }
    protected void Swim()
    {
        // Look in direction of next waypoint and swim there
        // To swim there, apply an impulse force. 
        // While fish is not within 5 units of the next point, apply another impulse force.
    }
  
    protected void Gather()
    {
        // Swim around a single point???
    }
}

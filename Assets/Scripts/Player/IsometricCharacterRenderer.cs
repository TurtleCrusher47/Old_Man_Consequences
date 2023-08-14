using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricCharacterRenderer : MonoBehaviour
{
    public static readonly string[] staticDirections = { "Static N", "Static W", "Static S", "Static E"};

    public static readonly string[] runDirections = { "Walk N", "Walk W", "Walk S", "Walk E"};

    public static readonly string[] idleDirections = { "Idle N", "Idle W", "Idle S", "Idle E"}; 

    private Animator ar;
    int lastDirection;

    float timer;

    private void Awake()
    {
        ar = GetComponent<Animator>();   
    }

    public void SetDirection(Vector2 direction)
    {
        //use the Run states by default
        string[] directionArray = null;
        //measure the magnitude of the input.
        if (direction.magnitude < .01f)
        {
            //if we are basically standing still, we'll use the Static states
            //we won't be able to calculate a direction if the user isn't pressing one, anyway!
            directionArray = staticDirections;

            timer += Time.fixedDeltaTime;
            // Debug.Log("TIMER:" + timer);

            if (timer >= 6)
                directionArray = idleDirections;
        }
        else
        {
            //we can calculate which direction we are going in
            //use DirectionToIndex to get the index of the slice from the direction vector
            //save the answer to lastDirection
            directionArray = runDirections;
            lastDirection = DirectionToIndex(direction, 4);

            timer = 0;
        }
        
        // Test this
        ar.SetBool(directionArray[lastDirection], true);
    }

    //this function converts a Vector2 direction to an index to a slice around a circle
    //this goes in a counter-clockwise direction.
    public static int DirectionToIndex(Vector2 dir, int sliceCount)
    {
        //get the normalized direction
        Vector2 normDir = dir.normalized;
        //calculate how many degrees one slice is
        float step = 360f / sliceCount;
        //calculate how many degress half a slice is.
        //we need this to offset the pie, so that the North (UP) slice is aligned in the center
        float halfstep = step / 2;
        //get the angle from -180 to 180 of the direction vector relative to the Up vector.
        //this will return the angle between dir and North.
        float angle = Vector2.SignedAngle(Vector2.up, normDir);
        //add the halfslice offset
        angle += halfstep;
        //if angle is negative, then let's make it positive by adding 360 to wrap it around.
        if (angle < 0) angle += 360;
        //calculate the amount of steps required to reach this angle
        float stepCount = angle / step;
        //round it, and we have the answer!
        return Mathf.FloorToInt(stepCount);
    }
}
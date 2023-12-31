using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricCharacterRenderer : MonoBehaviour
{
    public static readonly string walking = "Walking";

    public static readonly string[] directions = {"FacingUp", "FacingLeft", "FacingDown", "FacingRight"}; 

    private Animator ar;
    int lastDirection;

    private void Start()
    {
        ar = GetComponent<Animator>();   
    }

    public void SetDirection(Vector2 direction)
    {
        //measure the magnitude of the input.
        if (direction.magnitude < .01f)
        {
            ar.SetBool("Walking", false);
        }
        else
        {
            ar.SetBool("Walking", true);
            ar.SetBool(directions[lastDirection], false);
            lastDirection = DirectionToIndex(direction, 4);
        }
        
        // Test this
        ar.SetBool(directions[lastDirection], true);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerIsometric : MonoBehaviour
{
    [SerializeField] PlayerData playerData;

    private IsometricCharacterRenderer isometricCharacterRenderer;
    private Rigidbody2D rb;
    Vector2 moveDirection;
    Vector2 currentPos;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isometricCharacterRenderer = GetComponent<IsometricCharacterRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // // Get the current position of the game object
        // currentPos = transform.position;

        // Get the input from horizontal and vertical axes - x and y
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        // To ensure the vector is unit length
        moveDirection = Vector2.ClampMagnitude(moveDirection, 1);
    }

    void FixedUpdate()
    {
        // Calculate the new direction based on velocity (moveDirection * movementSpeed)
        Vector2 velocity = moveDirection * playerData.MovementSpeed;

        // // Calculate new position
        // Vector2 newPos = currentPos + velocity * Time.fixedDeltaTime;
        // transform.position = newPos;

        rb.velocity = velocity;

        // Update the sprite to show
        isometricCharacterRenderer.SetDirection( velocity);
    }
}
